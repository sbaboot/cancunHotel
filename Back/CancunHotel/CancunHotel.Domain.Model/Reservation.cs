using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Domain.Model
{
    /// <summary>
    /// Reservation data model
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Reservation Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// end-user email
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientName { get; set; }

        /// <summary>
        /// Check-In Date
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Check-Out Date
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Many to many relationship with Rooms table
        /// </summary>
        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}