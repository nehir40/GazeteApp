# GazeteApp

Turk gazetelerinden haberleri kategoriye gore ceken bir Windows Forms uygulamasi.

## Ozellikler

- Coklu Gazete Destegi: Hurriyet, CNN Turk, Milliyet, Sozcu, Sabah
- Kategori Filtreleme: Spor, Teknoloji, Ekonomi, Siyaset
- Haber Kartlari: Her haber icin resim, baslik, ozet ve tarih bilgisi
- Haber Linki: Devamini Oku butonu ile haberin orijinal sayfasina yonlendirme
- Tum Gazeteler: Tek tikla tum gazetelerden ayni kategoride haber getirme
- Yukleniyorum Animasyonu: Haberler yuklenirken ProgressBar gostergesi

## Teknolojiler

| Teknoloji     | Kullanim                  |
|---------------|---------------------------|
| C# / .NET     | Ana programlama dili      |
| Windows Forms | Masaustu arayuz           |
| HtmlAgilityPack | Web scraping (haber cekme) |
| Async/Await   | Asenkron haber yukleme    |

## Kurulum

1. Repoyu klonla:
   git clone https://github.com/nehir40/GazeteApp.git

2. Visual Studio ile GazeteApp1.slnx dosyasini ac
3. Projeyi derle ve calistir (F5)

## Kullanim

1. Gazete secin (veya "Tum Gazeteler" birakin)
2. Kategori secin (Spor / Teknoloji / Ekonomi / Siyaset)
3. Ara butonuna tiklayin
4. Cikan haber kartlarindan Devamini Oku ile habere gidin

## Gereksinimler

- .NET 10.0
- Windows isletim sistemi
- Internet baglantisi
