using CharactersApi_Logic.Interfaces;
using CharactersApi_Data;
using CharactersApi_Models.ViewModels;
using System;
using System.Collections.Generic;

using System.Globalization;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersApi_Logic.Mappers
{
  public class TraitMapper : IMapper<Trait, ViewTrait>
  {

    public ViewTrait ModelToViewModel(Trait trait)
    {
      ViewTrait viewTrait = new ViewTrait();
      viewTrait.TraitId = trait.TraitId;
      viewTrait.Description = trait.Description;

      return viewTrait;
    }

    public Trait ViewModelToModel(ViewTrait viewTrait)
    {
      Trait trait = new Trait();
      trait.Description = viewTrait.Description;

      return trait;
    }


    public List<ViewTrait> ModelToViewModel(List<Trait> obj)
    {
      List<ViewTrait> traits = new List<ViewTrait>();
      for (int i = 0; i < obj.Count; i++)
      {
        ViewTrait t = new ViewTrait(
        obj[i].TraitId,
        obj[i].Description
        );
        traits.Add(t);
      }

      return traits;
    }


    public List<Trait> ViewModelToModel(List<ViewTrait> obj)
    {
      List<Trait> traits = new List<Trait>(obj.Count);
      for (int i = 0; i < obj.Count; i++)
      {
        traits[i].TraitId = obj[i].TraitId;
        traits[i].Description = obj[i].Description;
      }

      return traits;
    }

  }
}
