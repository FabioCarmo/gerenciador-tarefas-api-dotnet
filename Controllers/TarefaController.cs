
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Context;
using GerenciadorTarefas.Models.Entities;

namespace GerenciadorTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController: ControllerBase
    {

        private readonly TarefaContext _context;

        // Passar a operacoes de CRUD para a propriedade _context
        public TarefaController(TarefaContext context) {
            _context = context;
        }

        // Obter o Id no DB
        [HttpGet("{id}")]
        public IActionResult ObterId(int id) {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null) {
                return NotFound();
            }

            return Ok(tarefa);
        }

        // Criar uma nova tarefa para inserir no DB
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa) {

            if (tarefa.Data == DateTime.MinValue) {
                return BadRequest(new {erro = "Data nao pode ser vazia !"});
            }

            _context.Add(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }    
    }
}