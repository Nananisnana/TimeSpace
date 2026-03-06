

namespace TimeSpace.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Hikayeler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Hikayeler()
        {
            this.Tbl_HikayeOzellikleri = new HashSet<Tbl_HikayeOzellikleri>();
        }
    
        public int HikayeID { get; set; }
        public Nullable<int> KullaniciID { get; set; }
        public Nullable<int> DonemID { get; set; }
        public string Baslik { get; set; }
        public string HikayeMetni { get; set; }
        public string GorselYolu { get; set; }
        public Nullable<System.DateTime> OlusturmaTarihi { get; set; }
    
        public virtual Tbl_Donemler Tbl_Donemler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_HikayeOzellikleri> Tbl_HikayeOzellikleri { get; set; }
        public virtual Tbl_Kullanicilar Tbl_Kullanicilar { get; set; }
    }
}
