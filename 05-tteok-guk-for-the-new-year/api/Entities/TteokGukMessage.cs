using System;

namespace TteokGuk.Api.Entities
{
    public class TteokGukMessage
    {
        public string UniqueTteokGukInstructionsId { get; set; }

        public string Message { get; set; }

        public int Duration { get; set; }

        public string EmailAddress { get; set; }
    }
}