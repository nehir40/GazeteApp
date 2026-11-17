namespace GazeteApp1
{
    partial class GazeteApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            yükleniyorBar = new ProgressBar();
            araBtn = new Button();
            kategoriCombo = new ComboBox();
            label3 = new Label();
            gazetCombo = new ComboBox();
            label2 = new Label();
            haberPanel = new FlowLayoutPanel();
            statusStrip1 = new StatusStrip();
            durumLabel = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.MidnightBlue;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(1058, 70);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.MidnightBlue;
            label1.Font = new Font("Showcard Gothic", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(432, 3);
            label1.Name = "label1";
            label1.Size = new Size(239, 43);
            label1.TabIndex = 0;
            label1.Text = "-GazeteApp-";
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(yükleniyorBar);
            panel2.Controls.Add(araBtn);
            panel2.Controls.Add(kategoriCombo);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(gazetCombo);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 53);
            panel2.Name = "panel2";
            panel2.Size = new Size(1058, 43);
            panel2.TabIndex = 1;
            // 
            // yükleniyorBar
            // 
            yükleniyorBar.Location = new Point(745, 5);
            yükleniyorBar.Name = "yükleniyorBar";
            yükleniyorBar.Size = new Size(125, 29);
            yükleniyorBar.TabIndex = 8;
            // 
            // araBtn
            // 
            araBtn.BackColor = Color.Crimson;
            araBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            araBtn.Location = new Point(576, 6);
            araBtn.Name = "araBtn";
            araBtn.Size = new Size(150, 30);
            araBtn.TabIndex = 7;
            araBtn.Text = "🔍 Ara";
            araBtn.UseVisualStyleBackColor = false;
            araBtn.Click += AraBtn_Click;
            // 
            // kategoriCombo
            // 
            kategoriCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            kategoriCombo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            kategoriCombo.FormattingEnabled = true;
            kategoriCombo.Location = new Point(379, 7);
            kategoriCombo.Name = "kategoriCombo";
            kategoriCombo.Size = new Size(151, 28);
            kategoriCombo.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            label3.Location = new Point(304, 10);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 5;
            label3.Text = "Kategori:";
            // 
            // gazetCombo
            // 
            gazetCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            gazetCombo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            gazetCombo.FormattingEnabled = true;
            gazetCombo.Location = new Point(89, 7);
            gazetCombo.Name = "gazetCombo";
            gazetCombo.Size = new Size(151, 28);
            gazetCombo.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            label2.Location = new Point(25, 10);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 3;
            label2.Text = "Gazete:";
            // 
            // haberPanel
            // 
            haberPanel.AutoScroll = true;
            haberPanel.BackColor = Color.Gainsboro;
            haberPanel.BorderStyle = BorderStyle.Fixed3D;
            haberPanel.Location = new Point(12, 95);
            haberPanel.Name = "haberPanel";
            haberPanel.Size = new Size(1058, 538);
            haberPanel.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.None;
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { durumLabel });
            statusStrip1.Location = new Point(418, 618);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(168, 26);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // durumLabel
            // 
            durumLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            durumLabel.Name = "durumLabel";
            durumLabel.Size = new Size(151, 20);
            durumLabel.Text = "toolStripStatusLabel1";
            // 
            // GazeteApp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1082, 653);
            Controls.Add(statusStrip1);
            Controls.Add(haberPanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "GazeteApp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Button araBtn;
        private ComboBox kategoriCombo;
        private Label label3;
        private ComboBox gazetCombo;
        private Label label2;
        private ProgressBar yükleniyorBar;
        private FlowLayoutPanel haberPanel;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel durumLabel;
    }
}
