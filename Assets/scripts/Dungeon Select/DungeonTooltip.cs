
using UnityEngine;
using UnityEngine.UI;



public class DungeonTooltip : MonoBehaviour
{
    [SerializeField] Text DungeonNameText;
    [SerializeField] Text LevelText;
    [SerializeField] Image Difficulty;
    [SerializeField] Text DunngeonExplainText;
    


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowTooltip(Map newmap)
    {
        DungeonNameText.text = newmap.DungeonName;
        LevelText.text = "요구 레벨: " + newmap.LevelText.ToString();
        DunngeonExplainText.text = newmap.DungeonEx;

        float fillRatio = (float)newmap.difficulty / 20f; // 20으로 나누어서 비율을 0 ~ 1 사이로 만듦
        Difficulty.fillAmount = fillRatio;
        Debug.Log(Difficulty.fillAmount);

        gameObject.SetActive(true);
    }

   
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }



}

