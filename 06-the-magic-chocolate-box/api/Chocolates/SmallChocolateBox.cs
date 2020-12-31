namespace MagicChocolateBox.Chocolates
{
    public class SmallChocolateBox : ChocolateBox
    {
        public SmallChocolateBox() : base(ChocolateBoxSize.Small) { }

        public override void Create()
        {
            Chocolates.Add(new Chocolate
            {
                Name = "Ambanje",
                Quantity = 5
            });

            Chocolates.Add(new Chocolate
            {
                Name = "Delhi",
                Quantity = 5
            });

            Chocolates.Add(new Chocolate
            {
                Name = "Discreet",
                Quantity = 5
            });

            Chocolates.Add(new Chocolate
            {
                Name = "Kathmandu",
                Quantity = 5
            });

            Chocolates.Add(new Chocolate
            {
                Name = "Milk cube",
                Quantity = 5
            });
        }
    }
}