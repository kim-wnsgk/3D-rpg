using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public interface IDescribable
{
    // Propriété d'accès au texte de l'élément
    string GetDescription();
}


public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDescribable
{
    
    protected Image icon;

    
    [SerializeField]
    private Text countText = default;

    
    [SerializeField]
    private int maxCount = 5;

    
    private int currentCount;

    
    public int MyCurrentCount { get => currentCount; set => currentCount = value; }

    
    private bool unlocked;


    [SerializeField]
    private Skill childTalent = default;

    [SerializeField]
    private SkillSet skillset = default;


    [SerializeField]
    private SKillTooltip tooltip = default;





    private void Awake()
    {
        
        icon = GetComponent<Image>();

        
        countText.text = getCountText();

        
        if (unlocked)
        {
            Unlock();
        }
    }

    
    public string getCountText()
    {
        return MyCurrentCount + " / " + maxCount;
    }

    
    public void Lock()
    {
        
        icon.color = Color.gray;

        
        if (countText != null)
        {
            countText.color = Color.gray;
        }

        
       
    }

    
    public void Unlock()
    {
        
        icon.color = Color.white;

        
        if (countText != null)
        {
            MyCurrentCount++;

            countText.color = new Color32(245, 220, 0, 255);
            countText.text = "1 / 10";
        }

        tooltip.Reset(skillset);
       

        
        unlocked = true;
    }

    
    public bool Click()
    {
        tooltip.RefreshTooltip(skillset);
        if (MyCurrentCount < maxCount && unlocked)
        {
            
            MyCurrentCount++;

            
            countText.text = getCountText();

            
            if (MyCurrentCount == 3)
            {
                
                if (childTalent != null)
                {
                   
                    childTalent.Unlock();
                }
            }

            
            return true;
        }

        
        return false;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {

        tooltip.ShowTooltip(skillset);
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        
        tooltip.HideTooltip();
    }


   
    public virtual string GetDescription()
    {
        return string.Empty;
    }

   
}
