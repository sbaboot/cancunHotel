using CancunHotel.Infrastructure.Repository.Contract.Reservation.QueryObject;
using System.Collections.Generic;
using ReservationModel = CancunHotel.Domain.Model.Reservation;

namespace CancunHotel.Infrastructure.Repository.Contract.Reservation
{
    public interface IReservationRepository
    {
        ReservationModel Get(ReservationQueryObject queryObject);
        List<ReservationModel> Filter(ReservationQueryObject queryObject);
        List<ReservationModel> Filter(ReservationQueryObject queryObject, out int count);
        int Count(ReservationQueryObject queryObject);
        void Add(ReservationModel entity);
        void Update(ReservationModel entity);
        void Remove(ReservationModel entity);
    }
}
