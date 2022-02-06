using CancunHotel.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Domain.Model
{
    public class Room
    {
        /// <summary>
        /// Room Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Room Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        /// <summary>
        /// Maximum number of guests
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// Room cost
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal RoomPrice { get; set; }

        /// <summary>
        /// Type of bed
        /// </summary>
        public BedType BedType { get; set; }

        /// <summary>
        /// Room Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Many to many relationship with Reservations table
        /// </summary>
        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}