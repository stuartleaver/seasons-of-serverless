using System.ComponentModel.DataAnnotations;

namespace app.Entities
{
    public class TteokGukMessage
    {
        public string UniqueTteokGukInstructionsId { get; set; }

        public string Message { get; set; }

        public int Duration { get; set; }

        [Required]
        public string EmailAddress { get; set; }
    }
}