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
  public class TraitRepositoryTests
  {

    [Fact]
    public async void TraitInsert_Test()
    {
      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new TraitMapper();
        var traitRepo = new TraitRepository(mapper, mockDbContext);

        var traitToAdd = new ViewTrait()
        {
          Description = "This is a test description",
          TraitId = 0
        };

        var result = await traitRepo.Add(traitToAdd);

        // test that returned result has correct data
        Assert.Equal(traitToAdd.Description, result.Description);
        Assert.NotEqual(0, result.TraitId);

        // test that database also has correct data
        var dbResult = mockDbContext.Traits.ToList();
        Assert.Single(dbResult);
        Assert.Equal(traitToAdd.Description, dbResult[0].Description);
        Assert.NotEqual(0, dbResult[0].TraitId);
      }
    }

    [Fact]
    public async void TraitReadAll_Test()
    {
      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new TraitMapper();
        var traitRepo = new TraitRepository(mapper, mockDbContext);

        Trait[] traitsToInsert = new Trait[]
        {
          new Trait()
          {
            Description = "test trait 1",
            TraitId = 1
          },
          new Trait()
          {
            Description = "test trait 2",
            TraitId = 2
          },
          new Trait()
          {
            Description = "test trait 3",
            TraitId = 3
          }
        };

        await mockDbContext.Traits.AddRangeAsync(traitsToInsert);
        await mockDbContext.SaveChangesAsync();

        var traits = await traitRepo.Read();

        Assert.Equal(3, traits.Count());
        Assert.Collection(traits,
          (t1) => Assert.Equal(traitsToInsert[0].Description, t1.Description),
          (t2) => Assert.Equal(traitsToInsert[1].Description, t2.Description),
          (t3) => Assert.Equal(traitsToInsert[2].Description, t3.Description)
        );
      }
    }

    [Fact]
    public async void TraitReadOne_Test()
    {
      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        Trait[] traitsToInsert = new Trait[]
        {
          new Trait()
          {
            Description = "test trait 1",
            TraitId = 1
          },
          new Trait()
          {
            Description = "test trait 2",
            TraitId = 2
          },
          new Trait()
          {
            Description = "test trait 3",
            TraitId = 3
          }
        };

        await mockDbContext.Traits.AddRangeAsync(traitsToInsert);
        await mockDbContext.SaveChangesAsync();

        var mapper = new TraitMapper();
        var traitRepo = new TraitRepository(mapper, mockDbContext);

        // get second trait
        var trait1 = await traitRepo.Read(2);
        Assert.Equal(traitsToInsert[1].Description, trait1.Description);

        // get nonexistent trait
        var trait2 = await traitRepo.Read(5);
        Assert.Null(trait2);
      }
      //Assert.False(true);
    }
  }
}
