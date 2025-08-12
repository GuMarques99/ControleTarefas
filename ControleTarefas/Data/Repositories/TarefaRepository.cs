using Data.Context;
using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ApplicationDbContext db;

        public TarefaRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task Criar(Tarefa tarefa)
        {
            await db.Tarefa.AddAsync(tarefa);
            await db.SaveChangesAsync();
        }

        public async Task<Tarefa> BuscarPorId(int id)
        {
            return (await db.Tarefa
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id) ) 
                ?? throw new ArgumentException("Tarefa não encontrada");
        }

        public async Task<IEnumerable<Tarefa>> BuscarPorStatus(IEnumerable<EStatusTarefa> status)
        {
            return await db.Tarefa
                .AsNoTracking()
                .Where(t => status.Any(st => st == t.Status))
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task Editar(Tarefa tarefa)
        {
            db.Tarefa.Update(tarefa);
            await db.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var tarefa = await BuscarPorId(id);

            db.Tarefa.Remove(tarefa);
            await db.SaveChangesAsync();
        }
    }
}
