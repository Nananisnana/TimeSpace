using System;
using System.Data.Entity;
using System.Linq;
using TimeSpace.DataAccess; // Veritabanı modelinin olduğu yer

namespace TimeSpace.Business
{
    public static class DMLManager
    {
        // GENERIC EKLEME METODU
        // T: Hangi tablo gelirse gelsin (Hikaye, Kullanıcı, Dönem...) kabul eder.
        public static void Ekle<T>(T veri) where T : class
        {
            using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
            {
                db.Entry(veri).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        // GENERIC GÜNCELLEME METODU
        public static void Guncelle<T>(T veri) where T : class
        {
            using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
            {
                db.Entry(veri).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GENERIC SİLME METODU
        public static void Sil<T>(T veri) where T : class
        {
            using (ChronoTaleDBEntities db = new ChronoTaleDBEntities())
            {
                db.Entry(veri).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}