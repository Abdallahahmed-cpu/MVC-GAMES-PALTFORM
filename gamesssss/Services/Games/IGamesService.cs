using gamesssss.ViewModels.Games;

namespace gamesssss.Services.Games
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFormVm model);
        Task<Game?> Update(EditGameVM model);
        bool Delete(int id);
       
    }
}
