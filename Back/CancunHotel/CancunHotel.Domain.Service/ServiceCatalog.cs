using CancunHotel.Domain.Service.Contract;
using System;
using Microsoft.Extensions.DependencyInjection;
using CancunHotel.Domain.Service.Contract.Room;
using CancunHotel.Domain.Service.Contract.Reservation;

namespace CancunHotel.Domain.Service
{
    public class ServiceCatalog : IServiceCatalog
    {
        private IServiceProvider ServiceProvider { get; }
        public IRoomService RoomService => this.ServiceProvider.GetService<IRoomService>();
        public IReservationService ReservationService => this.ServiceProvider.GetService<IReservationService>();
        public ServiceCatalog(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
    }
}
