using AutoMapper;
using GamesAPI.DTO;
using GamesAPI.Models;
using GamesAPI.Pagination;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("Jogos")]
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

        [HttpGet]
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

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
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

        [HttpPost]
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

        [HttpPut("{id:int}")]
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

        [HttpDelete("{id:int}")]
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