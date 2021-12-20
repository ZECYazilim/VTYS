
namespace VTYSOdev
{
    partial class DestekTalebi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picCaptha = new System.Windows.Forms.PictureBox();
            this.txtDogrulamaKodu = new System.Windows.Forms.TextBox();
            this.lblDogrulama = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.lblSorunTuru = new System.Windows.Forms.Label();
            this.cmbSorun = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptha)).BeginInit();
            this.SuspendLayout();
            // 
            // picCaptha
            // 
            this.picCaptha.Location = new System.Drawing.Point(275, 56);
            this.picCaptha.Name = "picCaptha";
            this.picCaptha.Size = new System.Drawing.Size(57, 38);
            this.picCaptha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCaptha.TabIndex = 33;
            this.picCaptha.TabStop = false;
            // 
            // txtDogrulamaKodu
            // 
            this.txtDogrulamaKodu.Location = new System.Drawing.Point(148, 56);
            this.txtDogrulamaKodu.Name = "txtDogrulamaKodu";
            this.txtDogrulamaKodu.Size = new System.Drawing.Size(121, 20);
            this.txtDogrulamaKodu.TabIndex = 26;
            // 
            // lblDogrulama
            // 
            this.lblDogrulama.AutoSize = true;
            this.lblDogrulama.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDogrulama.Location = new System.Drawing.Point(29, 59);
            this.lblDogrulama.Name = "lblDogrulama";
            this.lblDogrulama.Size = new System.Drawing.Size(113, 13);
            this.lblDogrulama.TabIndex = 32;
            this.lblDogrulama.Text = "*Doğrulama Kodu :";
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSend.Location = new System.Drawing.Point(94, 245);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(175, 23);
            this.btnSend.TabIndex = 30;
            this.btnSend.Text = "Bildirimi Gönder";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(32, 107);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(291, 132);
            this.richTextBox1.TabIndex = 29;
            this.richTextBox1.Text = "";
            // 
            // lblAciklama
            // 
            this.lblAciklama.AutoSize = true;
            this.lblAciklama.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAciklama.Location = new System.Drawing.Point(145, 91);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(66, 13);
            this.lblAciklama.TabIndex = 28;
            this.lblAciklama.Text = "Açıklama :";
            // 
            // lblSorunTuru
            // 
            this.lblSorunTuru.AutoSize = true;
            this.lblSorunTuru.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSorunTuru.Location = new System.Drawing.Point(4, 26);
            this.lblSorunTuru.Name = "lblSorunTuru";
            this.lblSorunTuru.Size = new System.Drawing.Size(138, 13);
            this.lblSorunTuru.TabIndex = 20;
            this.lblSorunTuru.Text = "* Lütfen sorun seçiniz :";
            // 
            // cmbSorun
            // 
            this.cmbSorun.FormattingEnabled = true;
            this.cmbSorun.Items.AddRange(new object[] {
            "1-user is already online errors.",
            "2-Stack errors.",
            "3-Database errors.",
            "4-Connection errors.",
            "5-Others errors."});
            this.cmbSorun.Location = new System.Drawing.Point(148, 23);
            this.cmbSorun.Name = "cmbSorun";
            this.cmbSorun.Size = new System.Drawing.Size(175, 21);
            this.cmbSorun.TabIndex = 24;
            // 
            // DestekTalebi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 280);
            this.Controls.Add(this.picCaptha);
            this.Controls.Add(this.txtDogrulamaKodu);
            this.Controls.Add(this.lblDogrulama);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.lblSorunTuru);
            this.Controls.Add(this.cmbSorun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DestekTalebi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DestekTalebi";
            this.Load += new System.EventHandler(this.DestekTalebi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCaptha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picCaptha;
        private System.Windows.Forms.TextBox txtDogrulamaKodu;
        private System.Windows.Forms.Label lblDogrulama;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblAciklama;
        private System.Windows.Forms.Label lblSorunTuru;
        private System.Windows.Forms.ComboBox cmbSorun;
    }
}