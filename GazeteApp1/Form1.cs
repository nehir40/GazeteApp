using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using GazeteApp1.Models;
using GazeteApp1.Services;

namespace GazeteApp1
{
    public partial class GazeteApp : Form
    {
        private readonly HaberService _haberService = new HaberService();

        public GazeteApp()
        {
            InitializeComponent();

            // Form ayarları
            this.Text = "GazeteApp";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Gazete seçenekleri
            gazetCombo.Items.AddRange(new string[]
            {
            "Tüm Gazeteler", "Hürriyet", "CNN Türk", "Milliyet", "Sözcü", "Sabah"
            });
            gazetCombo.SelectedIndex = 0;
            gazetCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Kategori seçenekleri
            kategoriCombo.Items.AddRange(new string[]
            {
                "spor", "teknoloji", "ekonomi", "siyaset"
            });
            kategoriCombo.SelectedIndex = 0;
            kategoriCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Buton ayarları
            araBtn.FlatStyle = FlatStyle.Flat;
            araBtn.FlatAppearance.BorderSize = 0;
            araBtn.Cursor = Cursors.Hand;
            araBtn.Click += AraBtn_Click;

            // ProgressBar ayarları
            yükleniyorBar.Style = ProgressBarStyle.Marquee;
            yükleniyorBar.Visible = false;

            // HaberPanel ayarları
            haberPanel.AutoScroll = true;
            haberPanel.BackColor = Color.WhiteSmoke;

            // Durum label
            durumLabel.Text = "Hazır — Haber getirmek için filtre seçin.";
            
        }

        private async void AraBtn_Click(object sender, EventArgs e)
        {
            string gazete = gazetCombo.SelectedItem?.ToString() ?? "Tüm Gazeteler";
            string kategori = kategoriCombo.SelectedItem?.ToString() ?? "spor";

            araBtn.Enabled = false;
            yükleniyorBar.Visible = true;
            durumLabel.Text = "Haberler yükleniyor...";
            haberPanel.Controls.Clear();

            try

            {
                
                List<HaberModel> haberler;

                if (gazetCombo.SelectedIndex == 0)
                    haberler = await _haberService.TumGazetelerdenGetirAsync(kategori);
                else
                    haberler = await _haberService.HaberleriGetirAsync(gazete.ToLower(), kategori);

                if (haberler.Count == 0)
                {
                    var bosLabel = new Label();
                    bosLabel.Text = "Haber bulunamadı. İnternet bağlantınızı kontrol edin.";
                    bosLabel.Font = new Font("Segoe UI", 12);
                    bosLabel.ForeColor = Color.Gray;
                    bosLabel.AutoSize = true;
                    bosLabel.Margin = new Padding(20);
                    haberPanel.Controls.Add(bosLabel);
                    durumLabel.Text = "Haber bulunamadı.";
                }
                else
                {
                    foreach (var haber in haberler)
                    {
                        var kart = HaberKartiOlustur(haber);
                        haberPanel.Controls.Add(kart);
                    }
                    durumLabel.Text = $"{haberler.Count} haber yüklendi.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                durumLabel.Text = "Hata oluştu.";
            }
            finally
            {
                araBtn.Enabled = true;
                yükleniyorBar.Visible = false;
            }
        }

        private Panel HaberKartiOlustur(HaberModel haber)
        {
            var kart = new Panel();
            kart.Size = new Size(320, 430);
            kart.Margin = new Padding(8);
            kart.BackColor = Color.White;
            kart.BorderStyle = BorderStyle.FixedSingle;
            kart.Cursor = Cursors.Hand;

            // Hover efekti
            kart.MouseEnter += (s, e) => kart.BackColor = Color.FromArgb(245, 245, 255);
            kart.MouseLeave += (s, e) => kart.BackColor = Color.White;

            // Resim
            var resimBox = new PictureBox();
            resimBox.Size = new Size(320, 170);
            resimBox.Location = new Point(0, 0);
            resimBox.SizeMode = PictureBoxSizeMode.Zoom;
            resimBox.BackColor = Color.LightGray;

            if (!string.IsNullOrEmpty(haber.ResimUrl))
            {
                try { resimBox.LoadAsync(haber.ResimUrl); }
                catch { }
            }

            // Kaynak etiketi
            var kaynakLabel = new Label();
            kaynakLabel.Text = haber.Kaynak.ToUpper();
            kaynakLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            kaynakLabel.ForeColor = Color.White;
            kaynakLabel.BackColor = Color.FromArgb(230, 57, 70);
            kaynakLabel.Location = new Point(10, 178);
            kaynakLabel.AutoSize = true;
            kaynakLabel.Padding = new Padding(4, 2, 4, 2);

            // Başlık
            var baslikLabel = new Label();
            baslikLabel.Text = haber.Baslik;
            baslikLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            baslikLabel.Location = new Point(10, 210);
            baslikLabel.Size = new Size(300, 80);
            baslikLabel.AutoEllipsis = true;

            // Özet
            var ozetLabel = new Label();
            ozetLabel.Text = haber.Ozet;
            ozetLabel.Font = new Font("Segoe UI", 9);
            ozetLabel.ForeColor = Color.Gray;
            ozetLabel.Location = new Point(10, 300);
            ozetLabel.Size = new Size(300, 60);
            ozetLabel.AutoEllipsis = true;

            // Tarih
            var tarihLabel = new Label();
            tarihLabel.Text = haber.TarihMetni;
            tarihLabel.Font = new Font("Segoe UI", 8);
            tarihLabel.ForeColor = Color.Silver;
            tarihLabel.Location = new Point(10, 375);
            tarihLabel.AutoSize = true;

            // Devamını oku
            var linkBtn = new LinkLabel();
            linkBtn.Text = "Devamını Oku →";
            linkBtn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            linkBtn.Location = new Point(200, 373);
            linkBtn.AutoSize = true;
            linkBtn.Tag = haber.HaberUrl;
            linkBtn.LinkClicked += (s, e) =>
            {
                try
                {
                    string url = linkBtn.Tag?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(url) && url.StartsWith("http"))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Bu haber için geçerli bir link bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Link açılamadı: " + ex.Message);
                }
            };

            kart.Controls.AddRange(new Control[]
            {
                resimBox, kaynakLabel, baslikLabel,
                ozetLabel, tarihLabel, linkBtn
            });

            return kart;
        }
        private void araBtn_Click(object sender, EventArgs e)
        {
            AraBtn_Click(sender, e);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void durumLabel_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}