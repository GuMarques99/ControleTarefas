using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Tarefa
{
    public class CriarTarefaDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode ter mais que 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "A descrição deve ter pelo menos 5 caracteres")]
        public string Descricao { get; set; } = string.Empty;
        
        public Domain.Entities.Tarefa MapTarefa()
        {
            return new Domain.Entities.Tarefa
            {
                Titulo = Titulo,
                Descricao = Descricao,
                DataCriacao = DateTime.Now,
                Status = Domain.Enums.EStatusTarefa.Novo
            };
        }
    }
}
