-- 1. ADIM: EKSİK SÜTUNLARI EKLEME
-- Önce bu kısım çalışacak ve sütunları oluşturacak.

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tbl_Kullanicilar' AND COLUMN_NAME = 'Email')
BEGIN
    ALTER TABLE Tbl_Kullanicilar ADD Email NVARCHAR(100);
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tbl_Kullanicilar' AND COLUMN_NAME = 'KullaniciTipi')
BEGIN
    ALTER TABLE Tbl_Kullanicilar ADD KullaniciTipi NVARCHAR(20) DEFAULT 'Uye';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tbl_Kullanicilar' AND COLUMN_NAME = 'Yas')
BEGIN
    ALTER TABLE Tbl_Kullanicilar ADD Yas INT;
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tbl_Kullanicilar' AND COLUMN_NAME = 'Cinsiyet')
BEGIN
    ALTER TABLE Tbl_Kullanicilar ADD Cinsiyet NVARCHAR(20);
END

-- BU KOMUT ÇOK ÖNEMLİ! İŞLEMİ İKİYE BÖLER.
GO 

-- 2. ADIM: ADMİN KULLANCISINI OLUŞTURMA
-- Artık sütunlar var olduğu için hata vermeyecek.

IF NOT EXISTS (SELECT * FROM Tbl_Kullanicilar WHERE KullaniciAdi = 'admin')
BEGIN
    INSERT INTO Tbl_Kullanicilar (KullaniciAdi, SifreHash, AdSoyad, Email, Yas, Cinsiyet, KullaniciTipi, KayitTarihi)
    VALUES ('admin', '1234', 'Yonetici Admin', 'admin@timespace.com', 25, 'Belirsiz', 'Admin', GETDATE());
END
ELSE
BEGIN
    -- Eğer admin zaten varsa yetkisini güncelle
    UPDATE Tbl_Kullanicilar SET KullaniciTipi = 'Admin' WHERE KullaniciAdi = 'admin';
END