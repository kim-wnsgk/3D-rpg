using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mapping : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private Map previousmap = default;

    [SerializeField]
    private Map map = default;

    [SerializeField]
    private Image img = default;

    [SerializeField]
    private DungeonTooltip tooptip = default;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (previousmap != null && previousmap.Clearstar >= 0)
        {
            Color currentColor = img.color;

            currentColor.a = 0;

            img.color = currentColor;
        }
        else if(previousmap == null)
        {
            Color currentColor = img.color;

            currentColor.a = 0;

            img.color = currentColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        tooptip.ShowTooltip(map);
    }


    public void OnPointerExit(PointerEventData eventData)
    {

        tooptip.HideTooltip();
    }
}
