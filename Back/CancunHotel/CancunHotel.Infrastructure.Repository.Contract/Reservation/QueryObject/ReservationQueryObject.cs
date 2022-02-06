using Foundation.Filter;
using System;

namespace CancunHotel.Infrastructure.Repository.Contract.Reservation.QueryObject
{
    public class ReservationQueryObject : Foundation.Filter.QueryObject
    {

        public ReservationQueryObject() : base() { }
        public ReservationQueryObject(IQueryObject @object) : base(@object) { }

        public bool WriteMode { get; set; } = false;
        public int? Id { get; set; }
        public int? RoomId { get; set; }
        public bool IncludeReservationRooms { get; set; }
        public bool IncludeReservationToRoom { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

    }
}
