using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Tarefa
{
    public class BuscarTarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public EStatusTarefa Status { get; set; }
        public string StatusStr { get =>
            Status switch
            {
                EStatusTarefa.Novo => "Novo",
                EStatusTarefa.EmAndamento => "Em Andamento",
                EStatusTarefa.Paralisado => "Paralisado",
                EStatusTarefa.Concluido => "Concluído",
                EStatusTarefa.Excluido => "Excluído",
                EStatusTarefa.Reaberto => "Reaberto",
                _ => "Desconhecido"
            };
        }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public TimeSpan? Duracao { get =>
            Status switch
            {
                EStatusTarefa.Novo => DateTime.Now - DataCriacao,
                EStatusTarefa.Concluido => (DataAlteracao ?? DateTime.Now) - (DataInicio ?? DateTime.Now),
                _ => DateTime.Now - (DataInicio ?? DateTime.Now)
            };            
        }

        public BuscarTarefaDTO(Domain.Entities.Tarefa tarefaDb)
        {
            Id = tarefaDb.Id;
            Titulo = tarefaDb.Titulo;
            Descricao = tarefaDb.Descricao;
            Status = tarefaDb.Status;
            DataCriacao = tarefaDb.DataCriacao;
            DataInicio = tarefaDb.DataInicio;
            DataAlteracao = tarefaDb.DataAlteracao;
        }
    }
}
