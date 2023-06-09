using GamesAPI.Models;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JogosController(IUnitOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Jogo>> Get()
        {
            var jogos = _unitOfWork.JogoRepository.Get().Take(10).ToList(); // Irá retornar no máximo 10 Jogos

            if (jogos is null)
                return NotFound("Jogos não encontrados.");

            return jogos;
        }

        [HttpGet("{id:int}", Name = "ObterJogo")]
        public ActionResult<Jogo> Get(int id)
        {
            var jogo = _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);

            if (jogo is null)
                return NotFound("Jogos não encontrado.");

            return jogo;
        }

        [HttpGet("MenorPreco")]
        public ActionResult<IEnumerable<Jogo>> GetJogosPrecos()
        {
            var jogos = _unitOfWork.JogoRepository.GetJogoPorPreco().Take(10).ToList();

            if (jogos is null)
                return NotFound("Jogos não encontrados.");

            return jogos;
        }

        [HttpPost]
        public ActionResult Post(Jogo jogo)
        {
            if (jogo is null)
                return BadRequest();

            _unitOfWork.JogoRepository.Add(jogo);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterJogo",
                new { id = jogo.JogoId }, jogo);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Jogo jogo)
        {
            if (id != jogo.JogoId)
                return BadRequest();

            _unitOfWork.JogoRepository.Update(jogo);
            _unitOfWork.Commit();

            return Ok(jogo);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var jogo = _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);                                                                     

            if (jogo is null)
                return NotFound("Jogo não localizado.");

            _unitOfWork.JogoRepository.Delete(jogo);
            _unitOfWork.Commit();

            return Ok(jogo);
        }

    }
}