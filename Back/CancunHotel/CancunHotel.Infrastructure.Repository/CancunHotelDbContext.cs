using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Infrastructure.Repository
{
    public class CancunHotelDbContext : DbContext
    {
        public DbSet<Domain.Model.Reservation> Reservation { get; set; }
        public DbSet<Domain.Model.ReservationRoom> ReservationRoom { get; set; }
        public DbSet<Domain.Model.Room> Room { get; set; }

        public CancunHotelDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Model.Room>()
                .HasData(
                new Domain.Model.Room
                {
                    Id = 1,
                    RoomName = "Room march 2020",
                    Capacity = 1,
                    RoomPrice = 400,
                    Description = "Room with the most beautiful view",
                    BedType = Domain.Enum.BedType.KING,
                });

            modelBuilder.Entity<Domain.Model.Room>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Model.Room>().HasMany(x => x.ReservationRooms).WithOne(x => x.Room);
            modelBuilder.Entity<Domain.Model.Room>().Property(x => x.BedType).HasConversion<byte>();

            modelBuilder.Entity<Domain.Model.Reservation>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Model.Reservation>().HasMany(x => x.ReservationRooms).WithOne(x => x.Reservation);

            modelBuilder.Entity<Domain.Model.ReservationRoom>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Model.ReservationRoom>().HasOne(x => x.Reservation).WithMany(x => x.ReservationRooms);
        }
    }

}
