using UnityEngine;



public class NPC : MonoBehaviour, IInteractable
{
    
    [SerializeField]
    private Window window = default;

    
    public bool IsInteracting { get; set; }


   
    public virtual void Interact()
    {
        
        if (!IsInteracting)
        {
            
            
            IsInteracting = true;

            
            window.Open(this);
        }
    }

    
    public virtual void StopInteract()
    {
        
        if (IsInteracting)
        {
            
            IsInteracting = false;

            
            window.Close();
        }
    }
}