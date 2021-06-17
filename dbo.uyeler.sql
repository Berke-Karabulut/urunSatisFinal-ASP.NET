CREATE TABLE [dbo].[uyeler] (
    [uyeId]      NVARCHAR (50) NOT NULL,
    [uyeKullaniciAdi] NVARCHAR (50) NOT NULL,
    [uyeMail]    NVARCHAR (50) NOT NULL,
    [uyeParola]  NVARCHAR (50) NOT NULL,
    [uyeAdSoyad]    NVARCHAR(50)           NOT NULL,
    [uyeAdmin] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([uyeId] ASC)
);

