using CancunHotel.Api.ViewModel;
using CancunHotel.Domain.Service.Contract.Room;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Controllers.Room
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomService RoomService { get; set; }
        public RoomController(IRoomService roomService)
        {
            RoomService = roomService;
        }
        #region GET
        [HttpGet("availability/{roomId}")]
        public RoomAvailability CheckRoomAvailability(int roomId)
        {
            return RoomService.CheckRoomAvailability(roomId);
        }
        #endregion
    }
}
