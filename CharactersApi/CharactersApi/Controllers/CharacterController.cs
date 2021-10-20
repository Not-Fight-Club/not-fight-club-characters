using Microsoft.AspNetCore.Mvc;
using CharactersApi_Logic.Interfaces;
using CharactersApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CharactersApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CharacterController : ControllerBase
  {
        
    private readonly ICharacterRepository _repo;
    private readonly ILogger<CharacterController> _logger;

    public CharacterController(ICharacterRepository repo, ILogger<CharacterController> logger)
    {
      _repo = repo;
      _logger = logger;

    }

    // GET: api/<CharacterController>
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new string[] { "value1", "value2" };
    //}

    // GET api/<CharacterController>
    
    [HttpGet("testing")]
    public string Get1()
    {
      return "success";
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ViewCharacter>>> Get()
    {
      var chars = await _repo.Read();
      return Ok(chars);
    }

    // GET api/<CharacterController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ViewCharacter>> Get(int id)
    {
      _logger.LogInformation($"Got request for character by id {id}");
      _logger.LogError("Test Error");
      var selectedCharacter = await _repo.Read(id);
      return Ok(selectedCharacter);
    }
    
    //Get just a single users list of character
    [HttpGet]
    [Route("[action]/{userId}")]
        public async Task<ActionResult<List<ViewCharacter>>> UserCharacters(Guid userId)
    {
        _logger.LogInformation($"Got request for character by user id {userId}");
        var characters = await _repo.ReadAll(userId);
        return Ok(characters);
    }

    // POST api/<CharacterController>
    [HttpPost]
    public async Task<ActionResult<ViewCharacter>> Post([FromBody] ViewCharacter viewCharacter)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      var createdCharacter = await _repo.Add(viewCharacter);
      _logger.LogInformation($"{createdCharacter.Name} created.");

      return Ok(createdCharacter);
    }
        //// PUT api/<CharacterController>/5
        [HttpPut]
        public async Task<ActionResult<ViewCharacter>> Put([FromBody] ViewCharacter character)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data.");
            _logger.LogInformation($"{character.CharacterId} was updated in store");
            var updatedCharacter = await _repo.Update(character);
            return Ok(updatedCharacter);
        }

        //// DELETE api/<CharacterController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
