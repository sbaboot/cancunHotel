using CancunHotel.Api.DataMapper.Contract;
using CancunHotel.Api.DataMapper.Contract.Reservation;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace CancunHotel.Api.DataMapper
{
    public class DataMapperCatalog : IDataMapperCatalog
    {
        private IServiceProvider ServiceProvider { get; }
        public IReservationMapper ReservationMapper => this.ServiceProvider.GetService<IReservationMapper>();
        public DataMapperCatalog(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
    }
}
