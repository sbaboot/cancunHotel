using CancunHotel.Infrastructure.Repository.Contract.Reservation;
using CancunHotel.Infrastructure.Repository.Contract.Room;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Infrastructure.Repository.Contract
{
    public interface ICancunHotelUnitOfWork : IUnitOfWork
    {
        IRoomRepository RoomRepository { get; }
        IReservationRepository ReservationRepository { get; }
    }
}
