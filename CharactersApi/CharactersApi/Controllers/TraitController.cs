using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CharactersApi.Dtos;
//using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharactersApi_Models.ViewModels;
using CharactersApi_Logic.Interfaces;
using CharactersApi_Data;

namespace CharactersApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TraitController : Controller
  {
    private readonly IRepository<ViewTrait, int> _repo;
    private readonly ILogger<TraitController> _logger;


    public TraitController(IRepository<ViewTrait, int> repo, ILogger<TraitController> logger)
    {
      _repo = repo;
      _logger = logger;

    }

    // GET: api/<TraitController>
    [HttpGet]
    public async Task<IEnumerable<ViewTrait>> GetAllTraits()
    {
      return await _repo.Read();
    }

    // GET api/traits/5
    [HttpGet("/Trait/{id}")]
    public async Task<ActionResult<Trait>> GetTraitById(int id)
    {
      //var trait = await _context.Traits.FindAsync(id);
      ViewTrait trait = await _repo.Read(id);
      //return trait;
      return Ok(trait);
    }

    // POST api/<TraitController>
    [HttpPost]
    public async Task<ActionResult<ViewTrait>> Post([FromBody] ViewTrait viewTrait)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      //call to repository to add trait
      //return the result
      //Console.WriteLine(viewTrait);
      var newTrait = await _repo.Add(viewTrait);
      _logger.LogInformation($"Trait created: {newTrait.Description}");

      return Ok(newTrait);
    }
  }
}

