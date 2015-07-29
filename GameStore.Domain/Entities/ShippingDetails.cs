using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter correct address")]
        [Display(Name = "First address")]
        public string Line1 { get; set; }

        [Display(Name = "Second address")]
        public string Line2 { get; set; }

        [Display(Name = "Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Enter City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
