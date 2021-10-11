using Microsoft.EntityFrameworkCore;
using CharactersApi_Data;
using CharactersApi_Logic.Interfaces;
using CharactersApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersApi_Logic.Repositiories
{
  public class TraitRepository : IRepository<ViewTrait, int>
  {
    private readonly P3_NotFightClub_CharactersContext _dbContext;
    private readonly IMapper<Trait, ViewTrait> _mapper;

    public TraitRepository(IMapper<Trait, ViewTrait> mapper, P3_NotFightClub_CharactersContext dbContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }


    public async Task<ViewTrait> Add(ViewTrait viewTrait)
    {
      //check if the trait already exists if so decline the entry ( implement later)
      //convert to Trait with Mapper class
      Trait trait = _mapper.ViewModelToModel(viewTrait);
      //add to the db
      //_dbContext.Database.ExecuteSqlInterpolated($"Insert into Trait(Description) values({trait.Description})");
      _dbContext.Traits.Add(trait);
      //save changes
      await _dbContext.SaveChangesAsync();
      //read trait back from the db
      //Trait newTrait = await _dbContext.Traits.FromSqlInterpolated($"select * from Trait where Description = {trait.Description}").FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(trait);
    }


    public async Task<ViewTrait> Read(int id)
    {

      Trait trait = await _dbContext.Traits.FromSqlInterpolated($"select * from Trait where TraitId = {id}").FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(trait);
    }



    public async Task<List<ViewTrait>> Read()
    {

      List<Trait> traits = await _dbContext.Traits.ToListAsync();
      return _mapper.ModelToViewModel(traits);

    }


    /*
        public Task<List<Fight>> ReadFight(){
          throw new NotImplementedException();
        }
    */

    public Task<ViewTrait> Update(ViewTrait obj)
    {
      throw new NotImplementedException();
    }
  }
}

