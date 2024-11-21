
namespace gamesssss.Models
{
    public class Device : BaseEntity
    {
        [MaxLength(1000)]
        public string Icon {  get; set; }
    }
}