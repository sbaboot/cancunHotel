using CancunHotel.Api.ViewModel;
using CancunHotel.Domain.Service.Contract.Reservation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CancunHotel.Controllers.Reservation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationService ReservationService { get; set; }
        public ReservationController(IReservationService roomService)
        {
            ReservationService = roomService;
        }
        #region Get

        [HttpGet()]
        public List<ReservationResponse> GetAll()
        {
            return ReservationService.GetAll();
        }

        [HttpGet("{id}")]
        public ReservationResponse GetById(int id)
        {
            return ReservationService.GetById(id);
        }

        #endregion
        #region POST
        [HttpPost]
        public int Save([FromBody] SaveReservation request)
        {
            return ReservationService.Save(request);
        }
        #endregion
        
        #region DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ReservationService.Delete(id);
        }
        #endregion
    }
}