using ATNB.Model;

namespace ATNB.Service.Abstractions
{
    public interface IAirPlaneService : IEntityService<AirPlane>
    {
        AirPlane GetById(string id);
    }
}
