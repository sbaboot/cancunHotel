using CancunHotel.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Domain.Service.Contract.Room
{
    public interface IRoomService
    {
        RoomAvailability CheckRoomAvailability(int roomId);
    }
}
