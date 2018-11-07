using ATNB.Model;

namespace ATNB.Service.Abstractions
{
    public interface IHelicopterService : IEntityService<Helicopter>
    {
        Helicopter GetById(string id);
    }
}
