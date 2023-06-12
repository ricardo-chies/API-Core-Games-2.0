using AutoMapper;
using GamesAPI.DTO;
using GamesAPI.Models;
using GamesAPI.Pagination;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public async Task<ActionResult<IEnumerable<JogoDTO>>> Get([FromQuery] JogosParameters jogosParameters)
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

            return jogosDTO;
        }

        [HttpGet("{id:int}", Name = "ObterJogo")]
        public async Task<ActionResult<JogoDTO>> Get(int id)
        {
            var jogo = await _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);

            if (jogo is null)
                return NotFound("Jogo não encontrado.");

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return jogoDTO;
        }

        [HttpGet("MenorPreco")]
        public async Task<ActionResult<IEnumerable<JogoDTO>>> GetJogosPrecos()
        {
            //var jogos = _unitOfWork.JogoRepository.GetJogoPorPreco().Take(10).ToList();

            var jogos = await _unitOfWork.JogoRepository.GetJogoPorPreco();

            if (jogos is null)
                return NotFound("Jogos não encontrados.");

            var jogosDTO = _mapper.Map<List<JogoDTO>>(jogos);

            return jogosDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]JogoDTO jogoDto)
        {
            if (jogoDto is null)
                return BadRequest();

            var jogo = _mapper.Map<Jogo>(jogoDto);

            _unitOfWork.JogoRepository.Add(jogo);
            await _unitOfWork.Commit();

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return new CreatedAtRouteResult("ObterJogo",
                new { id = jogo.JogoId }, jogoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody]Jogo jogoDto)
        {
            if (id != jogoDto.JogoId)
                return BadRequest();

            var jogo = _mapper.Map<Jogo>(jogoDto);

            _unitOfWork.JogoRepository.Update(jogo);
            await _unitOfWork.Commit();

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return Ok(jogoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<JogoDTO>> Delete(int id)
        {
            var jogo = await _unitOfWork.JogoRepository.GetById(p => p.JogoId == id);                                                                     

            if (jogo is null)
                return NotFound("Jogo não localizado.");

            _unitOfWork.JogoRepository.Delete(jogo);
            await _unitOfWork.Commit();

            var jogoDTO = _mapper.Map<JogoDTO>(jogo);

            return Ok(jogoDTO);
        }

    }
}