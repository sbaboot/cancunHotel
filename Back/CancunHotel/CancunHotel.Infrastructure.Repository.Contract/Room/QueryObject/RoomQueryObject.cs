using Foundation.Filter;
using System;

namespace CancunHotel.Infrastructure.Repository.Contract.Room.QueryObject
{
    public class RoomQueryObject : Foundation.Filter.QueryObject
    {

        public RoomQueryObject() : base() { }
        public RoomQueryObject(IQueryObject @object) : base(@object) { }

        public bool WriteMode { get; set; } = false;
        public int? Id { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public decimal RoomPrice { get; set; }
        public string Description { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public bool IncludeReservation { get; set; }
        public bool OnlyWithAvailableRoom { get; set; }

    }
}
