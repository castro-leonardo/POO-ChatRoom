using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChatRoom
{
    public partial class Saguao : Form
    {
        private TcpClient _tcpClient;
        private string nickName;
        public Saguao(TcpClient client, string nick)
        {
            InitializeComponent();
            this._tcpClient = client;
            this.nickName = nick;
        }

        private void Saguao_Load(object sender, EventArgs e)
        {

            //--------- thread que ouve o server continuamente ----------//
            Thread _OuveServidor = new Thread(OuveServidor);

            _OuveServidor.IsBackground = true;
            _OuveServidor.Start();
        }

        private void OuveServidor()
        {
            //------------ cria a stream pra ouvir ----------//
            NetworkStream str = this._tcpClient.GetStream();
            byte[] data = new byte[1024];

            //--------- while pra ficar ouvindo o server continuamente ------------//
            while (true)
            {
                try
                {

                    //--------- para ler a mensagem ---------//
                    int bytes = str.Read(data, 0, data.Length);
                    string message = Encoding.UTF8.GetString(data, 0, bytes);

                    //----- aqui preciso categorizar qual o tipo de mensagem -----//
                }
                catch
                {
                    break;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //------------ primeiro eu tenho q saber a mensagem --------------//
            string msg = "MSG:"+nickName+":"+txtMensagem.Text;

            //----------- stream pra mandar a mensagem -----------//
            byte[] data = Encoding.UTF8.GetBytes(msg); //encoda
            _tcpClient.GetStream().Write(data, 0, data.Length);

            //---- esvazio a txtbox ----//
            txtMensagem.Text = string.Empty;
        }
    }
}
