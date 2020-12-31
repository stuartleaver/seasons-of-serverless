using System.Collections.Generic;
using System.Threading.Tasks;
using MagicChocolateBox.Chocolates;
using MagicChocolateBox.Dto;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using MagicChocolateBox.Helpers;
using System.Linq;
using System;
using Microsoft.Azure.WebJobs;

namespace MagicChocolateBox.Api.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Family : IFamilyOperations
    {
        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("reservations")]
        public List<Reservation> Reservations { get; set; }

        [JsonConverter(typeof(ChocolateBoxConverter<ChocolateBox>))]
        [JsonProperty("chocolateBox")]
        public ChocolateBox ChocolateBox { get; set; }

        public void New(FamilyDto family)
        {
            FamilyName = family.FamilyName;

            Reservations = new List<Reservation>();

            ChocolateBox chocolateBox;

            switch (family.ChocolateBoxSize)
            {
                case ChocolateBoxSize.Small:
                default:
                    chocolateBox = new SmallChocolateBox();

                    break;

                case ChocolateBoxSize.Medium:
                    chocolateBox = new MediumChocolateBox();

                    break;

                case ChocolateBoxSize.Large:
                    chocolateBox = new LargeChocolateBox();

                    break;
            }

            chocolateBox.Create();

            ChocolateBox = chocolateBox;
        }

        public void AddReservation(ReservationDto reservation)
        {
            Reservations.Add(new Reservation
            {
                Name = reservation.Name,
                ChocolateName = reservation.ChocolateName,
                Quantity = reservation.Quantity,
                ReservedDateTime = DateTime.UtcNow
            });
        }

        public void UpdateRemainingChocolateQuantity(ReservationDto reservation)
        {
            ChocolateBox.Chocolates.Where(x => x.Name == reservation.ChocolateName).FirstOrDefault().Subtract(reservation.Quantity);
        }

        [FunctionName(nameof(Family))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx) => ctx.DispatchAsync<Family>();
    }
}