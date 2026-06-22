using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChatRoom
{
    internal class Cliente
    {
        private string nome = string.Empty;
        private TcpClient conexao = null;

        public Cliente(string nome, TcpClient conexao)
        {
            SetNome(nome);
            SetConexao(conexao);
            conectou(nome, conexao);
        }

        public void SetNome(string nome) => this.nome = nome;
        public void SetConexao(TcpClient conexao) => this.conexao = conexao;
        public string GetNome() => this.nome;
        public TcpClient GetConexao() => this.conexao;

        public void conectou(string n, TcpClient c)
        {
            Stream w = c.GetStream();
            string s = n + " se conectou! :D";

            byte[] resposta = Encoding.UTF8.GetBytes(s);
            w.Write(resposta, 0, resposta.Length);
        }

    }
}
