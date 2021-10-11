using CharactersApi_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersApi_Test.Mocks
{
  public static class CharacterDBMock
  {
    private static DbContextOptions<P3_NotFightClub_CharactersContext> _opts =
      new DbContextOptionsBuilder<P3_NotFightClub_CharactersContext>()
          .UseInMemoryDatabase("NotFightClubDB")
          .Options;

    public static P3_NotFightClub_CharactersContext GetMockDB()
    {
      return new P3_NotFightClub_CharactersContext(_opts);
    }
  }
}
