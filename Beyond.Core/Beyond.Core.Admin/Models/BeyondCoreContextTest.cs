using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Beyond.Core.Admin.Models
{
    public class BeyondCoreContextTest:DbContext
    {
        public BeyondCoreContextTest()
            : base("name=BeyondCore")
        { 
            
        }

        public DbSet<t_users> tb_users { get; set; }

        //public DbSet<t_orders> tb_orders { get; set; }

    }
}