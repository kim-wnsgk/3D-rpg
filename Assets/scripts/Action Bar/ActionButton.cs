/*
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;




public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{

    public IUseable MyUseable { get; set; }


    public Button MyActionButton { get; private set; }


    [SerializeField]
    private Image icon = default;


    public Image MyIcon { get => icon; set => icon = value; }


    private Stack<IUseable> useables = new Stack<IUseable>();


    public Stack<IUseable> MyUseables
    {
        get => useables;
        set
        {

            if (value.Count > 0)
            {

                MyUseable = value.Peek();
            }
            else
            {

                MyUseable = null;
            }

            useables = value;
        }
    }


    private int count;


    public int MyCount { get => count; }


    [SerializeField]
    private Text stackSize = default;


    public Text MyStackText { get => stackSize; }



    private void Start()
    {

        MyActionButton = GetComponent<Button>();


        MyActionButton.onClick.AddListener(OnClick);


        InventoryScript.MyInstance.ItemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }


    public void OnClick()
    {

        if (Hand.MyInstance.MyMoveable == null)
        {

            if (MyUseable != null)
            {

                MyUseable.Use();
            }

            else if (MyUseables != null && MyUseables.Count > 0)
            {

                MyUseables.Peek().Use();
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {

            if (Hand.MyInstance.MyMoveable != null && Hand.MyInstance.MyMoveable is IUseable)
            {

                SetUseable(Hand.MyInstance.MyMoveable as IUseable);
            }


            UIManager.MyInstance.HideTooltip();
        }


    }


    public void SetUseable(IUseable useable)
    {

        if (useable is Item)
        {

            MyUseables = InventoryScript.MyInstance.GetUseables(useable);

            if (InventoryScript.MyInstance.MyFromSlot != null)
            {

                InventoryScript.MyInstance.MyFromSlot.MyCover.enabled = false;


                InventoryScript.MyInstance.MyFromSlot.MyIcon.enabled = true;


                InventoryScript.MyInstance.MyFromSlot = null;
            }
        }
        else
        {

            MyUseables.Clear();


            MyUseable = useable;
        }


        count = MyUseables.Count;


        UpdateVisual(useable as IMoveable);


        UIManager.MyInstance.RefreshTooltip(MyUseable as IDescribable);
    }


    public void UpdateVisual(IMoveable moveable)
    {

        if (Hand.MyInstance.MyMoveable != null)
        {

            Hand.MyInstance.Drop();
        }


        MyIcon.sprite = moveable.MyIcon;


        MyIcon.enabled = true;


        if (count > 1)
        {

            UIManager.MyInstance.UpdateStackSize(this);
        }

        else if (MyUseable is Spell)
        {

            UIManager.MyInstance.ClearStackCount(this);
        }
    }


    public void UpdateItemCount(Item item)
    {

        if (item is IUseable && MyUseables.Count > 0)
        {

            if (MyUseables.Peek().GetType() == item.GetType())
            {

                MyUseables = InventoryScript.MyInstance.GetUseables(item as IUseable);


                count = MyUseables.Count;


                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescribable describable = null;


        if (MyUseable != null && MyUseable is IDescribable)
        {

            describable = MyUseable as IDescribable;
        }

        else if (MyUseables.Count > 0)
        {

            describable = MyUseables.Peek() as IDescribable;
        }


        if (describable != null)
        {

            UIManager.MyInstance.ShowTooltip(new Vector2(1, 0), transform.position, describable);
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        // Masque le tooltip
        UIManager.MyInstance.HideTooltip();
    }
}*/