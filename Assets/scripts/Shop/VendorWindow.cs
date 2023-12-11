using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class VendorWindow : Window
{
    
    [SerializeField]
    private VendorButton[] vendorButtons = default;

    private readonly List<List<VendorItem>> pages = new List<List<VendorItem>>();

    
    private int pageIndex = 0;

   
    [SerializeField]
    private Text pageNumber = default;

    
    [SerializeField]
    private GameObject previousButton = default, nextButton = default;


    
    public void CreatePages(VendorItem[] items)
    {
        
        pages.Clear();

        

        List<VendorItem> currentPage = new List<VendorItem>();

        
        for (int i = 0; i < items.Length; i++)
        {
            currentPage.Add(items[i]);

            
            if (currentPage.Count == vendorButtons.Length || i == items.Length - 1)
            {
                
                pages.Add(currentPage);

                
                currentPage = new List<VendorItem>();
            }
        }

        
        AddVendorItems();
    }

    
    public void PreviousPage()
    {
        
        if (pageIndex > 0)
        {
            
            pageIndex--;

            
            ClearButtons();

            
            AddVendorItems();
        }
    }

    
    public void NextPage()
    {
        
        if (pageIndex < pages.Count - 1)
        {
            
            pageIndex++;

            
            ClearButtons();

            
            AddVendorItems();
        }
    }

   
    private void AddVendorItems()
    {
        
        if (pages.Count > 0)
        {
            
            pageNumber.text = string.Format("{0}/{1}", pageIndex + 1, pages.Count);

            
            previousButton.SetActive(pageIndex > 0);

           
            nextButton.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                
                if (pages[pageIndex][i] != null)
                {
                    
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }

    
    public void ClearButtons()
    {
       
        foreach (VendorButton button in vendorButtons)
        {
            
            button.gameObject.SetActive(false);
        }
    }

   
    public override void Open(NPC npcRef)
    {
       
        CreatePages((npcRef as Vendor).MyItems);

        
        base.Open(npcRef);
    }
}