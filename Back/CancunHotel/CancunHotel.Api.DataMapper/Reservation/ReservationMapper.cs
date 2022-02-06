using CancunHotel.Api.DataMapper.Contract.Reservation;
using CancunHotel.Api.ViewModel;
using CancunHotel.Domain.Enum;
using CancunHotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Api.DataMapper.Reservation
{
    public class ReservationMapper : IReservationMapper
    {
        public ReservationResponse Convert(Domain.Model.Reservation reservation)
        {
            if (reservation == null)
            {
                return new ReservationResponse();
            }

            var result = new ReservationResponse()
            {
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                ClientName = reservation.ClientName,
                ClientEmail = reservation.ClientEmail,                
                ReservationId = reservation.Id, 
                Rooms = new List<RoomResponse>(),
            };

            foreach (var reservationRoom in reservation.ReservationRooms)
            {
                result.Rooms?.Add(new RoomResponse()
                {
                    RoomId = reservationRoom.RoomId,
                    Capacity = reservationRoom.Room.Capacity,
                    Description = reservationRoom.Room.Description,
                    Price = reservationRoom.Room.RoomPrice,
                    RoomName = reservationRoom.Room.RoomName,
                    BedType = ConvertEnumToStringBedType(reservationRoom.Room.BedType),
                });
            }
            return result;
        }

        public Domain.Model.Reservation UpdateModel(SaveReservation request, Domain.Model.Reservation reservation)
        {
            if (reservation == null)
            {
                reservation = new Domain.Model.Reservation();
            }

            if (request == null)
            {
                return reservation;
            }

            reservation.Id = request.ReservationId;
            reservation.CheckInDate = request.FromDate;
            reservation.CheckOutDate = request.ToDate;
            reservation.ClientEmail = request.ClientEmail;
            reservation.ClientName = request.ClientName;

            if (reservation.ReservationRooms == null)
            {
                reservation.ReservationRooms = new List<ReservationRoom>();
            }

            var booking = reservation.ReservationRooms.FirstOrDefault(x => x.ReservationId == request.ReservationId);
            if (booking == null)
            {
                reservation.ReservationRooms.Add(new ReservationRoom()
                {
                   RoomId = request.RoomId,
                   ReservationId = request.ReservationId,
                });
            }
            else
            {
                booking.RoomId = request.RoomId;
                booking.ReservationId = request.ReservationId;
            }
           
            return reservation;
        }

        private string ConvertEnumToStringBedType(BedType bedType)
        {
            return bedType switch
            {
                BedType.SINGLE => "Single size",
                BedType.TWIN => "Twin size",
                BedType.QUEEN => "Queen size",
                BedType.KING => "King size",
                _ => string.Empty,
            };
        }
    }
}