using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using GazeteApp1.Models;

namespace GazeteApp1.Services
{
    public class HaberService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly Dictionary<string, string> _rssLinkleri = new Dictionary<string, string>
        {
            // Hürriyet
            { "hürriyet_spor",      "https://www.hurriyet.com.tr/rss/spor" },
            { "hürriyet_teknoloji", "https://www.hurriyet.com.tr/rss/teknoloji" },
            { "hürriyet_ekonomi",   "https://www.hurriyet.com.tr/rss/ekonomi" },
            { "hürriyet_siyaset",   "https://www.hurriyet.com.tr/rss/gundem" },

            // CNN Türk
            { "cnn türk_spor",      "https://www.cnnturk.com/feed/rss/spor" },
            { "cnn türk_teknoloji", "https://www.cnnturk.com/feed/rss/teknoloji" },
            { "cnn türk_ekonomi",   "https://www.cnnturk.com/feed/rss/ekonomi" },
            { "cnn türk_siyaset",   "https://www.cnnturk.com/feed/rss/gundem" },

            // Milliyet
            { "milliyet_spor",      "https://www.milliyet.com.tr/rss/rssNew/spornews.xml" },
            { "milliyet_teknoloji", "https://www.milliyet.com.tr/rss/rssNew/teknolojinews.xml" },
            { "milliyet_ekonomi",   "https://www.milliyet.com.tr/rss/rssNew/ekonominews.xml" },
            { "milliyet_siyaset",   "https://www.milliyet.com.tr/rss/rssNew/sondakikanews.xml" },

            // Sözcü
            { "sözcü_spor",         "https://www.sozcu.com.tr/feeds-rss-category-spor" },
            { "sözcü_teknoloji",    "https://www.sozcu.com.tr/feeds-haberler" },
            { "sözcü_ekonomi",      "https://www.sozcu.com.tr/feeds-rss-category-gundem" },
            { "sözcü_siyaset",      "https://www.sozcu.com.tr/feeds-son-dakika" },

            // Sabah
            { "sabah_spor",         "https://www.sabah.com.tr/rss/spor.xml" },
            { "sabah_teknoloji",    "https://www.sabah.com.tr/rss/teknoloji.xml" },
            { "sabah_ekonomi",      "https://www.sabah.com.tr/rss/ekonomi.xml" },
            { "sabah_siyaset",      "https://www.sabah.com.tr/rss/gundem.xml" },
        };

        public async Task<List<HaberModel>> HaberleriGetirAsync(string gazete, string kategori)
        {
            var haberler = new List<HaberModel>();
            string anahtar = $"{gazete.ToLower()}_{kategori.ToLower()}";

            if (!_rssLinkleri.ContainsKey(anahtar))
                return haberler;

            string rssUrl = _rssLinkleri[anahtar];

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                    client.Timeout = TimeSpan.FromSeconds(15);

                    string xmlMetin = await client.GetStringAsync(rssUrl);
                    XDocument xmlBelge = XDocument.Parse(xmlMetin);
                    XNamespace mediaNs = "http://search.yahoo.com/mrss/";
                    var itemler = xmlBelge.Descendants("item");

                    foreach (var item in itemler.Take(10))
                    {
                        var haber = new HaberModel();
                        haber.Baslik = item.Element("title")?.Value ?? "Başlık yok";
                        string hamOzet = item.Element("description")?.Value ?? "";
                        haber.Ozet = HtmlTemizle(hamOzet);
                        string link = item.Element("link")?.Value ?? "";
                        if (string.IsNullOrEmpty(link))
                            link = item.Element("guid")?.Value ?? "";
                        haber.HaberUrl = link.Trim();
                        string tarihMetni = item.Element("pubDate")?.Value ?? "";
                        if (DateTime.TryParse(tarihMetni, out DateTime tarih))
                            haber.Tarih = tarih;
                        else
                            haber.Tarih = DateTime.Now;
                        haber.Kaynak = gazete.ToUpper();
                        haber.Kategori = kategori;
                        haber.ResimUrl = ResimBul(item, mediaNs);
                        haberler.Add(haber);
                    }
                }
            }
        
            catch (Exception ex)
            {
                MessageBox.Show($"RSS Hatası: {ex.Message}");
            }

            return haberler;
        }

        public async Task<List<HaberModel>> TumGazetelerdenGetirAsync(string kategori)
        {
            var tumHaberler = new List<HaberModel>();
            var gazeteler = new[] { "hürriyet", "cnn türk", "milliyet", "sözcü", "sabah" };

            foreach (var gazete in gazeteler)
            {
                var liste = await HaberleriGetirAsync(gazete, kategori);
                tumHaberler.AddRange(liste);
            }

            return tumHaberler.OrderByDescending(h => h.Tarih).ToList();
        }

        private string HtmlTemizle(string html)
        {
            if (string.IsNullOrEmpty(html)) return "";
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            string temiz = doc.DocumentNode.InnerText;
            temiz = System.Text.RegularExpressions.Regex.Replace(temiz, @"\s+", " ");
            return temiz.Trim();
        }

        private string ResimBul(XElement item, XNamespace mediaNs)
        {
            // Yöntem 1: <media:content url="..."/>
            var mediaContent = item.Element(mediaNs + "content");
            if (mediaContent != null)
            {
                string url = mediaContent.Attribute("url")?.Value ?? "";
                if (!string.IsNullOrEmpty(url)) return url;
            }

            // Yöntem 2: <media:thumbnail url="..."/>
            var mediaThumbnail = item.Element(mediaNs + "thumbnail");
            if (mediaThumbnail != null)
            {
                string url = mediaThumbnail.Attribute("url")?.Value ?? "";
                if (!string.IsNullOrEmpty(url)) return url;
            }

            // Yöntem 3: <enclosure url="..." type="image/..."/>
            var enclosure = item.Element("enclosure");
            if (enclosure != null)
            {
                string tip = enclosure.Attribute("type")?.Value ?? "";
                if (tip.StartsWith("image"))
                    return enclosure.Attribute("url")?.Value ?? "";
            }

            // Yöntem 4: description içinde <img src="..."/>
            string desc = item.Element("description")?.Value ?? "";
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(desc);
            var imgNode = htmlDoc.DocumentNode.SelectSingleNode("//img");
            if (imgNode != null)
            {
                string src = imgNode.GetAttributeValue("src", "");
                if (!string.IsNullOrEmpty(src)) return src;
            }

            // Yöntem 5: description içinde url ile biten jpg/png/jpeg
            var matches = System.Text.RegularExpressions.Regex.Match(
                desc, @"https?://[^\s""']+\.(jpg|jpeg|png|gif|webp)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (matches.Success) return matches.Value;

            return "";
        }
    }
}
