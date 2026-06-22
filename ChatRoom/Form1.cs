using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
                    // -------- continua esperando por outros clientes --------//

                    //---------- thread pra ouvir cada cliente --------//

                    Thread OuvirClientes = new Thread(() =>
                    {
                        NetworkStream str = novoCliente.GetStream();
                        byte[] bffr = new byte[4096];

                        //------ continuamente esperando quantas mensagens forem precisas ------/
                        while (true)
                        {
                            int bytesL = str.Read(bffr, 0, bffr.Length);
                            string msg = Encoding.UTF8.GetString(bffr, 0, bytesL);


                            //-------aqui, dependendo do que for a mensagem sobre, vai mudar o que o server faz-----//
                            if(msg.StartsWith("MSG:"))
                            {
                                Privado sala = salas.Find(s => s.Pertence(cliente));
                                sala.PvBroadcast(msg, cliente);
                                Broadcast(msg);
                            }
                            else if (msg.StartsWith("INVITE:"))
                            {

                            }
                            else if (msg.StartsWith("ACCEPT:"))
                            {

                            }
                            else if (msg.StartsWith("REFUSE:"))
                            {

                            }
                        }
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
            byte[] buffer = Encoding.UTF8.GetBytes(message);

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
