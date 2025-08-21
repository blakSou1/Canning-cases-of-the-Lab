
public class ListElementRouletteItem : ListElement
{
    private BankaObject myItem;
    public void SetItem(BankaObject _item) => myItem = _item;
    public BankaObject GetItem() => myItem;
}
