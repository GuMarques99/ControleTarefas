using AutoMapper;
using Core.DTOs.Tarefa;
using Core.ServiceInterfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<BuscarTarefaDTO> Criar(CriarTarefaDTO tarefa)
        {
            var tarefaDb = tarefa.MapTarefa();

            await _tarefaRepository.Criar(tarefaDb);

            return new BuscarTarefaDTO(tarefaDb);
        }

        public async Task<BuscarTarefaDTO> BuscarTarefaPorId(int id)
        {
            var tarefaDb = await _tarefaRepository.BuscarPorId(id);
            return new BuscarTarefaDTO(tarefaDb);
        }

        public async Task<IEnumerable<BuscarTarefaDTO>> BuscarTarefasPorStatus(IEnumerable<EStatusTarefa> listaStatus)
        {
            if (!(listaStatus?.Any() ?? false))
            {
                listaStatus = [EStatusTarefa.Novo, EStatusTarefa.EmAndamento, EStatusTarefa.Paralisado, EStatusTarefa.Concluido, EStatusTarefa.Reaberto];
            }

            return (await _tarefaRepository.BuscarPorStatus(listaStatus)).Select(t => new BuscarTarefaDTO(t));
        }

        public async Task<BuscarTarefaDTO> Editar(EditarTerefaDTO tarefa)
        {
            var tarefaDb = await _tarefaRepository.BuscarPorId(tarefa.Id);

            var statusSemEdicao = new[] { EStatusTarefa.Concluido, EStatusTarefa.Excluido };
            var statusNaoPermitidos = new[] { EStatusTarefa.Excluido, EStatusTarefa.Novo, EStatusTarefa.Reaberto };

            if (statusSemEdicao.Any(s => tarefaDb.Status == s))
                throw new ArgumentException($"A tarefa não pode ser alterada neste status");

            if (statusNaoPermitidos.Any(s => tarefa.Status == s))
                throw new ArgumentException($"A tarefa não pode ser alterada para este status");

            if (tarefaDb.Status == EStatusTarefa.Novo && tarefa.Status != EStatusTarefa.Novo)
            {
                tarefaDb.DataInicio = DateTime.Now;
            }

            tarefaDb.Status = tarefa.Status;
            tarefaDb.Descricao = tarefa.Descricao;
            tarefaDb.Titulo = tarefa.Titulo;
            tarefaDb.DataAlteracao = DateTime.Now;

            await _tarefaRepository.Editar(tarefaDb);
            return new BuscarTarefaDTO(tarefaDb);
        }

        public async Task<BuscarTarefaDTO> Excluir(int id)
        {
            var tarefaDb = await _tarefaRepository.BuscarPorId(id);
            tarefaDb.Status = EStatusTarefa.Excluido;
            tarefaDb.DataAlteracao = DateTime.Now;

            await _tarefaRepository.Editar(tarefaDb);
            return new BuscarTarefaDTO(tarefaDb);
        }

        public async Task<BuscarTarefaDTO> Reabrir(int id)
        {
            var tarefaDb = await _tarefaRepository.BuscarPorId(id);
            tarefaDb.Status = EStatusTarefa.Reaberto;
            tarefaDb.DataAlteracao = DateTime.Now;

            await _tarefaRepository.Editar(tarefaDb);
            return new BuscarTarefaDTO(tarefaDb);
        }
    }
}
