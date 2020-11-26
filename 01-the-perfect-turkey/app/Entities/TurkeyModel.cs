using System.ComponentModel.DataAnnotations;

namespace app
{
    public class TurkeyModel
    {
        [Required]
        [RegularExpression(@"^\d*\.?\d*$", ErrorMessage = "Please enter a numeric or decimal number.")]
        public string Weight { get; set; }
    }
}