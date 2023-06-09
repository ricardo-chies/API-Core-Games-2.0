using GamesAPI.Context;
using GamesAPI.Models;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Jogos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasJogos()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetCategoriasJogos().Take(10).ToList(); // Irá retornar no máximo 10 Jogos

            if (categorias is null)
                return NotFound("Jogos não encontrados.");

            return categorias;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _unitOfWork.CategoriaRepository.Get().AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _unitOfWork.CategoriaRepository.GetById(p => p.CategoriaId == id);

                if (categoria is null)
                    return NotFound($"Categoria com id={id} não encontrada.");

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação. ");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest("Dados inválidos...");

            _unitOfWork.CategoriaRepository.Add(categoria);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest("Dados inválidos...");

            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(p => p.CategoriaId == id);

            if (categoria is null)
                return NotFound($"Categoria com id= {id} não localizada.");

            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();

            return Ok(categoria);
        }

    }
}