using System;
using System.Collections.Generic;

#nullable disable

namespace CharactersApi_Data
{
  public partial class Trait
  {
    public Trait()
    {
      Characters = new HashSet<Character>();
    }

    public int TraitId { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Character> Characters { get; set; }
  }
}
