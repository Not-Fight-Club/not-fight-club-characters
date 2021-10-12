using CharactersApi_Data;
using CharactersApi_Logic.Mappers;
using CharactersApi_Logic.Repositiories;
using CharactersApi_Models.ViewModels;
using CharactersApi_Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharactersApi_Test.RepositoryTests
{
  [Collection("Repository Tests")]
  public class CharacterRepositoryTests
  {

    private void AssertCharsEqual(ViewCharacter expected, ViewCharacter actual)
    {
      Assert.Equal(expected.Baseform, actual.Baseform);
      Assert.Equal(expected.Level, actual.Level);
      Assert.Equal(expected.Losses, actual.Losses);
      Assert.Equal(expected.Wins, actual.Wins);
      Assert.Equal(expected.Ties, actual.Ties);
      Assert.Equal(expected.Name, actual.Name);
      Assert.Equal(expected.TraitId, actual.TraitId);
      Assert.Equal(expected.UserId, actual.UserId);
      Assert.Equal(expected.WeaponId, actual.WeaponId);
    }
    private void AssertCharsEqual(ViewCharacter expected, Character actual)
    {
      Assert.Equal(expected.Baseform, actual.Baseform);
      Assert.Equal(expected.Level, actual.Level);
      Assert.Equal(expected.Losses, actual.Losses);
      Assert.Equal(expected.Wins, actual.Wins);
      Assert.Equal(expected.Ties, actual.Ties);
      Assert.Equal(expected.Name, actual.Name);
      Assert.Equal(expected.TraitId, actual.TraitId);
      Assert.Equal(expected.UserId, actual.UserId);
      Assert.Equal(expected.WeaponId, actual.WeaponId);
    }

    private Character[] GetCharsToInsert()
    {
      var charactersToInsert = new Character[]
      {
        new Character()
        {
          CharacterId = 1,
          Baseform = "test baseform 1",
          Level = 1,
          Losses = 1,
          Wins = 1,
          Ties = 1,
          Name = "test name 1",
          TraitId = 1,
          UserId = (Guid.NewGuid()),
          WeaponId = 1
        },
        new Character()
        {
          CharacterId = 2,
          Baseform = "test baseform 2",
          Level = 2,
          Losses = 2,
          Wins = 2,
          Ties = 2,
          Name = "test name 2",
          TraitId = 2,
          UserId = (Guid.NewGuid()),
          WeaponId = 2
        },
        new Character()
        {
          CharacterId = 3,
          Baseform = "test baseform 3",
          Level = 3,
          Losses = 3,
          Wins = 3,
          Ties = 3,
          Name = "test name 3",
          TraitId = 3,
          UserId = (Guid.NewGuid()),
          WeaponId = 3
        }
      };
      return charactersToInsert;
    }


    [Fact]
    public async void TestCharacterRepo_Insert()
    {
      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new CharacterMapper();
        var characterRepo = new CharacterRepository(mapper, mockDbContext);

        var charToInsert = new ViewCharacter()
        {
          Baseform = "test baseform",
          Level = 101,
          Losses = 10,
          Wins = 5,
          Ties = 6,
          Name = "test name",
          TraitId = 2,
          UserId = (Guid.NewGuid()),
          WeaponId = 4
        };

        var result = await characterRepo.Add(charToInsert);
        var dbChar = mockDbContext.Characters.Where(c => c.CharacterId == result.CharacterId).FirstOrDefault();

        Assert.NotNull(dbChar);
        AssertCharsEqual(charToInsert, result);
        AssertCharsEqual(charToInsert, dbChar);
      }
    }

    [Fact]
    public async void TestCharacterRepo_ReadAll()
    {
      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new CharacterMapper();
        var characterRepo = new CharacterRepository(mapper, mockDbContext);

        var charsToInsert = GetCharsToInsert();

        mockDbContext.Characters.AddRange(charsToInsert);
        mockDbContext.SaveChanges();

        var result = await characterRepo.Read();
        Assert.Equal(3, result.Count());
        Assert.Collection(result,
          (c1) => AssertCharsEqual(c1, charsToInsert[0]),
          (c2) => AssertCharsEqual(c2, charsToInsert[1]),
          (c3) => AssertCharsEqual(c3, charsToInsert[2])
        );
      }
    }


    [Fact]
    public async void TestCharacterRepo_ReadOne()
    {
      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new CharacterMapper();
        var characterRepo = new CharacterRepository(mapper, mockDbContext);

        var charsToInsert = GetCharsToInsert();

        mockDbContext.Characters.AddRange(charsToInsert);
        mockDbContext.SaveChanges();

        var result = await characterRepo.Read(2);
        Assert.NotNull(result);
        Assert.Equal(2, result.CharacterId);
        AssertCharsEqual(result, charsToInsert[1]);
      }
    }

  }
}
