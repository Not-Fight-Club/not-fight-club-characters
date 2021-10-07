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
    private readonly IRepository<ViewCharacter, int> _repo;
    private readonly ILogger<CharacterController> _logger;

    public CharacterController(IRepository<ViewCharacter, int> repo, ILogger<CharacterController> logger)
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
      var selectedCharacter = await _repo.Read(id);
      return Ok(selectedCharacter);
    }

    // POST api/<CharacterController>
    [HttpPost]
    public async Task<ActionResult<ViewCharacter>> Post([FromBody] ViewCharacter viewCharacter)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      //call to repository to add user
      //return the result
      //Console.WriteLine(viewUser);
      var createdCharacter = await _repo.Add(viewCharacter);
      _logger.LogInformation($"{createdCharacter.Name} created.");

      return Ok(createdCharacter);
    }
    //// PUT api/<CharacterController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<CharacterController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
  }
}
