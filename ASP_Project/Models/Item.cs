using System.ComponentModel.DataAnnotations;

namespace ASP_net_Project.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        public bool IsAvailable { get; set; }

        public int UserId { get; set; }
    }
}
