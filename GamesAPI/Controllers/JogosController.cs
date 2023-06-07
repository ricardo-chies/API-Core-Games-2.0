using GamesAPI.Context;
using GamesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JogosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Jogo>> Get()
        {
            var jogos = _context.Jogos.Take(10).ToList(); // Irá retornar no máximo 10 Jogos

            if (jogos is null)
                return NotFound("Jogos não encontrados.");

            return jogos;
        }

        [HttpGet("{id:int}", Name = "ObterJogo")]
        public ActionResult<Jogo> Get(int id)
        {
            var jogo = _context.Jogos.FirstOrDefault(p => p.JogoId == id);

            if (jogo is null)
                return NotFound("Jogos não encontrado.");

            return jogo;
        }

        [HttpPost]
        public ActionResult Post(Jogo jogo)
        {
            if (jogo is null)
                return BadRequest();

            _context.Jogos.Add(jogo);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterJogo",
                new { id = jogo.JogoId }, jogo);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Jogo jogo)
        {
            if (id != jogo.JogoId)
                return BadRequest();

            _context.Entry(jogo).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(jogo);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var jogo = _context.Jogos.FirstOrDefault(p => p.JogoId == id);                                                                     

            if (jogo is null)
                return NotFound("Jogo não localizado.");

            _context.Jogos.Remove(jogo);
            _context.SaveChanges();

            return Ok(jogo);
        }

    }
}