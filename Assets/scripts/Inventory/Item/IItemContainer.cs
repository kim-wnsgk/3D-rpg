
public interface IItemContainer
{
	bool CanAddItem(Item item, int amount = 1);
	bool AddItem(Item item);

	bool AddItem2(Item item, int amount = 1);

	Item RemoveItem(string itemID);
	bool RemoveItem(Item item);

    bool RemoveItem2(string item, int amount=1);

    void Clear();

	int ItemCount(string itemID);
}
