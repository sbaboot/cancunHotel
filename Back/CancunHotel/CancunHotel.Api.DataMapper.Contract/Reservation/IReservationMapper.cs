using CancunHotel.Api.ViewModel;
using CancunHotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Api.DataMapper.Contract.Reservation
{
    public interface IReservationMapper
    {
        Domain.Model.Reservation UpdateModel(SaveReservation request, Domain.Model.Reservation reservation);
        ReservationResponse Convert(Domain.Model.Reservation reservation);
    }
}
