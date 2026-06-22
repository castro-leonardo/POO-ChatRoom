using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    internal class Privado
    {
        //------------------ Classe pras conversas no privado ------------------------//
        private Cliente cliente1;
        private Cliente cliente2;

        public void SetCliente_1(Cliente c) => this.cliente1 = c;
        public void SetCliente_2(Cliente c) => this.cliente2 = c;
        public Cliente GetCliente_1() => this.cliente1;
        public Cliente GetCliente_2() => this.cliente2;

        public Privado(Cliente c1, Cliente c2)
        {
            SetCliente_1(c1);
            SetCliente_2(c2);
        }

        public bool Pertence(Cliente c) => c == GetCliente_1() || c == GetCliente_2();
        public void PvBroadcast(string Mensagem, Cliente Remetente)
        {
            //---Ternary  ::: Funciona com Condição ? valor se verdadeiro : valor se falso ---------------//
            Cliente Destinatario = (Remetente == this.cliente1) ? GetCliente_2() : GetCliente_1();
            // assim, o destinatario sera o cliente 2 se o remetente foi o 1, e o cliente 1 se o remetente nao for o 1

            byte[] bfr = Encoding.UTF8.GetBytes(Mensagem);
            Destinatario.GetConexao().GetStream().Write(bfr, 0 , bfr.Length);

        }
    }
}
