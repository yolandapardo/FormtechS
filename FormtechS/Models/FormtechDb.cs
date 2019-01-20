using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class FormtechDb : DbContext
    {
        public FormtechDb () : base("name=DefaultConnection") {  }
        public DbSet<Community> Communities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TypeOfSurvey> TypeOfSurveys { get; set; }

       
    }
}