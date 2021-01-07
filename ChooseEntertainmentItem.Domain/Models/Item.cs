namespace ChooseEntertainmentItem.Domain.Models
{
    public class DoneItem : Item
    {
        public string DoneDate { get; set; }
    }

    public class BacklogItem : Item
    {
        public double Price { get; set; }
        public int Score { get; set; }
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}