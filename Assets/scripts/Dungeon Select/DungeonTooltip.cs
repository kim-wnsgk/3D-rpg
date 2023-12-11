
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
        LevelText.text = "�䱸 ����: " + newmap.LevelText.ToString();
        DunngeonExplainText.text = newmap.DungeonEx;

        float fillRatio = (float)newmap.difficulty / 20f; // 20���� ����� ������ 0 ~ 1 ���̷� ����
        Difficulty.fillAmount = fillRatio;
        Debug.Log(Difficulty.fillAmount);

        gameObject.SetActive(true);
    }

   
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }



}

