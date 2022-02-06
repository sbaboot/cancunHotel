using CancunHotel.Api.DataMapper.Contract;
using CancunHotel.Api.ViewModel;
using CancunHotel.Domain.Service.Contract.Room;
using CancunHotel.Infrastructure.Repository.Contract;
using CancunHotel.Infrastructure.Repository.Contract.Reservation.QueryObject;
using CancunHotel.Infrastructure.Repository.Contract.Room.QueryObject;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Domain.Service.Room
{
    public class RoomService : IRoomService
    {

        private ICancunHotelUnitOfWork UnitOfWork { get; set; }
        private IDataMapperCatalog Mapper { get; set; }
        public RoomService(ICancunHotelUnitOfWork unitOfWork, IDataMapperCatalog mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        #region Public methods
        public RoomAvailability CheckRoomAvailability(int roomId)
        {
            var roomInDb = UnitOfWork.RoomRepository.Get(new RoomQueryObject()
            {
                Id = roomId,
                IncludeReservation = true,
            });

            if (roomInDb == null)
                throw new Exception("No room found");

            var tupleReservationDates = GetBookingReservedDates(roomInDb);
            var reservedDates = tupleReservationDates.Item1;
            var room = tupleReservationDates.Item2;            

            var result = GetAvailableDates(reservedDates, room);
            return result;
        }

        #endregion
        #region Private methods
        private RoomAvailability GetAvailableDates(Dictionary<DateTime, int> reservedDates, Model.Room room)
        {
            var availableDates = new List<DateTime>();

            for (int i = 1; i <= 30; i++)
            {
                if (!reservedDates.Any(rd => rd.Key.Date == DateTime.Now.Date.AddDays(i).Date))
                {
                    availableDates.Add(DateTime.Now.Date.AddDays(i));
                }
            }

            if (!availableDates.Any())
            {
                throw new Exception($"The room {room.Id} does not have avaliable booking dates.");
            }
            return new RoomAvailability
            {
                RoomId = room.Id,
                RoomName = room.RoomName,
                Description = room.Description,
                Capacity = room.Capacity,
                Price = room.RoomPrice,
                AvailableDates = availableDates,
                UnavailableDates = reservedDates,
            };
        }

        private Tuple<Dictionary<DateTime, int>, Model.Room> GetBookingReservedDates(Model.Room room)
        {
            var reservationsByRoom = room.ReservationRooms.Select(br => br.Reservation).Where(b => b.CheckInDate.Date > DateTime.Now.Date);

            var reservationDates = new Dictionary<DateTime, int>();

            foreach (var reservation in reservationsByRoom)
            {
                for (var day = reservation.CheckInDate.Date; day.Date <= reservation.CheckOutDate.Date; day = day.AddDays(1))
                {
                    reservationDates.Add(day, reservation.Id);
                }
            }

            return new Tuple<Dictionary<DateTime, int>, Model.Room>(reservationDates, room);
        }
        #endregion
    }
}
