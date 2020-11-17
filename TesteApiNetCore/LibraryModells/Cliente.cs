using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryModells
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdClienteCpf")]
        public Int64 IdClienteCpf { get; set; }
        [Required]
        public string NomeCliente { get; set; }
        public string Email { get; set; }


    }
}
