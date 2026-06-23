using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
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
            InitializeComponent();

            Set_Client(t);
            Set_NickMeu(s);
            Set_NickOutro(g);

            label2.Text = s;
            label3.Text = "Chat Privado Com " + g;
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

                                if (nick == nick_meu)
                                {
                                    richTextBox2.SelectionColor = Color.Orange;
                                }
                                else
                                {
                                    richTextBox2.SelectionColor = Color.Black;
                                }

                                richTextBox2.AppendText(nick + ": " + txt + "\n");
                                richTextBox2.ScrollToCaret();
                            }));
                        }
                        else if (mensag.StartsWith("RETURN:"))
                        {
                            //------- caso outro user saia --------//
                            string[] partes = mensag.Replace("RETURN:", "").Split(':');
                            string saiu = partes[0];

                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show(saiu + "Saiu do seu castelinho :( ");
                                this.Close();
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

       private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "RETURN:" + nick_meu + "|";
            byte[] bt = Encoding.UTF8.GetBytes(msg);
            client.GetStream().Write(bt, 0, bt.Length);
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
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }

        }
    }
}
