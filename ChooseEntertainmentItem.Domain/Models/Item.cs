namespace ChooseEntertainmentItem.Domain.Models
{
    public class DoneItem : Item
    {
        public string DoneDate { get; set; }
    }

    public class BacklogItem : Item
    {
        private int _tier => RawTier == default ? 1 : RawTier;
        protected int _score { get; set; }

        public double Price { get; set; }
        public int RawTier { get; set; }

        public void AddScore(int value)
        {
            _score += value;
        }

        public int GetFinalScore() => _score / _tier;
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}