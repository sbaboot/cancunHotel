using CancunHotel.Api.ViewModel;
using System.Collections.Generic;

namespace CancunHotel.Domain.Service.Contract.Reservation
{
    public interface IReservationService
    {
        List<ReservationResponse> GetAll();
        ReservationResponse GetById(int id);
        void Delete(int reservationId);
        int Save(SaveReservation request);
    }
}
