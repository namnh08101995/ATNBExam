using ATNB.Model;

namespace ATNB.Repository.Abstractions
{
    public interface IAirPlaneRepository : IGenericRepository<AirPlane>
    {
        AirPlane GetById(string id);
    }
}
