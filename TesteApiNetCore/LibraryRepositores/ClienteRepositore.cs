using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using LibraryModells;
using System.Threading.Tasks;
using Dapper;
using LibraryServices;

namespace LibraryRepositores
{
    public class ClienteRepositore : ICliente
    {
        private readonly IConfiguration _config;

        public ClienteRepositore(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MasterDatabase"));
            }
        }


        // busca todos os clientes cadastrados 
        public async Task<IEnumerable<Cliente>> GetAll()

        {
            IEnumerable<Cliente> cliente = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = "select IdClienteCpf,  NomeCliente, Email from Cliente (nolock)";
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    cliente = await conn.QueryAsync<Cliente>(sQuery);
                }
            }
            return cliente;
        }
        // buscar produto por IdClienteCpf
        public async Task<IEnumerable<Cliente>> GetFind(Int64 key)
        {
            IEnumerable<Cliente> cliente = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"select IdClienteCpf,  NomeCliente, Email from Cliente (nolock)
                                    where IdClienteCpf = {key} ";
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    cliente = await conn.QueryAsync<Cliente>(sQuery);
                }
            }
            return cliente;
        }

        //inserir Cliente
        public async Task<int> InsertCliente(Cliente produto)
        {
            int valor = 0;
            
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"INSERT INTO Cliente(IdClienteCpf, NomeCliente, Email)
                                    VALUES(@IdClienteCpf, @NomeCliente, @Email)";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery, produto);
                }
            }
            return valor;
        }

        //atualiza Cliente
        public async Task<int> UpdateCliente(Cliente cliente)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"UPDATE Cliente
                                 SET IdClienteCpf = @IdClienteCpf, NomeCliente = @NomeCliente, Email = @Email
                                 WHERE IdClienteCpf = @IdClienteCpf";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery, cliente);
                }
            }
            return valor;
        }

        //deleta produto
        public async Task<int> DeleteCliente(Int64 Key)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"DELETE FROM Cliente where IdClienteCpf = {Key} ";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery);
                }
            }
            return valor;
        }
    }
}
