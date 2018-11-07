using ATNB.Model;
using ATNB.Repository.Abstractions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ATNB.Repository
{
    public class HelicopterRepository : GenericRepository<Helicopter>, IHelicopterRepository
    {
        public HelicopterRepository(DbContext context) : base(context)
        {

        }

        public override IEnumerable<Helicopter> GetAll()
        {
            return _entities.Set<Helicopter>().Include(x => x.AirPort).AsEnumerable();
        }

        public Helicopter GetById(string id)
        {
            return _dbset.Include(x => x.AirPort).Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
