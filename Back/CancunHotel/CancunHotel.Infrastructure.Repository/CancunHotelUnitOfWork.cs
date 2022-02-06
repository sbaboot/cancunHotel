using CancunHotel.Infrastructure.Repository.Contract;
using CancunHotel.Infrastructure.Repository.Contract.Reservation;
using CancunHotel.Infrastructure.Repository.Contract.Room;
using Foundation.Api.Database.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Infrastructure.Repository
{
    public class CancunHotelUnitOfWork : ICancunHotelUnitOfWork
    {
        public IRoomRepository RoomRepository { get; }
        public IReservationRepository ReservationRepository { get; }
        public CancunHotelDbContext DbContext { get; }

        public CancunHotelUnitOfWork(
            CancunHotelDbContext dbContext,
            IRoomRepository roomRepository,
            IReservationRepository reservationRepository)
        {
            DbContext = dbContext;
            RoomRepository = roomRepository;
            ReservationRepository = reservationRepository;
        }

        public int SaveChanges()
        {
           return DbContext.SaveChanges();
        }
    }
}
