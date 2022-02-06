using CancunHotel.Api.DataMapper.Contract.Reservation;

namespace CancunHotel.Api.DataMapper.Contract
{
    public interface IDataMapperCatalog
    {
        IReservationMapper ReservationMapper { get; }

    }
}
