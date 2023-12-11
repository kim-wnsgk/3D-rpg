using UnityEngine;
using UnityEngine.EventSystems;




public class SpellButton : MonoBehaviour, IPointerClickHandler
{
    
    [SerializeField]
    private string spellName = default;

  
    public void OnPointerClick(PointerEventData eventData)
    {
        // Clic gauche
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Drag le sort
            //Hand.MyInstance.TakeMoveable(SpellBook.MyInstance.GetSpell(spellName));
        }
    }
}