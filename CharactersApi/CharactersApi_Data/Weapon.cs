using System;
using System.Collections.Generic;

#nullable disable

namespace CharactersApi_Data
{
  public partial class Weapon
  {
    public Weapon()
    {
      Characters = new HashSet<Character>();
    }

    public int WeaponId { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Character> Characters { get; set; }
  }
}
