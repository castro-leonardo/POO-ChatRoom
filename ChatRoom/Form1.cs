using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 MSG: mensagem
 INVITE:
 ACCEPT:
 REFUSE:
 LIST:
 */

namespace ChatRoom
{
    public partial class Form1 : Form
    {
        List<Cliente> list = new List<Cliente>();
        List<Privado> salas = new List<Privado>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int porta = 999;
            TcpListener server = new TcpListener(IPAddress.Any, porta);
            server.Start();

            //thread para constantemente conferir se tem clientes para serem aceitos
            Thread AceitarClientes = new Thread(() =>
            {
                //------loop pra continuar aceitando clientes---------------//
                while (true)
                {
                    TcpClient novoCliente = server.AcceptTcpClient();
                    //-------- trava aqui esperando um cliente-----------//

                    //-------- se aceitou, o cliente precisa mandar o seu nickname------//
                    NetworkStream stream = novoCliente.GetStream();
                    byte[] buffer = new byte[4096];

                    int bytesLidos = stream.Read(buffer, 0, buffer.Length); ///////////os bytes, offset e o tamanho de bytes, vai ficar com o tamanho
                    //--------- espera o nickname------------//

                    //-------- recebe o nickname---------//

                    string Nickname = Encoding.UTF8.GetString(buffer, 0, bytesLidos); //o array de bites, index e o tamanho, vai traduzir pra string

                    //--------- cria um novo cliente do tipo Cliente ---------//
                    Cliente cliente = new Cliente(Nickname, novoCliente);
                    list.Add(cliente);
                    Broadcast("MSG:" + Nickname + " se conectou! :D");
                    ListaDeUsuarios();

                    // -------- continua esperando por outros clientes --------//

                    //---------- thread pra ouvir cada cliente --------//

                    Thread OuvirClientes = new Thread(() =>
                    {
                        NetworkStream str = novoCliente.GetStream();
                        byte[] bffr = new byte[4096];

                        //------ continuamente esperando quantas mensagens forem precisas ------/
                        while (true)
                        {
                            try
                            {
                                int bytesL = str.Read(bffr, 0, bffr.Length);
                                string msg = Encoding.UTF8.GetString(bffr, 0, bytesL);


                                //-------aqui, dependendo do que for a mensagem sobre, vai mudar o que o server faz-----//
                                if (msg.StartsWith("MSG:"))
                                {
                                    //----- confere se o cliente ta numa sala privada -----//
                                    Privado sala = salas.Find(s => s.Pertence(cliente));

                                    //----- se ele tiver, qr dizer que a mensagem é privada -----//
                                    if (sala != null)
                                    {
                                        sala.PvBroadcast(msg, cliente);
                                    }
                                    //----- se nao a mensagem é pra todos do saguão ----//
                                    else
                                    {
                                        Broadcast(msg);
                                    }

                                }
                                else if (msg.StartsWith("INVITE:"))
                                {
                                    //----- pra ver quem mandou e pra quem convidar -----//
                                    string[] s = msg.Split(':');

                                    //0 - INVITE | 1 = quem convidou | 2 - quem ta sendo convidado

                                    string invited = s[2];

                                    //--------- temps -----//

                                    Cliente a = null;

                                    //------- procuro o nickname -----//

                                    foreach (var user in list)
                                    {
                                        if (invited == user.GetNome())
                                        {
                                            a = user;
                                        }
                                    }

                                    //--- se eu achei, eu envio ---//
                                    if (a != null)
                                    {
                                        byte[] buff = Encoding.UTF8.GetBytes(msg);
                                        a.GetConexao().GetStream().Write(buff, 0, buff.Length);
                                    }

                                }
                                else if (msg.StartsWith("ACCEPT:"))
                                {
                                    //----- pra ver quem mandou e pra quem convidar -----//
                                    string[] s = msg.Split(':');

                                    //0 - ACCEPT | 1 = quem aceitou | 2 - quem pediu
                                    string invited = s[1];
                                    string inviter = s[2];

                                    Cliente a = null, b = null;

                                    //------- procuro os nicknames -----//

                                    foreach (var user in list)
                                    {
                                        if (invited == user.GetNome())
                                        {
                                            a = user;
                                        }
                                        else if (inviter == user.GetNome())
                                        {
                                            b = user;
                                        }
                                    }

                                    if (a != null && b != null)
                                    {
                                        salas.Add(new Privado(a, b));
                                    }
                                }
                                else if (msg.StartsWith("REFUSE:"))
                                {
                                    string[] s = msg.Split(':');

                                    //0 - ACCEPT | 1 = quem aceitou | 2 - quem pediu
                                    string inviter = s[2];

                                    //--- supostamente funciona igual o foreach, vou deixar pra testar aqui no refuse ----//
                                    Cliente user1 = list.Find(u => u.GetNome() == inviter);

                                    if (user1 != null)
                                    {
                                        byte[] buff = Encoding.UTF8.GetBytes(msg);
                                        user1.GetConexao().GetStream().Write(buff, 0, buff.Length);
                                    }
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }

                        list.Remove(cliente);
                        ListaDeUsuarios();
                    });

                    OuvirClientes.IsBackground = true;
                    OuvirClientes.Start();
                }

            });

            AceitarClientes.IsBackground = true;
            AceitarClientes.Start();


        }

        public void Broadcast(string message)
        {
            string msg = message + "|";

            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            foreach(Cliente cliente in list)
            {
                cliente.GetConexao().GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public void ListaDeUsuarios()
        {
            string message_ = "LIST:" + string.Join(",", list.Select(c => c.GetNome())) + "|";

            /*
            foreach(Cliente cliente in list)
            {
                message_ += cliente.GetNome();
                message_ += ",";
            }
            */

            byte[] buffer = Encoding.UTF8.GetBytes(message_);

            foreach(Cliente cliente in list)
            {
                cliente.GetConexao().GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
