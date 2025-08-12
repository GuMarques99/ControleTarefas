using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ITarefaRepository
    {
        Task Criar(Tarefa tarefa);
        Task<Tarefa> BuscarPorId(int id);
        Task<IEnumerable<Tarefa>> BuscarPorStatus(IEnumerable<EStatusTarefa> listaStatus);
        Task Editar(Tarefa tarefa);
        Task Excluir(int id);
    }
}
