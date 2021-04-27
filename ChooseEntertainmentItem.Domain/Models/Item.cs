namespace ChooseEntertainmentItem.Domain.Models
{
    // TODO: set all as protected or private set
    public class DoneItem : Item
    {
        public string DoneDate { get; set; }
    }

    public class BacklogItem : Item
    {
        public double Price { get; set; }
        // TODO: score should be in a dto
        public int Score { get; set; }
    }

    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}