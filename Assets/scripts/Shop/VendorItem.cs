using System;
using UnityEngine;



[Serializable]
public class VendorItem
{
    
    [SerializeField]
    private Item item = default;

   
    public Item MyItem { get => item; }

   
    [SerializeField]
    private int quantity = default;

    
    public int MyQuantity { get => quantity; set => quantity = value; }

    [SerializeField]
    private int price = default;

    public int MyPrice { get => price; set => price = value; }

    
    [SerializeField]
    private bool unlimited = default;

    [SerializeField]
    private Quality quality;

    public Quality MyQuality { get => quality; set => quality = value; }

    public bool MyUnlimited { get => unlimited; }
}
