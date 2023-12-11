using UnityEngine;


public class Inventory : ItemContainer
{

    [SerializeField] protected Item[] startingItems;
    [SerializeField] protected Transform itemsParent;
    [SerializeField] protected Transform ActionParent;

    protected override void OnValidate()


    {


        if (itemsParent != null)
            itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);

        if (ActionParent != null)
            ActionParent.GetComponentsInChildren(includeInactive: true, result: ItemSlot1);

        ItemSlots.AddRange(ItemSlot1);

        if (!Application.isPlaying)
        {
            SetStartingItems();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetStartingItems();
    }

    private void SetStartingItems()
    {
        Clear();
        foreach (Item item in startingItems)
        {
            AddItem(item.GetCopy());
        }
    }

}
