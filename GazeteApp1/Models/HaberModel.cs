using System;

namespace GazeteApp1.Models
{
    public class HaberModel
    {
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Kaynak { get; set; }
        public string Kategori { get; set; }
        public string ResimUrl { get; set; }
        public string HaberUrl { get; set; }
        public DateTime Tarih { get; set; }

        public string TarihMetni
        {
            get
            {
                var fark = DateTime.Now - Tarih;
                if (fark.TotalMinutes < 60)
                    return $"{(int)fark.TotalMinutes} dakika önce";
                else if (fark.TotalHours < 24)
                    return $"{(int)fark.TotalHours} saat önce";
                else
                    return Tarih.ToString("dd MMMM yyyy");
            }
        }
    }
}
