
using GerenciadorTarefas.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Context
{
    public class TarefaContext: DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options): base(options) {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}