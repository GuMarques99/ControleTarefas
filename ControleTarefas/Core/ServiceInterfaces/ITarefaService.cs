using Core.DTOs.Tarefa;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceInterfaces
{
    public interface ITarefaService
    {
        Task<BuscarTarefaDTO> Criar(CriarTarefaDTO tarefa);
        Task<BuscarTarefaDTO> Editar(EditarTerefaDTO tarefa);
        Task<BuscarTarefaDTO> BuscarTarefaPorId(int id);
        Task<IEnumerable<BuscarTarefaDTO>> BuscarTarefasPorStatus(IEnumerable<EStatusTarefa> listaStatus);
        Task<BuscarTarefaDTO> Excluir(int id);
        Task<BuscarTarefaDTO> Reabrir(int id);
    }
}
