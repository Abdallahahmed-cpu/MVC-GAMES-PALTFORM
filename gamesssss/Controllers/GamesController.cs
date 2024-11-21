using gamesssss.Services.Categories;
using gamesssss.Services.Devices;
using gamesssss.Services.Games;
using gamesssss.ViewModels.Games;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace gamesssss.Controllers
{
   
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService _categoriesServices;
        private readonly IDevicesServe _devicesServices;
        private readonly IGamesService _gamesServices;
        public GamesController(ApplicationDbContext context, ICategoriesService categoriesServices, IDevicesServe devicesServes,IGamesService gamesServices)
        {
            _context = context;
            _categoriesServices = categoriesServices;
            _devicesServices = devicesServes;
            _gamesServices = gamesServices;
        }

        public IActionResult Index(string searchQuery)
        {
            var games = _context.Games
                                .Include(g => g.Category)    // Include Category
                                .Include(g => g.Devices)     // Include Devices
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                games = games.Where(g => g.Name.Contains(searchQuery) || g.Description.Contains(searchQuery));
            }

            return View(games.ToList());
        }
        [HttpPost]
        public IActionResult SubmitRating(int gameId, int rating, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return Json(new { success = true});
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = new Review
            {
                GameId = gameId,
                UserId = userId,
                Rating = rating,
                Comment = comment, 
                DateCreated = DateTime.Now
            };

            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        public IActionResult Details(int id)
        {
            var game = _context.Games
                .Include(g => g.Reviews)
                .ThenInclude(r => r.User) 
                .FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }



        public IActionResult GetReviews(int gameId)
        {
            var reviews = _context.Reviews
                                  .Where(r => r.GameId == gameId)
                                  .Include(r => r.User) 
                                  .ToList();

          
            return View("Reviews", reviews); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormVm viewModel = new()
            {
                Categroies = _categoriesServices.GetSelectListItems(),
                Devices = _devicesServices.GetSelectListItems()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(int gameId, [FromBody] Review review)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == gameId);
            if (game == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            review.GameId = gameId;
            review.UserId = userId;
            review.DateCreated = DateTime.Now;

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Json(new { success = true });
        }
      
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var game = _gamesServices.GetById(id);

            if (game is null)

            return NotFound();

            EditGameVM viewModel = new() 
            {
              Id = id,
              Name = game.Name,
              Description = game.Description,
              CategoryId = game.CategoryId,
              SelectedDevices = game.Devices.Select(d=>d.DviceId).ToList(),
              Categroies = _categoriesServices.GetSelectListItems(),
              Devices = _devicesServices.GetSelectListItems(),
              CurruntCover= game.Cover
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categroies = _categoriesServices.GetSelectListItems();
                model.Devices = _devicesServices.GetSelectListItems();
                return View(model);
            }

            var game = await _gamesServices.Update(model);
            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
       
        
        
        [HttpDelete]

        public IActionResult Delete(int id) 
        {
            var isDeleted = _gamesServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchQuery))
            {
                // If no search query, return all games
                var games = _gamesServices.GetAll();
                return View("Index", games);
            }

            // If there's a search query, filter the games by Name or Description
            var gamesFiltered = _context.Games
                .Where(g => g.Name.Contains(model.SearchQuery) || g.Description.Contains(model.SearchQuery))
                .ToList();

            return View("Index", gamesFiltered); // Return the filtered games to the Index view
        }

    }
}