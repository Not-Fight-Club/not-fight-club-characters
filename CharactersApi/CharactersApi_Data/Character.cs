using System;
using System.Collections.Generic;

#nullable disable

namespace CharactersApi_Data
{
  public partial class Character
  {
    public int CharacterId { get; set; }
    public string Name { get; set; }
    public int? Level { get; set; }
    public int? Wins { get; set; }
    public int? Losses { get; set; }
    public int? Ties { get; set; }
    public string Baseform { get; set; }
    public Guid UserId { get; set; }
    public int TraitId { get; set; }
    public int WeaponId { get; set; }

    public virtual Trait Trait { get; set; }
    public virtual Weapon Weapon { get; set; }
  }
}
