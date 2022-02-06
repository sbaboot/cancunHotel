using CancunHotel.Infrastructure.Repository.Contract.Room.QueryObject;
using System.Collections.Generic;
using RoomModel = CancunHotel.Domain.Model.Room;

namespace CancunHotel.Infrastructure.Repository.Contract.Room
{
    public interface IRoomRepository
    {
        RoomModel Get(RoomQueryObject queryObject);
        List<RoomModel> Filter(RoomQueryObject queryObject);
        List<RoomModel> Filter(RoomQueryObject queryObject, out int count);
        int Count(RoomQueryObject queryObject);
        void Add(RoomModel entity);
        void Update(RoomModel entity);
        void Remove(RoomModel entity);
    }
}
