using CancunHotel.Infrastructure.Repository.Contract.Room;
using CancunHotel.Infrastructure.Repository.Contract.Room.QueryObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Infrastructure.Repository.Room
{
    public class RoomRepository : IRoomRepository
    {
        #region Properties
        private CancunHotelDbContext DbContext { get; }
        private IQueryable<Domain.Model.Room> ReadOnlyData => this.DbContext.Room.AsNoTracking();
        private DbSet<Domain.Model.Room> WriteableData => this.DbContext.Room;
        #endregion

        #region Constructor
        public RoomRepository(CancunHotelDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        #endregion
        #region Public methods
        public Domain.Model.Room Get(RoomQueryObject queryObject)
        {
            return this.ApplyFilter(queryObject).FirstOrDefault();
        }

        public List<Domain.Model.Room> Filter(RoomQueryObject queryObject)
        {
            return this.ApplyFilter(queryObject).ToList();
        }

        public List<Domain.Model.Room> Filter(RoomQueryObject queryObject, out int count)
        {
            count = this.ApplyFilter(queryObject, true).Count();
            return this.ApplyFilter(queryObject).ToList();
        }

        public int Count(RoomQueryObject queryObject)
        {
            return ApplyFilter(queryObject, true).Count();
        }
        public void Add(Domain.Model.Room entity)
        {
            this.WriteableData.Add(entity);
        }

        public void Update(Domain.Model.Room entity)
        {
            this.WriteableData.Update(entity);
        }

        public void Remove(Domain.Model.Room entity)
        {
            this.WriteableData.Remove(entity);
        }

        #endregion

        private IQueryable<Domain.Model.Room> ApplyFilter(RoomQueryObject queryObject, bool ignoreSkipTake = false)
        {
            IQueryable<Domain.Model.Room> query = this.ReadOnlyData;
            if (queryObject != null)
            {
                if (queryObject.WriteMode)
                    query = this.WriteableData;

                #region Where

                if (queryObject.Id.HasValue)
                    query = query.Where(a => a.Id == queryObject.Id.Value);

                if (queryObject.OnlyWithAvailableRoom)
                {
                    query = query.Where(room => room.ReservationRooms.All(r => r.Reservation.CheckOutDate < queryObject.CheckInDate || r.Reservation.CheckInDate > queryObject.CheckOutDate));
                }
                #endregion

                #region Include
                if (queryObject.IncludeReservation)
                {
                    query = query.Include(x => x.ReservationRooms).ThenInclude(x => x.Reservation);
                }

                #endregion

                if (!ignoreSkipTake)
                {
                    query = query.ApplyOrder(
                        queryObject.SortField,
                        queryObject.SortOrderAscending,
                        new List<SortableField<Domain.Model.Room>>()
                        {
                        }
                    );

                    //-- Dernière instruction obligatoirement
                    query = query.ApplySkipAndTake(queryObject.PageIndex, queryObject.PageSize);
                }
            }
            return query;
        }
    }
}
