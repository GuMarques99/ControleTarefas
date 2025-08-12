using Core.DTOs.Tarefa;
using Core.ServiceInterfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BuscarTarefaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarTarefa([FromBody] CriarTarefaDTO tarefa)
        {
            var criado = await _tarefaService.Criar(tarefa);            

            return CreatedAtAction(nameof(CriarTarefa), criado);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BuscarTarefaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarTarefa([FromBody] EditarTerefaDTO tarefa)
        {
            var editado = await _tarefaService.Editar(tarefa);

            return Ok(editado);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BuscarTarefaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarPorId([FromRoute] int id)
        {
            var tarefa = await _tarefaService.BuscarTarefaPorId(id);
            return Ok(tarefa);
        }

        [HttpGet("ativos")]
        [ProducesResponseType(typeof(IEnumerable<BuscarTarefaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarAtivos()
        {
            var listaStatus = new[] { EStatusTarefa.Novo, EStatusTarefa.EmAndamento, EStatusTarefa.Paralisado, EStatusTarefa.Reaberto };
            var tarefas = await _tarefaService.BuscarTarefasPorStatus(listaStatus);
            return Ok(tarefas);
        }

        [HttpGet("status")]
        [ProducesResponseType(typeof(IEnumerable<BuscarTarefaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarPorStatus([FromQuery] IEnumerable<EStatusTarefa> status)
        {            
            var tarefas = await _tarefaService.BuscarTarefasPorStatus(status);
            return Ok(tarefas);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BuscarTarefaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExcluirTarefa([FromRoute] int id)
        {
            var retorno = await _tarefaService.Excluir(id);
            return Ok(retorno);
        }

        [HttpPut("reabrir/{id}")]
        [ProducesResponseType(typeof(BuscarTarefaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reabrir([FromRoute] int id)
        {
            var retorno = await _tarefaService.Reabrir(id);
            return Ok(retorno);
        }
    }
}
