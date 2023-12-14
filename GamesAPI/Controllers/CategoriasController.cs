using AutoMapper;
using GamesAPI.DTO;
using GamesAPI.DTO.Examples;
using GamesAPI.Models;
using GamesAPI.Pagination;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace APICatalogo.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoriasController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Busca as informações dos jogos registrados acompanhados de suas categorias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Jogos")]
        [ProducesResponseType(typeof(List<CategoriaDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasJogos()
        {
            try
            {
                //var categorias = _unitOfWork.CategoriaRepository.GetCategoriasJogos().Take(10).ToList(); // Irá retornar no máximo 10 Jogos
                var categorias = await _unitOfWork.CategoriaRepository.GetCategoriasJogos();

                if (categorias is null)
                    return NotFound("Jogos não encontrados.");

                var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

                _logger.LogInformation("Categorias retornadas com sucesso.");
                return Ok(categoriasDTO);
            }             
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }

        }

        /// <summary>
        /// Busca as informações das categoria de forma páginada.
        /// </summary>
        /// <param name="categoriasParameters"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            try
            {
                //var categorias = _unitOfWork.CategoriaRepository.Get().AsNoTracking().ToListAsync();

                var categorias = await _unitOfWork.CategoriaRepository.GetCategorias(categoriasParameters);

                if (categorias is null)
                    return NotFound("Jogos não encontrados.");

                var metadata = new
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.CurrentPage,
                    categorias.TotalPages,
                    categorias.HasPrevious,
                    categorias.HasNext
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

                _logger.LogInformation("Categorias retornadas com sucesso.");
                return Ok(categoriasDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        /// <summary>
        /// Busca as informações da categoria com base no Id informado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        [ProducesResponseType(typeof(List<CategoriaDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<CategoriaDTO>> Get([DefaultValue(1)] int id)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriaRepository.GetById(p => p.CategoriaId == id);

                if (categoria is null)
                    return NotFound($"Categoria com id={id} não encontrada.");

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                _logger.LogInformation("Categoria retornada com sucesso.");
                return Ok(categoriaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        /// <summary>
        /// Realiza o registro de novas categorias.
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<CategoriaDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody]CategoriaDTO categoriaDto)
        {
            try
            {
                if (categoriaDto is null)
                    return BadRequest("Dados inválidos...");

                var categoria = _mapper.Map<Categoria>(categoriaDto);

                _unitOfWork.CategoriaRepository.Add(categoria);
                await _unitOfWork.Commit();

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                _logger.LogInformation("Categoria cadastrada com sucesso.");
                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoriaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }

        }

        /// <summary>
        /// Atualiza o registro de uma categoria com base no Id informado.
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(List<CategoriaDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Put(int id, [FromBody]CategoriaDTO categoriaDto)
        {
            try
            {
                if (id != categoriaDto.CategoriaId)
                    return BadRequest("Dados inválidos...");

                var categoria = _mapper.Map<Categoria>(categoriaDto);

                _unitOfWork.CategoriaRepository.Update(categoria);
                await _unitOfWork.Commit();

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                _logger.LogInformation("Categoria alterada com sucesso.");
                return Ok(categoriaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu um problema ao tratar a sua solicitação.", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }

        }

        /// <summary>
        /// Deleta o registro de uma categoria com base no Id informado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(List<CategoriaDTO>), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriaRepository.GetById(p => p.CategoriaId == id);

                if (categoria is null)
                    return NotFound($"Categoria com id= {id} não localizada.");

                _unitOfWork.CategoriaRepository.Delete(categoria);
                await _unitOfWork.Commit();

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                _logger.LogInformation("Categoria deletada com sucesso.");
                return Ok(categoriaDTO);
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