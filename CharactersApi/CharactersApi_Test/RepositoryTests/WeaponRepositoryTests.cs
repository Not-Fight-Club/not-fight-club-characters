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
  public class WeaponRepositoryTests
  {

    [Fact]
    public async Task TestWeaponRepo_Insert()
    {

      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new WeaponMapper();
        var weaponRepo = new WeaponRepository(mapper, mockDbContext);

        var weaponToInsert = new ViewWeapon()
        {
          Description = "test weapon 1",
        };

        var result = await weaponRepo.Add(weaponToInsert);

        var dbResult = mockDbContext.Weapons.ToList();

        Assert.Single(dbResult);

        var dbWeapon = dbResult[0];
        Assert.Equal(weaponToInsert.Description, result.Description);
        Assert.Equal(weaponToInsert.Description, dbWeapon.Description);
        Assert.Equal(dbWeapon.WeaponId, result.WeaponId);
      }
    }

    [Fact]
    public async Task TestWeaponRepo_ReadAll()
    {

      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new WeaponMapper();
        var weaponRepo = new WeaponRepository(mapper, mockDbContext);

        var weaponsToInsert = new Weapon[]
        {
          new Weapon() { Description = "test weapon 1", WeaponId = 1 },
          new Weapon() { Description = "test weapon 2", WeaponId = 2 },
          new Weapon() { Description = "test weapon 3", WeaponId = 3 },
        };

        mockDbContext.Weapons.AddRange(weaponsToInsert);
        mockDbContext.SaveChanges();

        var results = await weaponRepo.Read();

        Assert.Equal(3, results.Count());

        Assert.Collection(results,
          (w1) => Assert.Equal(weaponsToInsert[0].Description, w1.Description),
          (w2) => Assert.Equal(weaponsToInsert[1].Description, w2.Description),
          (w3) => Assert.Equal(weaponsToInsert[2].Description, w3.Description)
        );

      }
    }

    [Fact]
    public async Task TestWeaponRepo_ReadOne()
    {

      using (var mockDbContext = CharacterDBMock.GetMockDB())
      {
        await mockDbContext.Database.EnsureDeletedAsync();
        await mockDbContext.Database.EnsureCreatedAsync();

        var mapper = new WeaponMapper();
        var weaponRepo = new WeaponRepository(mapper, mockDbContext);

        var weaponsToInsert = new Weapon[]
        {
          new Weapon() { Description = "test weapon 1", WeaponId = 1 },
          new Weapon() { Description = "test weapon 2", WeaponId = 2 },
          new Weapon() { Description = "test weapon 3", WeaponId = 3 },
        };

        mockDbContext.Weapons.AddRange(weaponsToInsert);
        mockDbContext.SaveChanges();

        var weapon = await weaponRepo.Read(1);
        Assert.Equal(weaponsToInsert[0].Description, weapon.Description);

        weapon = await weaponRepo.Read(2);
        Assert.Equal(weaponsToInsert[1].Description, weapon.Description);

        weapon = await weaponRepo.Read(6);
        Assert.Null(weapon);
      }
    }
  }
}
