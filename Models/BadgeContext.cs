using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BadgeContext : DbContext
    {
        public BadgeContext() : base("Badges")
        {

        }
        public BadgeContext(string db = "Badges") : base(db)
        {

        }

        public DbSet<Badge> Badges { get; set; }
    }
}
