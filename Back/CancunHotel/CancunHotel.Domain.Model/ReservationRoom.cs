using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Domain.Model
{
    // <summary>
    /// ReservationRoom data model
    /// </summary>
    public class ReservationRoom
    {
        /// <summary>
        /// ReservationRoom Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Reservation Identifier
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// Room Identifier
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Reservation relationship
        /// </summary>
        public Reservation Reservation { get; set; }

        /// <summary>
        /// Room relatioship
        /// </summary>
        public Room Room { get; set; }
    }
}