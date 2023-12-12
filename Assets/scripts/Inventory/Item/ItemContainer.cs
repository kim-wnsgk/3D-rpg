using System;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Progress;

public delegate void ItemCountChanged(Item item);

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
	public List<ItemSlot> ItemSlots;

	public List<ItemSlot> ItemSlot1;

	public event ItemCountChanged ItemCountChangedEvent;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;
	public event Action<BaseItemSlot> OnLeftClickEvent;
	public event Action<BaseItemSlot> OnBeginDragEvent;
	public event Action<BaseItemSlot> OnEndDragEvent;
	public event Action<BaseItemSlot> OnDragEvent;
	public event Action<BaseItemSlot> OnDropEvent;


	public event Action<BaseItemSlot> OnRightClickDropEvent;

	protected virtual void OnValidate()
	{
		GetComponentsInChildren(includeInactive: true, result: ItemSlots);
	}

	protected virtual void Awake()
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			ItemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot, OnPointerEnterEvent);
			ItemSlots[i].OnPointerExitEvent += slot => EventHelper(slot, OnPointerExitEvent);
			if (i < ItemSlots.Count - 5)
			{
				ItemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickEvent);
			}

			ItemSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);

			ItemSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
			ItemSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
			ItemSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);

		}

		if (ItemSlots.Count > 10)
		{
			for (int i = ItemSlots.Count - 4; i < ItemSlots.Count; i++)
			{
				if (ItemSlots[i] != null)
				{
					ItemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickDropEvent);
					ItemSlots[i].OnLeftClickEvent += slot => EventHelper(slot, OnLeftClickEvent);

				}
			}

		}
	}

	private void EventHelper(BaseItemSlot itemSlot, Action<BaseItemSlot> action)
	{
		if (action != null)
			action(itemSlot);
	}

	public virtual bool CanAddItem(Item item, int amount = 1)
	{
		int freeSpaces = 0;

		foreach (ItemSlot itemSlot in ItemSlots)
		{
			if (itemSlot.Item == null || itemSlot.Item.ID == item.ID)
			{
				freeSpaces += item.MaximumStacks - itemSlot.Amount;
			}
		}
		return freeSpaces >= amount;
	}

	public virtual bool AddItem(Item item)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].CanAddStack(item))
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount++;
				OnItemCountChanged(item);
				return true;
			}
		}

		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == null)
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount++;
				OnItemCountChanged(item);
				return true;
			}
		}
		return false;
	}

	public virtual bool AddItem2(Item item, int amount = 1)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].CanAddStack(item, amount))
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount += amount;
				OnItemCountChanged(item);
				return true;
			}
		}

		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == null)
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount += amount;
				OnItemCountChanged(item);
				return true;
			}
		}
		return false;
	}

	public virtual bool RemoveItem(Item item)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == item)
			{
				ItemSlots[i].Amount--;
				return true;
			}
		}
		return false;
	}

	public virtual Item RemoveItem(string itemID)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				ItemSlots[i].Amount--;
				return item;
			}
		}
		return null;
	}

	public virtual bool RemoveItem2(string itemID, int amount = 1)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID && ItemCount(itemID) >= amount)
			{
				ItemSlots[i].Amount -= amount;
				return item;
			}
		}
		return false;
	}

	public virtual int ItemCount(string itemID)
	{
		int number = 0;

		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				number += ItemSlots[i].Amount;
			}
		}
		return number;
	}


	public void Clear()
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item != null && Application.isPlaying)
			{
				ItemSlots[i].Item.Destroy();
			}
			ItemSlots[i].Item = null;
			ItemSlots[i].Amount = 0;
		}
	}

	public void OnItemCountChanged(Item item)
	{

		if (ItemCountChangedEvent != null)
		{

			ItemCountChangedEvent.Invoke(item);
		}
	}
}
