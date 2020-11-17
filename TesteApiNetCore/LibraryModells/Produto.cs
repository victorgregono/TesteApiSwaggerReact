using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryModells
{
    [Table("Produto")]
    public class Produto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdProduto")]
        public int IdProduto { get; set; }
        [Required]
        //[Column("NomeProduto")]
        public string NomeProduto { get; set; }
        [Required]
        //[Column("ValorProduto")]
        public decimal ValorProduto { get; set; }


    }
}
