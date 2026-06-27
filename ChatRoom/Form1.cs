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
        //-------- lista de clientes conectador & salas privadas --------//
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
                    Broadcast("JNET:" + cliente.GetNome() + " se conectou :D");
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
                                else if (msg.StartsWith("INVITE:")) //convidar para um privado
                                {
                                    //----- pra ver quem mandou e pra quem convidar -----//
                                    string[] s = msg.Replace("|", "").Split(':');

                                    //0 - INVITE | 1 = quem convidou | 2 - quem ta sendo convidado
                                    if (s.Length < 3)
                                        continue;

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
                                        string st = msg + "|";
                                        byte[] buff = Encoding.UTF8.GetBytes(st);
                                        a.GetConexao().GetStream().Write(buff, 0, buff.Length);
                                    }

                                }
                                else if (msg.StartsWith("ACCEPT:")) //aceitar chat priv
                                {
                                    //----- pra ver quem mandou e pra quem convidar -----//
                                    string[] s = msg.Replace("|", "").Split(':');

                                    //0 - ACCEPT | 1 = quem aceitou | 2 - quem pediu
                                    if (s.Length < 3)
                                        continue;

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

                                    //------ se os nicknames existirem, faz o convite ------//
                                    if (a != null && b != null)
                                    {
                                        salas.Add(new Privado(a, b));
                                        string aviso = "ACCEPT:" + invited + ":" + inviter + "|";
                                        byte[] buff = Encoding.UTF8.GetBytes(aviso);
                                        b.GetConexao().GetStream().Write(buff, 0, buff.Length);
                                    }


                                }
                                else if (msg.StartsWith("REFUSE:")) //recusar chat priv
                                {
                                    string[] s = msg.Replace("|", "").Split(':');

                                    //0 - REFUSE | 1 = quem aceitou | 2 - quem pediu
                                    if (s.Length < 3)
                                        continue;

                                    string inviter = s[2];

                                    //--- supostamente funciona igual o foreach, vou deixar pra testar aqui no refuse ----//
                                    // Cliente user1 = list.Find(u => u.GetNome() == inviter);
                                    Cliente user1 = null;

                                    foreach(var us in list)
                                    {
                                        if(inviter == us.GetNome())
                                        {
                                            user1 = us;
                                        }
                                    }

                                    if (user1 != null)
                                    {
                                        byte[] buff = Encoding.UTF8.GetBytes(msg);
                                        user1.GetConexao().GetStream().Write(buff, 0, buff.Length);
                                    }
                                }
                                else if (msg.StartsWith("RETURN:")) //sair do chat priv
                                {
                                    //---------- recebo quem saiu ---------//
                                    string[] s_ = msg.Replace("|", "").Split(':');
                                    //0 - RETURN: | 1 -nickname | 2- outro

                                    if (s_.Length < 3) continue; //pra evitar erros precisa ser divido em 3 partes 

                                    //----- nicks do chat pv ----//
                                    string nick = s_[1];
                                    string outro = s_[2];

                                    Cliente a = null;
                                    Cliente b = null;

                                    //----- achar quem sao os clientes ----//
                                    foreach (Cliente us in list)
                                    {
                                        if (us.GetNome().Equals(nick))
                                        {
                                            a = us;
                                        }
                                        else if(us.GetNome().Equals(outro))
                                        {
                                            b = us;
                                        }
                                    }

                                    //-------- avisar o outro cliente de quem retornou ------//
                                    string mensagem = "RETURN:" + nick + ":" + outro + "|";

                                    byte[] buff = Encoding.UTF8.GetBytes(mensagem);

                                    if (b != null)
                                    {
                                        b.GetConexao().GetStream().Write(buff, 0, buff.Length);
                                    }

                                    foreach (var us in salas.ToList()) //ToList cria uma copia temporaria para nao dar exception
                                    {
                                        if (us.GetCliente_1().Equals(a) || us.GetCliente_2().Equals(a))
                                        {
                                            salas.Remove(us);
                                        }
                                    }
                                }
                                else if (msg.StartsWith("JNET:"))
                                {
                                    Broadcast("JNET:" + cliente.GetNome() + " foi pescado :(");
                                    /*
                                    string msgn = msg + "|";

                                    byte[] bufferr = Encoding.UTF8.GetBytes(msgn);

                                    foreach (Cliente c in list.ToList())
                                    {
                                        if(c != cliente)
                                        {
                                            c.GetConexao().GetStream().Write(bufferr, 0, bufferr.Length);
                                        }
                                    }
                                    */
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

        //--------- envia as mensagens ----------//
        public void Broadcast(string message)
        {
            string msg = message + "|";

            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            foreach(Cliente cliente in list)
            {
                bool emSala = salas.Any(s => s.Pertence(cliente));
                if (!emSala)
                {
                    try
                    {
                        cliente.GetConexao().GetStream().Write(buffer, 0, buffer.Length);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        public void ListaDeUsuarios()
        {
            string message_ = "LIST:" + string.Join(",", list.Select(c => c.GetNome())) + "|";

            byte[] buffer = Encoding.UTF8.GetBytes(message_);

            foreach(Cliente cliente in list)
            {
                cliente.GetConexao().GetStream().Write(buffer, 0, buffer.Length);
            }

            //----------- para aparecer na tela do server ------------//

            this.Invoke(new Action(() =>
            {
                listBox1.Items.Clear();
                foreach (Cliente c in list)
                {
                    string ip = ((System.Net.IPEndPoint)c.GetConexao().Client.RemoteEndPoint).Address.ToString();
                    listBox1.Items.Add(c.GetNome() + " - " + ip);
                }
            }));
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
