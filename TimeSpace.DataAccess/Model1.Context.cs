
namespace TimeSpace.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ChronoTaleDBEntities : DbContext
    {
        public ChronoTaleDBEntities()
            : base("name=ChronoTaleDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Donemler> Tbl_Donemler { get; set; }
        public virtual DbSet<Tbl_Hikayeler> Tbl_Hikayeler { get; set; }
        public virtual DbSet<Tbl_HikayeOzellikleri> Tbl_HikayeOzellikleri { get; set; }
        public virtual DbSet<Tbl_KarakterOzellikleri> Tbl_KarakterOzellikleri { get; set; }
        public virtual DbSet<Tbl_Kullanicilar> Tbl_Kullanicilar { get; set; }
        public virtual DbSet<Tbl_Karakterler> Tbl_Karakterler { get; set; }
    }
}
