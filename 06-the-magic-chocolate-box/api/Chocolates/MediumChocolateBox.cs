namespace MagicChocolateBox.Chocolates
{
    public class MediumChocolateBox : ChocolateBox
    {
        public MediumChocolateBox() : base(ChocolateBoxSize.Medium) { }

        public override void Create()
        {
            Chocolates.Add(new Chocolate
            {
                Name = "Ambanje",
                Quantity = 5
            });

            Chocolates.Add(new Chocolate
            {
                Name = "Black cube",
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
                Name = "Hypoxia",
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