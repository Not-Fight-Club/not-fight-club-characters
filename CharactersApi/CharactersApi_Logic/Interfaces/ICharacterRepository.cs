using CharactersApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersApi_Logic.Interfaces
{
    public interface ICharacterRepository : IRepository<ViewCharacter, int>
    {

        public Task<List<ViewCharacter>> ReadAll(Guid userId);
    }
}
