using UnityEngine;
using UnityEngine.UI;
using Kryz.CharacterStats;
using Kryz.CharacterStats.Examples;
using TMPro;
using UnityEngine.TextCore.Text;

public delegate void KillConfirmed(Character character);
public class Character : MonoBehaviour
{
    Player player;
    public int Health = 50;

    [Header("Level")]
    public int level;
    public int experience;
    public int requireExperience;
    public int SkillPoint;
    public int StatPoint;
    public int gold;

    public LevelConfig levelConfig;
    public SkillLevelUp skilllevelUp;

    public event KillConfirmed KillConfirmedEvent;


    [Header("Stats")]
    public CharacterStat Strength;
    public CharacterStat Dexterity;
    public CharacterStat HPandMP;
    public CharacterStat Damage;

    [Header("UI")]
    public Image experienceBar;
    public Image backXpBar;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI experienceText;

    [SerializeField]
    private string type = default;
    public string MyType { get => type; }


    [Header("Public")]
    public Inventory Inventory;
    public EquipmentPanel EquipmentPanel;

    [Header("Serialize Field")]
    [SerializeField] CraftingWindow craftingWindow;
    [SerializeField] StatPanel statPanel;
    [SerializeField] SkillLevelUp SkillPanel;
    [SerializeField] StatLevelUp statLvUp;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] Image draggableSkill;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog reallyDropItemDialog;
    [SerializeField] ItemSaveManager itemSaveManager;
    [SerializeField] StageChanger stageChanger;
    

    private BaseItemSlot dragItemSlot;

    [SerializeField] private BaseItemSlot[] quickSlots;
    [SerializeField] private GameObject go_SelectedImage;

    private float lerpTimer;
    private float delayTimer;

    private int selected;
    

    

    [SerializeField]
    private CanvasGroup[] menus = default;
    [SerializeField] bool showAndHideMouse = true;

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    private void Awake()
    {

        CalculateRequiredExp();
        
        statPanel.SetStats(Strength, Dexterity, HPandMP, Damage);
        statPanel.UpdateStatValues();
        SkillPanel.CalculatePoint(1);
        SkillPanel.CalculatePoint(2);
        statLvUp.CalculatePoint(1,this);
        statLvUp.CalculatePoint(2,this);

        // Setup Events:
        // Right Click
        Inventory.OnRightClickEvent += InventoryRightClick;
        Inventory.OnRightClickDropEvent += InventoryRightClickDrop;
        Inventory.OnLeftClickEvent += InventoryLeftClick;
        EquipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        // Pointer Enter
        Inventory.OnPointerEnterEvent += ShowTooltip;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltip;
        craftingWindow.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        Inventory.OnPointerExitEvent += HideTooltip;
        EquipmentPanel.OnPointerExitEvent += HideTooltip;
        craftingWindow.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;
        // Drop
        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;

        
    }

   

    void Update()
    {
        
        selected = ChangeSlot();
        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryRightClick(quickSlots[selected]);
            
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            // Ouverture/Fermeture du menu des sorts
            OpenClose(menus[0]);
        }

        // player.str = (int)Strength.BaseValue;
        // player.hp = (int)HPandMP.BaseValue;
        Debug.Log("여기" + Strength.Value);
    }

    private int ChangeSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            selected = 0;
            go_SelectedImage.transform.position = quickSlots[selected].transform.position;

            
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2");
            selected = 1;
            go_SelectedImage.transform.position = quickSlots[selected].transform.position;
           

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3");
            selected = 2;
            go_SelectedImage.transform.position = quickSlots[selected].transform.position;
           

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4");
            selected = 3;
            go_SelectedImage.transform.position = quickSlots[selected].transform.position;
            

        }

        return selected;
    }
    private void Start()
    {

        
        if (itemSaveManager != null)
        {
            itemSaveManager.LoadEquipment(this);
            itemSaveManager.LoadInventory(this);
        }

        
    }

    private void OnDestroy()
    {
        if (itemSaveManager != null)
        {
            itemSaveManager.SaveEquipment(this);
            itemSaveManager.SaveInventory(this);
        }
    }

    private void InventoryRightClick(BaseItemSlot itemSlot)
    {
        
        if (itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
        else if (itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);

            if (usableItem.IsConsumable)
            {
                itemSlot.Amount--;
                //Inventory.RemoveItem(usableItem);
                usableItem.Destroy();
            }
        }
    }

    private void InventoryLeftClick(BaseItemSlot itemSlot)
    {

        if (itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
        else if (itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);

            if (usableItem.IsConsumable)
            {
                itemSlot.Amount--;
                //Inventory.RemoveItem(usableItem);
                usableItem.Destroy();
            }
        }
    }



    public void IncreaseExp(int value)
    {
        experience += value;
        UpdateUI();

        if (experience >= requireExperience)
        {
            while (experience >= requireExperience)
            {
                experience -= requireExperience;
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        level++;
        LevelUpStats();
        CalculateRequiredExp();
        SkillPanel.CalculatePoint(level);
        statLvUp.CalculatePoint(level,this);
    }

    public void CalculateRequiredExp()
    {
        requireExperience = levelConfig.GetRequiredExp(level);
        UpdateUI();
    }

    private void UpdateUI()
    {

        experienceBar.fillAmount = ((float)experience / (float)requireExperience);
        LevelText.text = "Level: " + level;
        experienceText.text = experience + " / " + requireExperience + " Exp";
    }

   
    public void LevelUpStats()
    {
        
    }

        private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }

    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    private void HideTooltip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideTooltip();
        }
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.gameObject.SetActive(true);
        }
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        draggableItem.transform.position = Input.mousePosition;
    }

    private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.gameObject.SetActive(false);
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }

    private void AddStacks(BaseItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }

    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquippableItem dragEquipItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropEquipItem = dropItemSlot.Item as EquippableItem;

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.Equip(this);
            if (dropEquipItem != null) dropEquipItem.Unequip(this);
        }
        if (dragItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.Unequip(this);
            if (dropEquipItem != null) dropEquipItem.Equip(this);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void DropItemOutsideUI()
    {
        if (dragItemSlot == null) return;

        reallyDropItemDialog.Show();
        BaseItemSlot slot = dragItemSlot;
        reallyDropItemDialog.OnYesEvent += () => DestroyItemInSlot(slot);
    }

    private void DestroyItemInSlot(BaseItemSlot itemSlot)
    {
        // If the item is equiped, unequip first
        if (itemSlot is EquipmentSlot)
        {
            EquippableItem equippableItem = (EquippableItem)itemSlot.Item;
            equippableItem.Unequip(this);
        }

        itemSlot.Item.Destroy();
        itemSlot.Item = null;
    }

    public void Equip(EquippableItem item)
    {
        if (Inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                Inventory.AddItem(item);
            }
        }
    }

    public void Levelup()
    {

    }

    public void Unequip(EquippableItem item)
    {
        if (Inventory.CanAddItem(item) && EquipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }

    private ItemContainer openItemContainer;

    private void InventoryRightClickDrop(BaseItemSlot itemSlot)
    {
        Debug.Log("인식");
        Item item = itemSlot.Item;
        if (item != null && Inventory.CanAddItem(item))
        {
            Inventory.RemoveItem(item);
            Inventory.AddItem(item);
        }
    }

    private void TransferToItemContainer(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && openItemContainer.CanAddItem(item))
        {
            Inventory.RemoveItem(item);
            openItemContainer.AddItem(item);
        }
    }

    private void TransferToInventory(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && Inventory.CanAddItem(item))
        {
            openItemContainer.RemoveItem(item);
            Inventory.AddItem(item);
        }
    }

    public void OpenItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = itemContainer;

        Inventory.OnRightClickEvent -= InventoryRightClick;
        Inventory.OnRightClickEvent += TransferToItemContainer;

        itemContainer.OnRightClickEvent += TransferToInventory;

        itemContainer.OnPointerEnterEvent += ShowTooltip;
        itemContainer.OnPointerExitEvent += HideTooltip;
        itemContainer.OnBeginDragEvent += BeginDrag;
        itemContainer.OnEndDragEvent += EndDrag;
        itemContainer.OnDragEvent += Drag;
        itemContainer.OnDropEvent += Drop;
    }

    public void CloseItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = null;

        Inventory.OnRightClickEvent += InventoryRightClick;
        Inventory.OnRightClickEvent -= TransferToItemContainer;

        itemContainer.OnRightClickEvent -= TransferToInventory;

        itemContainer.OnPointerEnterEvent -= ShowTooltip;
        itemContainer.OnPointerExitEvent -= HideTooltip;
        itemContainer.OnBeginDragEvent -= BeginDrag;
        itemContainer.OnEndDragEvent -= EndDrag;
        itemContainer.OnDragEvent -= Drag;
        itemContainer.OnDropEvent -= Drop;
    }

    public void UpdateStatValues()
    {
        statPanel.UpdateStatValues();
    }

    public int CalculateXP(Quest quest)
    {

        if (level <= quest.MyLevel + 5)
        {
            // 100% 
            return quest.MyXp;
        }

        else if (level == quest.MyLevel + 6)
        {
            // 80% 
            return (int)((quest.MyXp * 0.8 / 5) * 5);
        }

        else if (level == quest.MyLevel + 7)
        {
            // 60% 
            return (int)((quest.MyXp * 0.6 / 5) * 5);
        }

        else if (level == quest.MyLevel + 8)
        {
            // 40% 
            return (int)((quest.MyXp * 0.4 / 5) * 5);
        }

        else if (level == quest.MyLevel + 9)
        {
            // 20% 
            return (int)((quest.MyXp * 0.2 / 5) * 5);
        }

        else if (level >= quest.MyLevel + 10)
        {
            // 10% 
            return (int)((quest.MyXp * 0.1 / 5) * 5);
        }

        return 0;
    }

    public void OnKillConfirmed()
    {
       
        if (KillConfirmedEvent != null)
        {
            
            KillConfirmedEvent.Invoke(this);
        }
    }
    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;

        
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        if (canvasGroup.blocksRaycasts)
        {
            ShowMouseCursor();
        }
        else
        {
            HideMouseCursor();
        }
        //Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    public void ShowMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HideMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    

}
