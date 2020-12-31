using MagicChocolateBox.Dto;

namespace MagicChocolateBox.Api.Entities
{
    public interface IFamilyOperations
    {
        void New(FamilyDto newFamily);

        void AddReservation(ReservationDto reservation);

        void UpdateRemainingChocolateQuantity(ReservationDto reservation);
    }
}