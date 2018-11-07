using ATNB.Model;
using ATNB.Repository.Abstractions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ATNB.Repository
{
    public class AirPlaneRepository : GenericRepository<AirPlane>, IAirPlaneRepository
    {
        public AirPlaneRepository(DbContext context) : base(context)
        {

        }

        public override IEnumerable<AirPlane> GetAll()
        {
            return _entities.Set<AirPlane>().Include(x => x.AirPort).AsEnumerable();
        }

        public AirPlane GetById(string id)
        {
            return _dbset.Include(x => x.AirPort).Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
