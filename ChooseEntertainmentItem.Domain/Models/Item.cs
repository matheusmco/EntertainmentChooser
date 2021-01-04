namespace ChooseEntertainmentItem.Domain.Models
{
    public class DoneItem : Item
    {
        public string DoneDate { get; set; }
    }

    public class BacklogItem : Item
    {
        public double Price { get; set; }
        // TODO: create csv mapper - this shoudn't be on model
        //[Ignore]
        public int Score { get; set; }
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}