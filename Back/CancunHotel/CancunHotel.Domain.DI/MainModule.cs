using CancunHotel.Api.DataMapper;
using CancunHotel.Api.DataMapper.Contract;
using CancunHotel.Api.DataMapper.Contract.Reservation;
using CancunHotel.Api.DataMapper.Reservation;
using CancunHotel.Domain.Service;
using CancunHotel.Domain.Service.Contract;
using CancunHotel.Domain.Service.Contract.Reservation;
using CancunHotel.Domain.Service.Contract.Room;
using CancunHotel.Domain.Service.Reservation;
using CancunHotel.Domain.Service.Room;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CancunHotel.Domain.DI
{
    public static class MainModule
    {
        public static IServiceCollection AddCancunHotelApiDomain(this IServiceCollection services)
        {

            services.AddScoped<IServiceCatalog, ServiceCatalog>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();

            return services.AddCancunHotelApiDataMapper();
        }



        private static IServiceCollection AddCancunHotelApiDataMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataMapperCatalog, DataMapperCatalog>();
            services.AddScoped<IReservationMapper, ReservationMapper>();

            return services;
        }
    }

}
