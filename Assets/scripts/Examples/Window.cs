using UnityEngine;



public class Window : MonoBehaviour
{
    
    protected CanvasGroup canvasGroup;

   
    private NPC npc;


   
    private void Awake()
    {
        
        canvasGroup = GetComponent<CanvasGroup>();

       
        Close();
    }

    
    public virtual void Open(NPC npcRef)
    {
        
        npc = npcRef;

        
        canvasGroup.blocksRaycasts = true;

        
        canvasGroup.alpha = 1;
    }

    
    public virtual void Close()
    {
        
        if (npc)
        {
            
            npc.IsInteracting = false;

           
            npc = null;
        }

        
        canvasGroup.blocksRaycasts = false;

        
        canvasGroup.alpha = 0;
    }
}