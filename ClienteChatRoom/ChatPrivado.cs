using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChatRoom
{
    public partial class ChatPrivado : Form
    {
        private TcpClient client;
        private string nick_meu;
        private string nick_outro;

        public void Set_Client(TcpClient client) => this.client = client;
        public void Set_NickMeu(string meu) => this.nick_meu = meu;
        public void Set_NickOutro(string outro) => this.nick_outro = outro;
        public TcpClient Get_Client() => this.client;
        public string Get_NickMeu() => this.nick_meu;
        public string Get_NickOutro() => this.nick_outro;

        public ChatPrivado(TcpClient t, string s, string g)
        {
            Set_Client(t);
            Set_NickMeu(s);
            Set_NickOutro(g);

            InitializeComponent();
        }

        private void OuveServidor_PV()
        {
            NetworkStream str = this.client.GetStream();
            byte[] data = new byte[4096];
            string buffer = "";

            while (true)
            {
                try
                {
                    int bytes = str.Read(data, 0, data.Length);
                    string msg = Encoding.UTF8.GetString(data, 0, bytes);
                    buffer += msg;

                    string[] mensagem = buffer.Split('|');

                    for(int i = 0; i < mensagem.Length; i++)
                    {
                        string mensag = mensagem[i];

                        System.Diagnostics.Debug.WriteLine("ChatPrivado recebeu: '" + mensag + "'");

                        if (mensag.StartsWith("MSG:"))
                        {
                            this.Invoke(new Action(() =>
                            {
                                string[] partes = mensag.Replace("MSG:", "").Split(':');
                                string nick = partes[0];
                                string txt = partes[1];

                                richTextBox2.AppendText(nick + ": " + txt + "\n");
                            }));
                        }
                        
                    }

                    buffer = mensagem[mensagem.Length - 1];
                }
                catch
                {
                    return;
                }
            }
        }

        private void ChatPrivado_Load(object sender, EventArgs e)
        {
            Thread Escutando = new Thread(OuveServidor_PV);

            Escutando.IsBackground = true;
            Escutando.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        //----------- enviar mensagem -----------//
        private void button1_Click(object sender, EventArgs e)
        {
            string mensagem = "MSG:" + nick_meu + ":" + textBox1.Text + "|";
            byte[] bt = Encoding.UTF8.GetBytes(mensagem);
            client.GetStream().Write(bt, 0, bt.Length);

            textBox1.Text = string.Empty;
        }
    }
}
