using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryModells;

namespace LibraryServices
{
   public interface ICliente
    {

        public Task<IEnumerable<Cliente>> GetAll();
        public Task<IEnumerable<Cliente>> GetFind(Int64 key);
        public Task<int> InsertCliente(Cliente produto);
        public Task<int> UpdateCliente(Cliente produto);
        //public Task<IEnumerable<Produto>> DeleteProduto(int id);
        public Task<int> DeleteCliente(Int64 id);




    }
}
