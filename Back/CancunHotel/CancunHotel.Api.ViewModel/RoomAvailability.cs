using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Api.ViewModel
{
         /// <summary>
        /// Room information with avaliable dates
        /// </summary>
        public class RoomAvailability
        {
            /// <summary>
            /// Room Identifier
            /// </summary>
            public int RoomId { get; set; }

            /// <summary>
            /// Room Name
            /// </summary>
            public string RoomName { get; set; }

            /// <summary>
            /// Room Description
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// Maximum number of guests
            /// </summary>
            public int Capacity { get; set; }

            /// <summary>
            /// Room cost
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// List of avaliable dates
            /// </summary>
            public List<DateTime> AvailableDates { get; set; }
            public Dictionary<DateTime, int> UnavailableDates { get; set; }
        }
    }