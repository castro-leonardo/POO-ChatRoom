namespace ClienteChatRoom
{
    partial class Saguao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Saguao));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConvidar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(242)))), ((int)(((byte)(223)))));
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox1.Location = new System.Drawing.Point(12, 63);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(584, 317);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(12, 386);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(502, 52);
            this.txtMensagem.TabIndex = 2;
            this.txtMensagem.Enter += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(618, 109);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(170, 293);
            this.listBox1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Orange;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(618, 82);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(170, 25);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "ONLINE:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ClienteChatRoom.Properties.Resources._4ILKIBJ265HWLSFGA6Q4LOZMTJHPZGMI;
            this.pictureBox3.Location = new System.Drawing.Point(602, -6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(197, 91);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ClienteChatRoom.Properties.Resources.gfishnet;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 54);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btnConvidar
            // 
            this.btnConvidar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnConvidar.BackgroundImage = global::ClienteChatRoom.Properties.Resources.Goldfish_Download_Free_PNG;
            this.btnConvidar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConvidar.Font = new System.Drawing.Font("Segoe UI", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvidar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnConvidar.Location = new System.Drawing.Point(618, 408);
            this.btnConvidar.Name = "btnConvidar";
            this.btnConvidar.Size = new System.Drawing.Size(170, 30);
            this.btnConvidar.TabIndex = 5;
            this.btnConvidar.Text = "CONVIDAR";
            this.btnConvidar.UseVisualStyleBackColor = false;
            this.btnConvidar.Click += new System.EventHandler(this.btnConvidar_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.NavajoWhite;
            this.button1.BackgroundImage = global::ClienteChatRoom.Properties.Resources.goldfish_PNG73;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(521, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 52);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Fishname:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(254, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Leo";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Saguao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnConvidar);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.pictureBox3);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Saguao";
            this.Text = "Aquário";
            this.Load += new System.EventHandler(this.Saguao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnConvidar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}