using CancunHotel.Infrastructure.Repository;
using CancunHotel.Infrastructure.Repository.Contract;
using CancunHotel.Infrastructure.Repository.Contract.Reservation;
using CancunHotel.Infrastructure.Repository.Contract.Room;
using CancunHotel.Infrastructure.Repository.Reservation;
using CancunHotel.Infrastructure.Repository.Room;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CancunHotel.Infrastructure.DI
{
    public static class MainModule
    {
        public static IServiceCollection AddHotelCancunApiInfrastructure(this IServiceCollection services)
        {

            // UoW
            services.AddScoped<ICancunHotelUnitOfWork, CancunHotelUnitOfWork>();

            // Repositories
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            return services;
        }
    }
}
