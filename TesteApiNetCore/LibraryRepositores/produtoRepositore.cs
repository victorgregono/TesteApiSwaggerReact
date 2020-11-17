using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LibraryServices;
using Microsoft.Extensions.Configuration;
using LibraryModells;
using Dapper;

namespace LibraryRepositores
{
    public class produtoRepositore : IProduto
    {
        private readonly IConfiguration _config;

        public produtoRepositore(IConfiguration config)
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

        // busca todos os produtos cadastrados 
        public async Task<IEnumerable<Produto>> GetAll()

        {
            IEnumerable<Produto> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = "select IdProduto,  NomeProduto, ValorProduto from Produto (nolock)";
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    produto = await conn.QueryAsync<Produto>(sQuery);
                }
            }
            return produto;
        }
        // buscar produto por id_produto
        public async Task<IEnumerable<Produto>> GetFind(int key)
        {
            IEnumerable<Produto> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"select IdProduto,  NomeProduto, ValorProduto from Produto (nolock)
                                    where IdProduto = {key} ";
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    produto = await conn.QueryAsync<Produto>(sQuery);
                }
            }
            return produto;
        }

        //inserir produto
        public async Task<int> Insertproduto(Produto produto)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"INSERT INTO Produto(NomeProduto, ValorProduto)
                                    VALUES(@NomeProduto, @ValorProduto)";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery, produto);
                }
            }
            return valor;
        }

        //atualiza produto
        public async Task<int> Updateproduto(Produto produto)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"UPDATE Produto
                                 SET NomeProduto = @NomeProduto, ValorProduto = @ValorProduto
                                 WHERE IdProduto = @IdProduto";

                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    valor = await conn.ExecuteAsync(sQuery, produto);
                }
            }
            return valor;
        }

        //deleta produto
        public async Task<int> DeleteProduto(int Key)
        {
            int valor = 0;

            // IEnumerable<Produtos> produto = null;
            using (IDbConnection conn = Connection)
            {
                string sQuery = $@"DELETE FROM Produto where IdProduto = {Key} ";

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
