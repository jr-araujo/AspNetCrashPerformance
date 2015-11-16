using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCrashPerformance.Models
{
    public class EFDBContext : DbContext
    {
        public EFDBContext() :
            base("name=EFDBContext")
        {
            Database.SetInitializer<EFDBContext>(null);

            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.UseDatabaseNullSemantics = true;

            this.Database.Log = Console.Write;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Produto> Produtos { get; set; }
    }
}
