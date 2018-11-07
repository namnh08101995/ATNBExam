using ATNB.Model;

namespace ATNB.Service.Abstractions
{
    public interface IAirPortService : IEntityService<AirPort>
    {
        AirPort GetById(string id);
    }
}
