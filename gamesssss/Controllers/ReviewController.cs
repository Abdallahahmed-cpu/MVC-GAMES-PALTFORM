public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Review/Create
    public IActionResult Create(int gameId)
    {
        // Pass the GameId to the view to create a review for a specific game
        ViewData["GameId"] = gameId;
        return View();
    }

    // POST: Review/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Rating, Comment, GameId")] Review review)
    {
        if (ModelState.IsValid)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            review.UserId = user.Id;
            review.DateCreated = DateTime.Now;

            // Add the review to the database
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Games", new { id = review.GameId });
        }
        return View(review);
    }
}
