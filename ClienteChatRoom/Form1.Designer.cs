namespace ClienteChatRoom
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblNickname = new System.Windows.Forms.Label();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pcbLogin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNickname
            // 
            this.lblNickname.AutoSize = true;
            this.lblNickname.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNickname.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNickname.Location = new System.Drawing.Point(77, 308);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(79, 20);
            this.lblNickname.TabIndex = 1;
            this.lblNickname.Text = "Fishname:";
            // 
            // txtNickname
            // 
            this.txtNickname.Location = new System.Drawing.Point(81, 331);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(221, 22);
            this.txtNickname.TabIndex = 2;
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.Color.SandyBrown;
            this.btnConectar.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.Location = new System.Drawing.Point(96, 383);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(190, 34);
            this.btnConectar.TabIndex = 5;
            this.btnConectar.Text = "PULAR NO AQUÁRIO";
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::ClienteChatRoom.Properties.Resources.gfishnet;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(105, 44);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pcbLogin
            // 
            this.pcbLogin.BackColor = System.Drawing.Color.Transparent;
            this.pcbLogin.BackgroundImage = global::ClienteChatRoom.Properties.Resources.sealife_8;
            this.pcbLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbLogin.Location = new System.Drawing.Point(12, 53);
            this.pcbLogin.Name = "pcbLogin";
            this.pcbLogin.Size = new System.Drawing.Size(359, 228);
            this.pcbLogin.TabIndex = 0;
            this.pcbLogin.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(383, 450);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtNickname);
            this.Controls.Add(this.lblNickname);
            this.Controls.Add(this.pcbLogin);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Net.GoldFish";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbLogin;
        private System.Windows.Forms.Label lblNickname;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnConectar;
    }
}

