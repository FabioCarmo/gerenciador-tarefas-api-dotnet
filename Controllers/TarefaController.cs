
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

        // Obter a tarefa por Id no DB
        [HttpGet("{id}")]
        public IActionResult ObterId(int id) {
            var tarefa = _context.Tarefas.Find(id); // Procura o valor do parametro no banco

            if (tarefa == null) {
                return NotFound();
            }

            return Ok(tarefa);
        }

        // Obter tarefa pelo titulo
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterTitulo(string titulo) {
            // Busca o valor do parametro passado no campo titulo do db
            var tarefadb = _context.Tarefas.Where(x => x.Titulo == titulo);

            if (tarefadb == null) {
                return NotFound();
            }

            return Ok(tarefadb);
        }

        // Criar uma nova tarefa para inserir no DB passando como parametro o JSON tarefa
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa) {

            if (tarefa.Data == DateTime.MinValue) {
                return BadRequest(new {erro = "Data nao pode ser vazia !"});
            }

            _context.Add(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        } 

        // Atualizar nova tarefa no DB 
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa) {
            var tarefadb = _context.Tarefas.Find(id);

            if (tarefadb == null) {
                return NotFound();
            }

            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new {erro = "A data da tarefa não pode ser vazia !"});
            }

            // Atribui novos valores a propriedade do banco
            tarefadb.Descricao = tarefa.Descricao;
            tarefadb.Titulo = tarefa.Titulo;
            tarefadb.Data = tarefa.Data;
            tarefadb.Status = tarefa.Status;

            // Atualiza e Salva no DB
            _context.Update(tarefadb);
            _context.SaveChanges();

            return Ok(tarefa);
        }

        // Apaga a tarefa no DB passando como parametro o id da tarefa
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            var tarefadb = _context.Tarefas.Find(id);

            if (tarefadb == null) {
                return NotFound();
            }

            // Remover e Salvar
            _context.Tarefas.Remove(tarefadb);
            _context.SaveChanges();
            return NoContent();
        }   
    }
}