using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class CreateItemViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Line length exceeded.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        [StringLength(20, ErrorMessage = "Line length exceeded.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "The Short Description field is required.")]
        [StringLength(200, ErrorMessage = "Line length exceeded.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "The Long Description field is required.")]
        [StringLength(500, ErrorMessage = "Line length exceeded.")]
        public string LongDescription { get; set; }

        [Required]
        [RegularExpression("([0-9]{0,6})(.?)([0-9]{1,2})", ErrorMessage = "Incorrect format.")]
        public string Price { get; set; }

        [Required]
        [RegularExpression("[0-9]{1,5}", ErrorMessage = "Incorrect format.")]
        public string Quentity { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
