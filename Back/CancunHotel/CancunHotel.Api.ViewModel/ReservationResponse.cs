using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Api.ViewModel
{
    public class ReservationResponse
    {
        /// <summary>
        /// Booking Identifier
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// end-user owner email
        /// </summary>
        public string ClientEmail { get; set; }

        public string ClientName { get; set; }

        /// <summary>
        /// check-in date
        /// </summary>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// check-out date
        /// </summary>        
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Rooms of the booking
        /// </summary>
        public List<RoomResponse> Rooms { get; set; }

    }
}
