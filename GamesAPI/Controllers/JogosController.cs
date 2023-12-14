using AutoMapper;
using GamesAPI.DTO;
using GamesAPI.DTO.Examples;
using GamesAPI.Models;
using GamesAPI.Pagination;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<JogosController> _logger;

        public JogosController(IUnitOfWork unityOfWork, IMapper mapper, ILogger<JogosController> logger)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Busca as informações dos jogos de forma páginada.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<JogoDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<JogoDTO>>> Get([FromQuery] JogosParameters jogosParameters)
        {
            try
            {
                //var jogos = _unitOfWork.JogoRepository.Get().Take(10).ToListAsync(); // Irá retornar no máximo 10 Jogos
                var jogos = await _unitOfWork.JogoRepository.GetJogos(jogosParameters);

                if (jogos is null)
                    return NotFound("Jogos não encontrados.");

                var metadata = new
                {
                    jogos.TotalCount,
                    jogos.PageSize,
                    jogos.CurrentPage,
                    jogos.TotalPages,
                    jogos.HasPrevious,
                    jogos.HasNext
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var jogosDTO = _mapper.Map<List<JogoDTO>>(jogos);

                _logger.LogInformation("Jogos retornados com sucesso.");
                return jogosDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }

        }

        /// <summary>
        /// Busca as informações do jogo com base no Id informado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "ObterJogo")]
        [ProducesResponseType(typeof(List<JogoDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<JogoDTO>> Get(int id)
        {
            try
            {
                var jogo = await _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);

                if (jogo is null)
                    return NotFound("Jogo não encontrado.");

                var jogoDTO = _mapper.Map<JogoDTO>(jogo);

                _logger.LogInformation("Jogo retornado com sucesso.");
                return jogoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        /// <summary>
        /// Busca as informações dos jogos registrados ordenados com base no preço.
        /// </summary>
        /// <returns></returns>
        [HttpGet("MenorPreco")]
        [ProducesResponseType(typeof(List<JogoDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<JogoDTO>>> GetJogosPrecos()
        {
            try
            {
                //var jogos = _unitOfWork.JogoRepository.GetJogoPorPreco().Take(10).ToList();

                var jogos = await _unitOfWork.JogoRepository.GetJogoPorPreco();

                if (jogos is null)
                    return NotFound("Jogos não encontrados.");

                var jogosDTO = _mapper.Map<List<JogoDTO>>(jogos);

                _logger.LogInformation("Jogos retornados com sucesso.");
                return jogosDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        /// <summary>
        /// Realiza o registro de novos jogos.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<JogoDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody]JogoDTO jogoDto)
        {
            try
            {
                if (jogoDto is null)
                    return BadRequest();

                var jogo = _mapper.Map<Jogo>(jogoDto);

                _unitOfWork.JogoRepository.Add(jogo);
                await _unitOfWork.Commit();

                var jogoDTO = _mapper.Map<JogoDTO>(jogo);

                _logger.LogInformation("Jogo cadastrado com sucesso.");
                return new CreatedAtRouteResult("ObterJogo",
                    new { id = jogo.JogoId }, jogoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        /// <summary>
        /// Atualiza o registro de um jogo com base no Id informado.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(List<JogoDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Put(int id, [FromBody]Jogo jogoDto)
        {
            try
            {
                if (id != jogoDto.JogoId)
                    return BadRequest();

                var jogo = _mapper.Map<Jogo>(jogoDto);

                _unitOfWork.JogoRepository.Update(jogo);
                await _unitOfWork.Commit();

                var jogoDTO = _mapper.Map<JogoDTO>(jogo);

                _logger.LogInformation("Jogo alterado com sucesso.");
                return Ok(jogoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        /// <summary>
        /// Deleta o registro de um jogo com base no Id informado.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(List<JogoDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<JogoDTO>> Delete(int id)
        {
            try
            {
                var jogo = await _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);

                if (jogo is null)
                    return NotFound("Jogo não localizado.");

                _unitOfWork.JogoRepository.Delete(jogo);
                await _unitOfWork.Commit();

                var jogoDTO = _mapper.Map<JogoDTO>(jogo);

                _logger.LogInformation("Jogo deletado com sucesso.");
                return Ok(jogoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

    }
}