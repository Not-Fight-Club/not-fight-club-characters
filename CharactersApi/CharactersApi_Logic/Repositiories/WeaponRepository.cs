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
  public class WeaponRepository : IRepository<ViewWeapon, int>
  {
    private readonly P3_NotFightClub_CharactersContext _dbContext;
    private readonly IMapper<Weapon, ViewWeapon> _mapper;

    public WeaponRepository(IMapper<Weapon, ViewWeapon> mapper, P3_NotFightClub_CharactersContext dbContext)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }
    public async Task<ViewWeapon> Add(ViewWeapon obj)
    {
      Weapon weapon = _mapper.ViewModelToModel(obj);
      _dbContext.Add(weapon);
      _dbContext.SaveChanges();

      Weapon addedWeapon = await _dbContext.Weapons.FromSqlInterpolated($"Select * from Weapon where description = {weapon.Description}").FirstOrDefaultAsync();
      return _mapper.ModelToViewModel(addedWeapon);
    }

    public async Task<ViewWeapon> Read(int obj)
    {
      //Weapon weapon = await _dbContext.Weapons.FromSqlInterpolated($"select * from Weapon where WeaponId = {obj}").FirstOrDefaultAsync();
      Weapon weapon = await _dbContext.Weapons.Where(w => w.WeaponId == obj).FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(weapon);
    }

    public async Task<List<ViewWeapon>> Read()
    {
      var weapons = await _dbContext.Weapons.ToListAsync();
      return weapons.ConvertAll(_mapper.ModelToViewModel);
    }

    public Task<ViewWeapon> Update(ViewWeapon obj)
    {
      throw new NotImplementedException();
    }
  }
}
