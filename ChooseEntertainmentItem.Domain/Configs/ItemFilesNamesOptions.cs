namespace ChooseEntertainmentItem.Domain.Configs;
public class ItemsFilesNamesOptions
{
    public string Path { get; set; }
    public string BacklogFileName { get; set; }
    public string DoneFileName { get; set; }
}

public class ItemsFiltersOptions
{
    public bool ShouldIncludePrice { get; set; }
    public string ItemType { get; set; }
}