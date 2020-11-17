using System;
using System.Collections.Generic;
using System.Text;
using LibraryServices;
using LibraryModells;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace LibraryRepositores
{
   public class PedidoRepositore : IPedido
    {
        private readonly IConfiguration _config;

        public PedidoRepositore(IConfiguration config)
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

        // busca todos os pedidos cadastrados 
        public async Task<IEnumerable<Pedido>> GetAll()

        {
            IEnumerable<Pedido> pedido = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = "select NumPedido,  DataPedido, NomeCliente, ValorPedido, IdClienteCpf from Pedido (nolock)";

                
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    //var testeee = await conn.QueryAsync<dynamic>(sQuery);


                    pedido = await conn.QueryAsync<Pedido>(sQuery);
                }
            }
            return pedido;
        }
        // buscar pedido  por id_pedido
        public async Task<IEnumerable<Pedido>> GetFind(int key)
        {
            IEnumerable<Pedido> pedido = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"select NumPedido,  DataPedido, NomeCliente, ValorPedido, IdClienteCpf from Pedido (nolock)
                                    where NumPedido = {key} ";
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    pedido = await conn.QueryAsync<Pedido>(sQuery);
                }
            }
            return pedido;
        }

        //inserir produto
        public async Task<int> InsertPedido(Pedido pedido)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"INSERT INTO Pedido(NomeCliente, ValorPedido, IdClienteCpf)
                                    VALUES(@NomeCliente, @ValorPedido, @IdClienteCpf)";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery, pedido);
                }
            }
            return valor;
        }

        //atualiza produto
        public async Task<int> UpdatePedido(Pedido pedido)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"UPDATE Pedido
                                 SET NomeProduto = @NomeProduto, ValorProduto = @ValorProduto
                                 , IdClienteCpf = @IdClienteCpf
                                 WHERE IdProduto = @IdProduto";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery, pedido);
                }
            }
            return valor;
        }

        //deleta produto
        public async Task<int> DeletePedido(int Key)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"DELETE FROM Pedido where NumPedido = {Key}
                                   DELETE FROM ItensPedidos where NumPedido = {Key}";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    //valor = await conn.ExecuteAsync(sQuery);

                    valor = await conn.ExecuteAsync(sQuery, new { Key});


                }
            }
            return valor;
        }









    }
}
