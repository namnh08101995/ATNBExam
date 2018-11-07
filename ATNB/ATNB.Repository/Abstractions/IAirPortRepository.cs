using ATNB.Model;

namespace ATNB.Repository.Abstractions
{
    public interface IAirPortRepository : IGenericRepository<AirPort>
    {
        AirPort GetById(string id);
    }
}
