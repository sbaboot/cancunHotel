using CancunHotel.Domain.Service.Contract.Reservation;
using CancunHotel.Domain.Service.Contract.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Domain.Service.Contract
{
    public interface IServiceCatalog
    {
        IRoomService RoomService { get;  }
        IReservationService ReservationService { get; }
    }
}
