using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Api.ViewModel
{
    public class SaveReservation
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}