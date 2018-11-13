using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace ATNB.Model
{
    public class ATNBContext : DbContext
    {
        public ATNBContext() : base("Name=ATNBContext")
        { 
        }

        public DbSet<AirPort> AirPorts { get; set; }
        public DbSet<AirPlane> AirPlanes { get; set; }
        public DbSet<Helicopter> Helicopters { get; set; }


        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }

    }
}
