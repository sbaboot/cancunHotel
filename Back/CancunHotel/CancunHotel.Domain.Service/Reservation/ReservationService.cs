using CancunHotel.Api.DataMapper.Contract;
using CancunHotel.Api.ViewModel;
using CancunHotel.Domain.Service.Contract;
using CancunHotel.Domain.Service.Contract.Reservation;
using CancunHotel.Infrastructure.Repository.Contract;
using CancunHotel.Infrastructure.Repository.Contract.Reservation.QueryObject;
using CancunHotel.Infrastructure.Repository.Contract.Room.QueryObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CancunHotel.Domain.Service.Reservation
{
    public class ReservationService : IReservationService
    {
        private ICancunHotelUnitOfWork UnitOfWork { get; set; }
        private IServiceCatalog ServiceCatalog { get; set; }
        private IDataMapperCatalog Mapper { get; set; }
        public ReservationService(ICancunHotelUnitOfWork unitOfWork, IDataMapperCatalog mapper, IServiceCatalog serviceCatalog)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            ServiceCatalog = serviceCatalog;
        }

        #region Public methods
        public List<ReservationResponse> GetAll()
        {
            var result = new List<ReservationResponse>();
            var reservationList = UnitOfWork.ReservationRepository.Filter(new ReservationQueryObject()
            { 
                IncludeReservationToRoom = true,
            });

            foreach (var reservation in reservationList)
            {
                var res = Mapper.ReservationMapper.Convert(reservation);
                result.Add(res);
            }
            return result;
        }


        public ReservationResponse GetById(int id)
        {
            var reservation = UnitOfWork.ReservationRepository.Get(new ReservationQueryObject()
            {
                Id = id,
                IncludeReservationToRoom = true,
            });

            return Mapper.ReservationMapper.Convert(reservation);
        }

 

        public int Save(SaveReservation request)
        {
            var fromDate = request.FromDate.Date;
            var toDate = request.ToDate.Date;
            var totalDays = (toDate - fromDate).Days + 1;


            if (Math.Abs((fromDate - DateTime.UtcNow).TotalDays) > 30)
            {
                throw new Exception("You can't book 30 days before your journey");
            }

            if (totalDays <= 0)
            {
                throw new Exception("Error: Verify your date");
            }
            if (totalDays > 3)
            {
                throw new Exception("You journey can't last more than 3 days");
            }

            if (fromDate <= DateTime.UtcNow)
            {
                throw new Exception("You can't take your reservation begining today or sooner");
            }

            var availableDates = ServiceCatalog.RoomService.CheckRoomAvailability(request.RoomId).AvailableDates;
            var noValidDates = ServiceCatalog.RoomService.CheckRoomAvailability(request.RoomId).UnavailableDates;

            if (!availableDates.Contains(fromDate) && noValidDates[fromDate] != request.ReservationId)
            {
                throw new Exception("The room is not available at theses dates");
            }

            var dateList = noValidDates.Select(x => x.Key).Concat(availableDates).OrderBy(x => x).ToList();

            var indexFromDate = dateList.IndexOf(fromDate);
            var indexToDate = dateList.IndexOf(toDate);

            if (indexFromDate == -1 || indexToDate == -1)
            {
                throw new Exception("The room is not available at theses dates");
            }

            var journeyDates = dateList.Skip(indexFromDate).Take(indexToDate - indexFromDate + 1);
            if (totalDays != journeyDates.Count())
            {
                throw new Exception("The room is not available at theses dates");
            }

            var reservation = UnitOfWork.ReservationRepository.Get(new ReservationQueryObject()
            {
                Id = request.ReservationId,
                IncludeReservationToRoom = true,
                WriteMode = true,
            });

            reservation = Mapper.ReservationMapper.UpdateModel(request, reservation);

            if (reservation == null)
            {
                throw new Exception("No reservation registered");
            }

            if (reservation.Id == 0)
            {
                UnitOfWork.ReservationRepository.Add(reservation);
            }
            else
            {
                UnitOfWork.ReservationRepository.Update(reservation);
            }

            UnitOfWork.SaveChanges();
            return reservation.Id;
        }

        public void Delete(int id)
        {
            var reservation = UnitOfWork.ReservationRepository.Get(new ReservationQueryObject()
            {
                Id = id,
                WriteMode = true,
            });


            if (reservation == null)
            {
                throw new Exception("No reservation to cancel");
            }

            UnitOfWork.ReservationRepository.Remove(reservation);
            UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
