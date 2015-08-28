using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is empty")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is empty")]
        public string Description { get; set; }
        
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is empty")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pleas enter correct price value")]
        public decimal Price { get; set; }
        
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}