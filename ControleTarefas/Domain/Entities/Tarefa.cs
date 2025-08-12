using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tarefa")]
    public partial class Tarefa
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("titulo")]
        [MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Column("descricao")]
        [MinLength(5)]
        public string? Descricao { get; set; }

        [Required]
        [Column("status")]
        public EStatusTarefa Status { get; set; }

        [Required]
        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; }

        [Column("data_inicio")]
        public DateTime? DataInicio { get; set; }

        [Column("data_altercao")]
        public DateTime? DataAlteracao { get; set; }
    }
}
