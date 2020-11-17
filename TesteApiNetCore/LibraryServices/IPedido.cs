using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryModells;


namespace LibraryServices
{
    public interface IPedido
    {
        public Task<IEnumerable<Pedido>> GetAll();
        public Task<IEnumerable<Pedido>> GetFind(int key);
        public Task<int> InsertPedido(Pedido pedido);
        public Task<int> UpdatePedido(Pedido pedido);
        //public Task<IEnumerable<Produto>> DeleteProduto(int id);
        public Task<int> DeletePedido(int id);

    }

}
