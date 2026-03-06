<div align="center">

# 🌌 TimeSpace 
**Yapay Zeka Destekli, N-Tier Mimarili İnteraktif Hikaye Platformu**

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MSSQL](https://img.shields.io/badge/MS_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-68217A?style=for-the-badge&logo=nuget&logoColor=white)
![Stable Diffusion](https://img.shields.io/badge/Stable_Diffusion_API-000000?style=for-the-badge&logo=artificial-intelligence&logoColor=white)
![DevExpress](https://img.shields.io/badge/DevExpress-FF7200?style=for-the-badge&logo=devexpress&logoColor=white)

> *"Gelişmiş veri yönetimi ve üretken yapay zeka ile tarihi yeniden yazın."*

</div>

---

## 📖 Proje Özeti
**TimeSpace**, kullanıcıların tarihi dönemler (Orta Çağ, Gelecek vb.) ve karakter rolleri seçerek kendi interaktif hikayelerini oluşturduğu bir masaüstü (Windows Forms) uygulamasıdır. 

Projenin temel amacı, kullanıcı etkileşimlerinden elde edilen verileri anlamlı bir **Prompt (İstek Metni)** haline getirip **Stable Diffusion API**'sine iletmek ve dönen sonuçları asenkron olarak kullanıcı arayüzünde görselleştirmektir. Proje, profesyonel yazılım geliştirme standartları göz önünde bulundurularak **Katmanlı Mimari (N-Tier Architecture)** ile geliştirilmiştir.

---

## 🏗️ Proje Yapısı ve Katmanlı Mimari (N-Tier)

Sistemin sürdürülebilir, modüler ve test edilebilir olması için "Separation of Concerns" prensibi uygulanmış ve proje 4 ana katmana bölünmüştür:

### 1. 📦 TimeSpace.Entities (Varlık Katmanı)
Veritabanı tablolarının C# tarafındaki nesne (POCO - Plain Old CLR Object) karşılıklarını barındırır.
* **Sorumluluğu:** Sadece veriyi taşımak. İçerisinde hiçbir iş kuralı veya veritabanı bağlantı kodu bulunmaz.
* **Avantajı:** Diğer katmanların (UI, Business, DataAccess) ortak dilidir ve döngüsel bağımlılığı (Circular Dependency) engeller.

### 2. 🗄️ TimeSpace.DataAccess (Veri Erişim Katmanı)
Veritabanı ile uygulamanın konuştuğu katmandır. **Entity Framework (Database-First)** yaklaşımı kullanılmıştır.
* **`Model1.edmx`:** SQL Server üzerindeki `ChronoTaleDB` veritabanının haritalandırıldığı (mapping) yapı.
* **Bağlantı Yönetimi:** CRUD (Create, Read, Update, Delete) işlemleri için veritabanı context'ini oluşturur ve yönetir.

### 3. 🧠 TimeSpace.Business (İş Katmanı)
Uygulamanın "beynidir". Arayüzden gelen istekler veritabanına veya API'ye gitmeden önce burada işlenir, filtrelenir ve kurallara tabi tutulur.
* **`DMLManager.cs`:** Veri manipülasyon (Data Manipulation Language) işlemlerinin merkezi yöneticisidir. Veritabanı kayıt, silme ve güncelleme operasyonları buradan koordine edilir.
* **`SKullanici.cs`:** Kullanıcı giriş (Login), kayıt ve yetkilendirme gibi kullanıcı tabanlı iş kurallarını (Business Logic) denetleyen özel servis sınıfıdır.

### 4. 🖥️ TimeSpace (Sunum/Arayüz Katmanı - UI)
Kullanıcının etkileşime girdiği masaüstü formlarıdır. Kod kirliliğini önlemek için iş kuralları içermez; sadece Business katmanındaki yöneticileri çağırır.
* **DevExpress:** Arayüzün modern ve karanlık temalı (Skin/Office 2019 Black) görünmesi için kullanılmıştır.
* **Asenkron İşlemler:** Kullanıcı deneyimini (UX) kesintiye uğratmamak adına `async` ve `await` anahtar kelimeleriyle tasarlanmıştır.

---

## 🤖 Yapay Zeka Entegrasyonu (Stable Diffusion)

Projenin en kritik modüllerinden biri, metinden görsel üreten (Text-to-Image) yapay zeka entegrasyonudur.
* **RESTful İletişim:** C# `HttpClient` sınıfı kullanılarak Stable Diffusion API'sine HTTP POST istekleri atılır.
* **Veri Formatı:** Kullanıcının hikaye verileri **JSON** formatına serileştirilerek (`JsonConvert`) API'ye iletilir.
* **Base64 Decode:** API'den dönen karmaşık görsel verisi, arka planda tekrar Bitmap imajlara dönüştürülerek arayüzdeki `PictureBox` üzerinde gösterilir.

---

## 🗃️ Veritabanı Şeması (ChronoTaleDB)

Sistem, ilişkisel veritabanı (RDBMS) standartlarına uygun olarak tasarlanmıştır. Tablolar arası **One-to-Many (Bire-Çok)** ilişkiler ve **Navigation Properties** aktif olarak kullanılmıştır.

* **`Tbl_Kullanicilar`:** Sisteme giriş yapan kullanıcıların şifreli bilgilerini tutar.
* **`Tbl_Donemler`:** Hikayelerin geçebileceği çağların (Orta Çağ, Antik Çağ vb.) referans tablosudur.
* **`Tbl_Karakterler`:** Seçilebilir karakterlerin özelliklerini barındırır.
* **`Tbl_Hikayeler`:** Kullanıcı ID, Dönem ID ve Karakter ID'lerini Foreign Key (FK) olarak bağlayan, üretilen hikaye metnini ve **görselin dosya yolunu (Path)** tutan ana işlem tablosudur. *(Not: Performans optimizasyonu için görseller veritabanında değil, dosya sisteminde tutulur.)*

---
> ⚠️ **ÖNEMLİ (API KEY)** > Projenin GitHub reposunda güvenlik politikaları gereği **Stable Diffusion API Anahtarı** gizlenmiştir. 
> Görsel üretim modülünün çalışabilmesi için:
> 1. Proje dizinindeki **`YapayZekaServisi.cs`** dosyasını açın.
> 2. İlgili değişkene (`"BURAYA_API_ANAHTARI_GELECEK"` şeklinde bırakılan alana) **kendi Stable Diffusion API anahtarınızı** ekleyin.
