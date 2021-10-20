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
  public class CharacterRepository : IRepository<ViewCharacter, int>
  {
    private readonly P3_NotFightClub_CharactersContext _dbContext;
    private readonly IMapper<Character, ViewCharacter> _mapper;

    public CharacterRepository(IMapper<Character, ViewCharacter> mapper, P3_NotFightClub_CharactersContext dbContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }
    public async Task<ViewCharacter> Add(ViewCharacter viewCharacter)
    {
      Character character = _mapper.ViewModelToModel(viewCharacter);
      //add to the db
      //_dbContext.Database.ExecuteSqlInterpolated($"Insert into Character(name, baseform, traitId, weaponId, userId) values({character.Name},{character.Baseform},{character.TraitId},{character.WeaponId}, {character.UserId})");
      _dbContext.Characters.Add(character);
      //save changes
      await _dbContext.SaveChangesAsync();
      //read user back from the db
      Character createdCharacter = await _dbContext.Characters.FromSqlInterpolated($"select * from Character where UserId = {character.UserId} and name = {character.Name} and baseform = {character.Baseform}").FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(createdCharacter);
    }

    public async Task<ViewCharacter> Read(int id)
    {
      //this will not work because userId is not an integer
      //Character selectedCharacter = await _dbContext.Characters.FromSqlInterpolated($"select * from Character where CharacterId = {id}").FirstOrDefaultAsync();
      Character selectedCharacter = await (from c in _dbContext.Characters where c.CharacterId == id select c)
        .Include(c => c.Trait)
        .Include(c => c.Weapon)
        .FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(selectedCharacter);
    }

    public async Task<List<ViewCharacter>> Read()
    {
      var characters = await _dbContext.Characters.Include(c => c.Trait).Include(c => c.Weapon).ToListAsync();
      return characters.ConvertAll(_mapper.ModelToViewModel);
    }

    public Task<ViewCharacter> Update(ViewCharacter obj)
    {
      throw new NotImplementedException();
    }
  }
}
