using AutoMapper;
using GamesAPI.DTO;
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
        private readonly IMapper _mapper;

        public JogosController(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unitOfWork = unityOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JogoDTO>> Get()
        {
            var jogos = _unitOfWork.JogoRepository.Get().Take(10).ToList(); // Irá retornar no máximo 10 Jogos

            if (jogos is null)
                return NotFound("Jogos não encontrados.");

            var jogosDTO = _mapper.Map<List<JogoDTO>>(jogos);

            return jogosDTO;
        }

        [HttpGet("{id:int}", Name = "ObterJogo")]
        public ActionResult<JogoDTO> Get(int id)
        {
            var jogo = _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);

            if (jogo is null)
                return NotFound("Jogo não encontrado.");

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return jogoDTO;
        }

        [HttpGet("MenorPreco")]
        public ActionResult<IEnumerable<JogoDTO>> GetJogosPrecos()
        {
            var jogos = _unitOfWork.JogoRepository.GetJogoPorPreco().Take(10).ToList();

            if (jogos is null)
                return NotFound("Jogos não encontrados.");

            var jogosDTO = _mapper.Map<List<JogoDTO>>(jogos);

            return jogosDTO;
        }

        [HttpPost]
        public ActionResult Post([FromBody]JogoDTO jogoDto)
        {
            if (jogoDto is null)
                return BadRequest();

            var jogo = _mapper.Map<Jogo>(jogoDto);

            _unitOfWork.JogoRepository.Add(jogo);
            _unitOfWork.Commit();

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return new CreatedAtRouteResult("ObterJogo",
                new { id = jogo.JogoId }, jogoDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody]Jogo jogoDto)
        {
            if (id != jogoDto.JogoId)
                return BadRequest();

            var jogo = _mapper.Map<Jogo>(jogoDto);

            _unitOfWork.JogoRepository.Update(jogo);
            _unitOfWork.Commit();

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return Ok(jogoDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<JogoDTO> Delete(int id)
        {
            var jogo = _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);                                                                     

            if (jogo is null)
                return NotFound("Jogo não localizado.");

            _unitOfWork.JogoRepository.Delete(jogo);
            _unitOfWork.Commit();

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return Ok(jogoDTO);
        }

    }
}