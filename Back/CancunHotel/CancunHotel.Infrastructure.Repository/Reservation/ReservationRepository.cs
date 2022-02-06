using CancunHotel.Infrastructure.Repository.Contract.Reservation;
using CancunHotel.Infrastructure.Repository.Contract.Reservation.QueryObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Infrastructure.Repository.Reservation
{
    public class ReservationRepository : IReservationRepository
    {
        #region Properties
        private CancunHotelDbContext DbContext { get; }
        private IQueryable<Domain.Model.Reservation> ReadOnlyData => this.DbContext.Reservation.AsNoTracking();
        private DbSet<Domain.Model.Reservation> WriteableData => this.DbContext.Reservation;
        #endregion

        #region Constructor
        public ReservationRepository(CancunHotelDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        #endregion
        #region Public methods
        public Domain.Model.Reservation Get(ReservationQueryObject queryObject)
        {
            return this.ApplyFilter(queryObject).FirstOrDefault();
        }

        public List<Domain.Model.Reservation> Filter(ReservationQueryObject queryObject)
        {
            return this.ApplyFilter(queryObject).ToList();
        }

        public List<Domain.Model.Reservation> Filter(ReservationQueryObject queryObject, out int count)
        {
            count = this.ApplyFilter(queryObject, true).Count();
            return this.ApplyFilter(queryObject).ToList();
        }

        public int Count(ReservationQueryObject queryObject)
        {
            return ApplyFilter(queryObject, true).Count();
        }
        public void Add(Domain.Model.Reservation entity)
        {
            this.WriteableData.Add(entity);
        }

        public void Update(Domain.Model.Reservation entity)
        {
            this.WriteableData.Update(entity);
        }

        public void Remove(Domain.Model.Reservation entity)
        {
            this.WriteableData.Remove(entity);
        }

        #endregion

        private IQueryable<Domain.Model.Reservation> ApplyFilter(ReservationQueryObject queryObject, bool ignoreSkipTake = false)
        {
            IQueryable<Domain.Model.Reservation> query = this.ReadOnlyData;
            if (queryObject != null)
            {
                if (queryObject.WriteMode)
                    query = this.WriteableData;

                #region Where
                if (queryObject.Id.HasValue)
                {
                    query = query.Where(x => x.Id == queryObject.Id);
                }

                if (queryObject.RoomId.HasValue)
                {
                    query = query.Where(x => x.ReservationRooms.Any(x => x.RoomId == queryObject.RoomId));
                }

                
                if (queryObject.CheckInDate.HasValue)
                {
                    query = query.Where(x => x.CheckInDate <= queryObject.CheckInDate);
                }

                if (queryObject.CheckOutDate.HasValue)
                {
                    query = query.Where(x => x.CheckOutDate <= queryObject.CheckOutDate);
                }
                #endregion

                #region Include
                if (queryObject.IncludeReservationRooms)
                {
                    query = query.Include(x => x.ReservationRooms);
                }

                if (queryObject.IncludeReservationToRoom)
                {
                    query = query.Include(x => x.ReservationRooms).ThenInclude(x => x.Room);
                }


                #endregion

                if (!ignoreSkipTake)
                {
                    query = query.ApplyOrder(
                        queryObject.SortField,
                        queryObject.SortOrderAscending,
                        new List<SortableField<Domain.Model.Reservation>>()
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
