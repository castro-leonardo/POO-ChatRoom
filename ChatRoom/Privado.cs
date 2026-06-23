using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRoom
{
    internal class Privado
    {
        //------------------ Classe pras conversas no privado ------------------------//
        private Cliente cliente1;
        private Cliente cliente2;
        private List<Cliente> clientes_ = new List<Cliente>();

        public void SetCliente_1(Cliente c) => this.cliente1 = c;
        public void SetCliente_2(Cliente c) => this.cliente2 = c;
        public Cliente GetCliente_1() => this.cliente1;
        public Cliente GetCliente_2() => this.cliente2;

        public Privado(Cliente c1, Cliente c2)
        {
            SetCliente_1(c1);
            SetCliente_2(c2);

            clientes_.Add(c1 as Cliente);
            clientes_.Add(c2 as Cliente);
        }

        public bool Pertence(Cliente c) => c == GetCliente_1() || c == GetCliente_2();
        public void PvBroadcast(string Mensagem, Cliente Remetente)
        {
            string msg = Mensagem + "|";

            byte[] bfr = Encoding.UTF8.GetBytes(msg);

            foreach (Cliente cliente in clientes_)
            {
                cliente.GetConexao().GetStream().Write(bfr, 0, bfr.Length);
            }

        }
    }
}
