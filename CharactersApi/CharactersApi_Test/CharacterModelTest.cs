using CharactersApi_Logic.Interfaces;
using CharactersApi_Logic.Mappers;
using CharactersApi_Data;
using CharactersApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace NotFightClub_Test
{
  public class CharacterModelTest
  {
        
    [Fact]
    public void MappertoModel ()
    {
            //Arrange
            ViewCharacter c = new ViewCharacter();
            c.Baseform = "robot";
            c.CharacterId = 0;
            c.Level = 0;
            c.Losses = 0;
            c.Name = "ben";
            c.Ties = 9;
            c.TraitId = 5;
            c.UserId = new Guid();
            c.WeaponId = 43;
            c.Wins = 3;
            
            IMapper<Character, ViewCharacter> _mapper = new CharacterMapper();
            //Act
            Character c1 = _mapper.ViewModelToModel(c);
            //Assert
            Assert.Equal("robot", c1.Baseform);
            Assert.Equal("ben", c1.Name);
            Assert.Equal(c.UserId, c1.UserId);
            Assert.Equal(43, c1.WeaponId);

        }
        [Fact]
        public void MappertoViewModel()
        {
            //Arrange
            Character c = new Character();
            c.Baseform = "robot";
            c.CharacterId = 0;
            c.Level = 0;
            c.Losses = 0;
            c.Name = "ben";
            c.Ties = 9;
            c.TraitId = 5;
            c.UserId = new Guid();
            c.WeaponId = 43;
            c.Wins = 3;

            IMapper<Character, ViewCharacter> _mapper = new CharacterMapper();
            //Act
            ViewCharacter c1 = _mapper.ModelToViewModel(c);
            //Assert
            Assert.Equal("robot", c1.Baseform);
            Assert.Equal("ben", c1.Name);
            Assert.Equal(c.UserId, c1.UserId);
            Assert.Equal(43, c1.WeaponId);

        }

        //public void MappertoModelList()
        //{
        //    //Arrange
        //    ViewCharacter c = new ViewCharacter();
        //    c.Baseform = "robot";
        //    c.CharacterId = 0;
        //    c.Level = 0;
        //    c.Losses = 0;
        //    c.Name = "ben";
        //    c.Ties = 9;
        //    c.TraitId = 5;
        //    c.UserId = new Guid();
        //    c.WeaponId = 43;
        //    c.Wins = 3;

        //    ViewCharacter c2 = new ViewCharacter();
        //    c.Baseform = "human";
        //    c.CharacterId = 2;
        //    c.Level = 0;
        //    c.Losses = 0;
        //    c.Name = "jerry";
        //    c.Ties = 9;
        //    c.TraitId = 5;
        //    c.UserId = new Guid();
        //    c.WeaponId = 2;
        //    c.Wins = 9;

        //    List<ViewCharacter> list = new List<ViewCharacter>() { c, c2 };
        //    IMapper<Character, ViewCharacter> _mapper = new CharacterMapper();
        //    //Act
        //    Character c1 = _mapper.ViewModelToModel(list);
        //    //Assert
        //    Assert.Equal("robot", c1.Baseform);
        //    Assert.Equal("ben", c1.Name);
        //    Assert.Equal(c.UserId, c1.UserId);
        //    Assert.Equal(43, c1.WeaponId);

        //}
    }
}
