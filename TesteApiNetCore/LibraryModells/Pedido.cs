using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModells
{
    [Table("Cliente")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("NumPedido")]
        public int NumPedido { get; set; }
        public string DataPedido { get; set; }
        public string NomeCliente { get; set; }
        public decimal ValorPedido { get; set; }
        public Int64 IdClienteCpf { get; set; }
        //public List<Produto> produtos { get; set; }



    }
}
