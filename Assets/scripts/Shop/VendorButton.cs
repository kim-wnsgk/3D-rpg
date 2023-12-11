using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class VendorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private ItemTooltip itemtooltip;
    [SerializeField]
    public Inventory inventory;

    [SerializeField]
    private Image icon = default;

   
    public Image MyIcon { get => icon; }

    
    [SerializeField]
    private Text title = default;

   
    public Text MyTitle { get => title; }

    
    [SerializeField]
    private Text price = default;

    

    
    [SerializeField]
    private Text quantity = default;

    
    private VendorItem vendorItem;

    


    
    private void Awake()
    {
        // Référence sur le script VendorWindow
        //vendorWindow = GetComponentInParent<VendorWindow>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        itemtooltip.ShowTooltip(vendorItem.MyItem);
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {

        itemtooltip.HideTooltip();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        if ((character.gold >= vendorItem.MyPrice) && inventory.CanAddItem(vendorItem.MyItem))
        {
            
            SellItem();
        }
    }

    
    public void AddItem(VendorItem itemToAdd)
    {
        
        vendorItem = itemToAdd;

       
        if (itemToAdd.MyQuantity > 0 || (itemToAdd.MyQuantity == 0 && itemToAdd.MyUnlimited))
        {
            
            icon.sprite = itemToAdd.MyItem.Icon;

            
            title.text = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[itemToAdd.MyQuality], itemToAdd.MyItem.ItemName);
            Debug.Log("title"+title.text);
            
            price.text = (itemToAdd.MyPrice > 0) ? string.Format("Price : {0}", itemToAdd.MyPrice) : string.Empty;

            
            if (!itemToAdd.MyUnlimited)
            {
                
                quantity.text = itemToAdd.MyQuantity.ToString();
                Debug.Log("quantity"+quantity.text);
            }
            else
            {
                
                quantity.text = string.Empty;
            }

            
            Color buttonColor = Color.clear;

            
            ColorUtility.TryParseHtmlString(QualityColor.MyColors[itemToAdd.MyQuality], out buttonColor);

            
            GetComponent<Image>().color = buttonColor;

           
            gameObject.SetActive(true);
        }
    }

    
    private void SellItem()
    {
       
        character.gold -= vendorItem.MyPrice;

        inventory.AddItem(vendorItem.MyItem);
        if (!vendorItem.MyUnlimited)
        {
            
            vendorItem.MyQuantity--;

            
            quantity.text = vendorItem.MyQuantity.ToString();

            if (vendorItem.MyQuantity == 0)
            {
                
                gameObject.SetActive(false);

                
                itemtooltip.HideTooltip();
            }
        }
    }
}
