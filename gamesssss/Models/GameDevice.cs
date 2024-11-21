namespace gamesssss.Models
{
    public class GameDevice
    {
        public int GameId { get; set; }
        public Game game { get; set; }

        public int DviceId { get; set; }
        public Device dvice { get; set; }
    }
}
