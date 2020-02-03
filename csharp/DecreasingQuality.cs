namespace csharp
{
    public class DecreasingQuality
    {
        public int CalculateNewQuality(Item item)
        {
            return CalculateNewQuality(item, item.Quality);
        }

        public int CalculateNewQuality(Item item, int quality)
        {
            var newQuality = quality;

            newQuality -= QualityDecreaseStep;

            if (item.Name == ProductNames.Conjured)
                newQuality -= QualityDecreaseStep;

            if (item.Name.Contains(ProductAttributes.Summoned))
                newQuality -= 2 * QualityDecreaseStep;

            if (newQuality < Item.MinQuality) newQuality = Item.MinQuality;

            return newQuality;
        }

        public const int QualityDecreaseStep = 1;
    }
}