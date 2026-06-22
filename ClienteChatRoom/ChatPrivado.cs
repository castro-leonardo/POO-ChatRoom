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

        private void ChatPrivado_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
