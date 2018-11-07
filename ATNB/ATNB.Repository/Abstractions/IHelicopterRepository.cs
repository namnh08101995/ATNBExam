using ATNB.Model;

namespace ATNB.Repository.Abstractions
{
    public interface IHelicopterRepository : IGenericRepository<Helicopter>
    {
        Helicopter GetById(string id);
    }
}
