namespace csharp
{
    public class Item
    {
        private DecreasingQuality _agingBehavior;
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public const int MinQuality = 0;

        public const int MaxQuality = 50;

        public const int QualityIncreaseStep = 1;

        public Item()
        {
            _agingBehavior = new DecreasingQuality();
        }

        public override string ToString()
        {
            return this.Name + ", " + this.SellIn + ", " + this.Quality;
        }

        public void DecreaseQuality()
        {
            Quality = _agingBehavior.CalculateNewQuality(this);
        }

        public void IncreaseQuality()
        {
            if (Quality < MaxQuality) Quality += QualityIncreaseStep;
        }
    }
}
