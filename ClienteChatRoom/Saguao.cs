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
        private bool escutandoSaguao = true;
        private bool fechadoPeloServidor = false;
        public Saguao(TcpClient client, string nick)
        {
            InitializeComponent();
            this._tcpClient = client;
            this.nickName = nick;
            label2.Text = nick;
            this.KeyPreview = true;
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
            byte[] data = new byte[4096];
            string bffr = "";

            //--------- while pra ficar ouvindo o server continuamente ------------//
            while (true)
            {
                try
                {
                    //--- caso nao teja escutando pula rapidinho ------//
                    if (!escutandoSaguao)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    //--------- para ler a mensagem ---------//
                    int bytes = str.Read(data, 0, data.Length);
                    string messag = Encoding.UTF8.GetString(data, 0, bytes);
                    bffr += messag;

                    //-------- separa as mensagens por split ----------//
                    string[] mensagens = bffr.Split('|');

                    //------- pra cada parte do vetor de string, ele analisa o que tem q fazer -----//
                    for (int i = 0; i < mensagens.Length - 1; i++)
                    {
                        string message = mensagens[i];

                        // Ignora mensagens vazias ou brancas
                        if (string.IsNullOrWhiteSpace(message))
                            continue;

                        if (message.StartsWith("LIST:"))
                        {
                            if (escutandoSaguao && this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.Invoke(new Action(() => usersOnline(message)));
                            }
                        }

                        else if (message.StartsWith("MSG:"))
                        {
                            if (!escutandoSaguao || !this.IsHandleCreated || this.IsDisposed)
                                continue;

                            this.Invoke(new Action(() =>
                            {
                                if (!escutandoSaguao) return;

                                string[] partes = message.Replace("MSG:", "").Split(':');
                                if (partes.Length < 2)
                                    return;

                                string nick = partes[0];
                                string texto = partes[1];
                                if(nick == nickName)
                                {
                                    richTextBox1.SelectionColor = Color.DarkOrange;
                                } else
                                {
                                    richTextBox1.SelectionColor = Color.Black;
                                }
                                richTextBox1.AppendText(nick + ": " + texto + "\n");
                                richTextBox1.ScrollToCaret();
                            }));
                        }

                        else if (message.StartsWith("INVITE:"))
                        {
                            if (escutandoSaguao && this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.Invoke(new Action(() => mostrarConvite(message)));
                            }
                        }

                        else if (message.StartsWith("ACCEPT:"))
                        {

                            //------- caso o bate papo privado seja aceito --------//
                            string[] partes = message.Replace("ACCEPT:", "").Split(':');
                            if (partes.Length < 1)
                                continue;

                            string convidou = partes[0];
                            bool c = false;

                            //-------- preciso do invoke pq o OuveServidor ta numa thread secundaria ---------//
                            if (escutandoSaguao && this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    if (!escutandoSaguao) return;

                                    ChatPrivado chat = new ChatPrivado(_tcpClient, nickName, convidou);

                                    escutandoSaguao = false;
                                    this.Hide();
                                    chat.ShowDialog();

                                    this.Show();
                                    escutandoSaguao = true;
                                    c = true;
                                }));
                            }

                            if(c == true)
                            {
                                string aviso = "RETURN:" + nickName + "|";
                                byte[] bite = Encoding.UTF8.GetBytes(aviso);

                                _tcpClient.GetStream().Write(bite, 0, bite.Length);

                                if (escutandoSaguao && this.IsHandleCreated && !this.IsDisposed)
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        if (!escutandoSaguao) return;
                                        MessageBox.Show("Você saiu do castelinho com " + convidou);
                                    }));
                                }
                            }


                        }
                        else if (message.StartsWith("REFUSE:"))
                        {
                            //------- caso seja rejeitado ---------//
                            string[] partes = message.Replace("REFUSE:", "").Split(':');
                            if (partes.Length < 1)
                                continue;

                            string convidado = partes[0];

                            if (escutandoSaguao && this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    if (!escutandoSaguao) return;
                                    MessageBox.Show(convidado + " não quer nadar com você :(");
                                }));
                            }
                        } else if (message.StartsWith("JNET:"))
                        {
                            string texto = message.Replace("JNET:", "");
                            //user e mensagem

                            this.Invoke(new Action(() =>
                            {
                                if (!escutandoSaguao) return;
                                richTextBox1.SelectionColor = Color.Gray;

                                richTextBox1.AppendText(texto + "\n");
                                richTextBox1.ScrollToCaret();
                            }));
                        }

                    }

                    //------ pra nao processar mensagens incompletas, deixa a ultima pra prox leitura ------//
                    bffr = mensagens[mensagens.Length - 1];
                }
                catch
                {
                    break;
                }
            }

        }

        public void enviar(string mensagem)
        {
            //------------ primeiro eu tenho q saber a mensagem --------------//
            string msg = "MSG:" + nickName + ":" + mensagem + "|";

            //----------- stream pra mandar a mensagem -----------//
            byte[] data = Encoding.UTF8.GetBytes(msg); //encoda
            _tcpClient.GetStream().Write(data, 0, data.Length);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMensagem.Text)) return;//-- n manda mensagem vazia

            enviar(txtMensagem.Text);
            
            //---- esvazio a txtbox ----//
            txtMensagem.Text = string.Empty;
            txtMensagem.AcceptsReturn = false;
        }

        private void mostrarConvite(string convite)
        {
            //--------- tiro o cod da mensagem e dou um split -------------//
            string[] conv = convite.Replace("INVITE:", "").Split(':');
            if (conv.Length < 1)
                return;

            string convidou = conv[0];

            //--------- resultado do dialogo criado -------------//

            DialogResult result = MessageBox.Show(convidou + " quer pegar uma onda com você!", "Convite de chat", MessageBoxButtons.YesNo);

            //---------- se o resultado for positivo, crio um novo chat privado ------------//
            if(result == DialogResult.Yes)
            {
                //------- tem q avisar o outro user q aceitou ----------//
                string reply = "ACCEPT:" + nickName + ":" + convidou + "|";
                byte[] ar = Encoding.UTF8.GetBytes(reply);
                _tcpClient.GetStream().Write(ar, 0, ar.Length);

                ChatPrivado chat = new ChatPrivado(_tcpClient, nickName, convidou);

                escutandoSaguao = false;
                //---------- esconde esse form --------//
                this.Hide();

                //-------- mostra o chat pv sozinho ----------//
                chat.ShowDialog();


                //-------- qnd o pv eh fechado esse fica em foco -----//
                this.Show();
                escutandoSaguao = true;

            }
            else
            {
                string rp = "REFUSE:" + nickName + ":" + convidou + "|";
                byte[] arr = Encoding.UTF8.GetBytes(rp);
                _tcpClient.GetStream().Write(arr, 0, arr.Length);
            }
        }

        private void usersOnline(string usuariosOnline)
        {
            //-------------- ele cria um vetor de string onde tira a primeira partezinha de LIST: e depois da um split em cada virgulha -------//
            string[] users = usuariosOnline.Replace("LIST:", "").Split(',');

            listBox1.Items.Clear();

            //--------- percorre o vetor e adiciona o user na lista de users online --------//
            foreach (string user in users)
            {
                string userLimpo = user.Trim();

                if (!string.IsNullOrEmpty(userLimpo)) 
                {
                    if (user == nickName)
                    {
                        listBox1.Items.Insert(0, "♛ " + userLimpo); //-- Usuário sempre se ve como primeiro da lista
                    }
                    else
                    {
                        listBox1.Items.Add(userLimpo);
                    }
                }
            }
        }

        private void btnConvidar_Click(object sender, EventArgs e)
        {
            //----- caso o user n tenha selecionado ninguem ----//
            if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um usuário para teclar :p");
                return;
            }
            else if (listBox1.SelectedItem.ToString() == nickName)
            {
                MessageBox.Show("Ei! Você não pode convidar a si mesmo!");
                return;
            }
            else
            {
                //----- passa o user que foi selecionado da lista pra string -------//
                string selecionado = "INVITE:" + nickName + ":" + listBox1.SelectedItem.ToString() + "|";

                //------ manda pro servidor ------//
                byte[] data = Encoding.UTF8.GetBytes(selecionado);
                _tcpClient.GetStream().Write(data, 0, data.Length);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Saguao_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "JNET:" + nickName + ":" + nickName + " foi pescado..." + "|";

            //----------- stream pra mandar a mensagem -----------//
            byte[] data = Encoding.UTF8.GetBytes(msg); //encoda
            _tcpClient.GetStream().Write(data, 0, data.Length);
        }
    }
}
