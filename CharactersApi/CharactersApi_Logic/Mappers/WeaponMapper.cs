using CharactersApi_Logic.Interfaces;
using CharactersApi_Data;
using CharactersApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersApi_Logic.Mappers
{
  public class WeaponMapper : IMapper<Weapon, ViewWeapon>
  {
    public ViewWeapon ModelToViewModel(Weapon obj)
    {
      if (obj == null) return null;
      ViewWeapon weapon = new ViewWeapon();
      weapon.Description = obj.Description;
      weapon.WeaponId = obj.WeaponId;

      return weapon;
    }

    public List<ViewWeapon> ModelToViewModel(List<Weapon> obj)
    {
      throw new NotImplementedException();
    }

    public Weapon ViewModelToModel(ViewWeapon obj)
    {
      if (obj == null) return null;
      Weapon weapon = new Weapon();
      weapon.Description = obj.Description;
      weapon.WeaponId = obj.WeaponId;

      return weapon;
    }

    public List<Weapon> ViewModelToModel(List<ViewWeapon> obj)
    {
      throw new NotImplementedException();
    }
  }
}
