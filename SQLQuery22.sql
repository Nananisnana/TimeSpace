-- 1. Önce tablodaki her şeyi temizle (Sil)
DELETE FROM Tbl_Donemler;

-- 2. Sayacı sıfırla (Böylece bir sonraki kayıt 1 numarasını alacak)
DBCC CHECKIDENT ('Tbl_Donemler', RESEED, 0);

-- 3. Hazır temizlemişken verileri de doğrusuyla biz ekleyelim (Sen elle uğraşma)
INSERT INTO Tbl_Donemler (DonemAdi, Aciklama, AktifMi, BaslangicYili, BitisYili) VALUES
('Taş Devri Dönemi', 'İnsanlığın şafağı, avcı toplayıcılar ve ilk aletlerin keşfi.', 1, -100000, -3000),
('Antik Mısır Dönemi', 'Piramitlerin gölgesinde, firavunların ve tanrıların altın çağı.', 1, -3150, -30),
('Antik Yunan Dönemi', 'Filozofların, demokrasinin doğuşu ve efsanevi mitolojilerin diyarı.', 1, -800, -146),
('Antik Roma Dönemi', 'Gladyatörler, lejyonlar ve dünyaya hükmeden devasa bir imparatorluk.', 1, -753, 476),
('Antik Çin Dönemi', 'İpek Yolu, görkemli hanedanlıklar ve bilgeliğin doğuşu.', 1, -2100, 220),
('Bizans İmparatorluğu Dönemi', 'Konstantinopolis surları, entrikalar ve Doğu Roma ihtişamı.', 1, 330, 1453),
('İslam Altın Çağı Dönemi', 'Bağdat kütüphaneleri, bilim, tıp ve astronomide devrim yılları.', 1, 750, 1258),
('Orta Çağ Dönemi', 'Şövalyeler, kaleler, krallar ve karanlık efsaneler.', 1, 476, 1453),
('Moğol İmparatorluğu Dönemi', 'Bozkırın atlı okçuları ve dünyanın gördüğü en büyük bitişik imparatorluk.', 1, 1206, 1368),
('Osmanlı İmparatorluğu Dönemi', 'Üç kıtaya yayılan hakimiyet, Yeniçeriler ve muhteşem mimari.', 1, 1299, 1922),
('Rönesans Dönemi', 'Sanatın, bilimin ve insanlığın Avrupa''da yeniden doğuşu.', 1, 1400, 1600),
('Sanayi Devrimi Dönemi', 'Buharlı makineler, fabrikalar ve değişen dünyanın çarkları.', 1, 1760, 1840),
('Viktorya Dönemi', 'Sisli Londra sokakları, sanayi, dedektiflik ve şıklık.', 1, 1837, 1901),
('Viking Çağı Dönemi', 'Kuzeyin sert denizcileri, drakkar gemileri ve Valhalla inancı.', 1, 793, 1066);