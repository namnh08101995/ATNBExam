using ATNB.Model;
using System.Data.Entity;
using System.Linq;
using ATNB.Repository.Abstractions;

namespace ATNB.Repository
{
    public class AirPortRepository : GenericRepository<AirPort>, IAirPortRepository
    {
        public AirPortRepository(DbContext context) : base(context)
        {

        }

        public AirPort GetById(string id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}
