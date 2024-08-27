namespace ChooseEntertainmentItem.Domain.Models
{
    public class DoneItem : Item
    {
        public string DoneDate { get; set; }
    }

    public class BacklogItem : Item
    {
        protected int _score { get; set; }

        public double Price { get; set; }

        public void AddScore(int value)
        {
            _score += value;
        }

        public int GetFinalScore() => _score;
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}