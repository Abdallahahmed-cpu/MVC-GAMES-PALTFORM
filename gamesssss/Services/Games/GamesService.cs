using gamesssss.Models;
using gamesssss.Settings;
using gamesssss.ViewModels.Games;

namespace gamesssss.Services.Games
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgpath;



        public IEnumerable<Game> GetAll()
        {
            var games = _context.Games.AsNoTracking().Include(g=>g.Category).Include(g=>g.Devices).ThenInclude(d=>d.dvice).ToList();
            return games;
        }

        public Game? GetById(int id)
        {
            return _context.Games.
                AsNoTracking().
                Include(g => g.Category).
                Include(g => g.Devices).
                ThenInclude(d => d.dvice).SingleOrDefault(g=>g.Id==id);

        }

        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imgpath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImgsPath}";
        }

        public async Task Create(CreateGameFormVm model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DviceId = d }).ToList()

            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameVM model)
        {
            var game = _context.Games.Include(g=>g.Devices)
                .SingleOrDefault(g=> g.Id==model.Id);   

            if (game is null)

                return null;

            var HasnNewcover = model.Cover is not null;
            var oldcover = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DviceId = d }).ToList();

            if (HasnNewcover)
            {
                game.Cover = await SaveCover(model.Cover);
            }
            var EffectedRows= _context.SaveChanges();



            if(EffectedRows > 0)
            {
                if (HasnNewcover)
                {
                    var cover = Path.Combine(_imgpath, oldcover);
                    File.Delete(cover);
                }
                return game;
            }
            else 
            {
                var cover = Path.Combine(_imgpath, game.Cover);
                File.Delete(cover);
                return null;
            }
           
        }
        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = _context.Games.Find(id);

            if (game is null)

             return isDeleted;
            _context.Remove(game);

            var EffectedRows = _context.SaveChanges();
            if (EffectedRows > 0) 
            {
                isDeleted= true;
                var cover = Path.Combine(_imgpath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }
        private async Task<string> SaveCover (IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imgpath, coverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return coverName;
        }

      
    }
}
