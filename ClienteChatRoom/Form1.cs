using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChatRoom
{
    public partial class Form1 : Form
    {
        TcpClient cliente;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        private void btnConectar_Click(object sender, EventArgs e)
        {
            string fishname = txtNickname.Text;

            //------------ se ja tiver o nick escrito ---------//
            if (fishname != string.Empty)
            {
                //---------- tento conectar o cliente ----------//
                try
                {
                    cliente = new TcpClient();
                    cliente.Connect("127.0.0.1", 999);
                    NetworkStream stream = cliente.GetStream();
                    byte[] nick = Encoding.UTF8.GetBytes(fishname);
                    stream.Write(nick, 0, nick.Length);

                    txtNickname.ReadOnly = true;

                    //---------- se chegou aqui, é pq conectou ------------//
                    MessageBox.Show("Você chegou no aquário!");

                    //--------- apos se conectar, abre o ""saguao"" -----------//

                    //---- cria o form2 ----//
                    Saguao aq = new Saguao(cliente, fishname);

                    //----- fecha/esconde o form atual -----//
                    this.Hide();
                    aq.ShowDialog(); // --->> posso colocar como .Show, conferir dps ************/////////////////////////////////////////****************
                    this.Close();
                }
                catch
                {
                    //-------------- se nao conectar exibe isso ----------------//
                    MessageBox.Show("Não foi possível conectar ao servidor...");
                }

            }
            else
            {
                //---------- se o usuario nao inserir o nick continua aguardando por um nick para conectar -----------//
                MessageBox.Show("Por favor insira seu nome antes! :)");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConectar_Click(sender, e);
            }

        }
    }
}
