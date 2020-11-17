using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryModells;

namespace LibraryServices
{
   public interface IProduto
    {

        public Task<IEnumerable<Produto>> GetAll();
        public Task<IEnumerable<Produto>> GetFind(int key);
        public Task<int> Insertproduto(Produto produto);
        public Task<int> Updateproduto(Produto produto);
        //public Task<IEnumerable<Produto>> DeleteProduto(int id);
        public Task<int> DeleteProduto(int id);

    }
}
