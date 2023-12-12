using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageChanger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] StageList = default;

    public Image nextButton;
    public Image previousButton;
    private int currentStageIndex;

    public void Awake()
    {


        ChangeStage(0);

    }

    public void ChangeStage(int _index)
    {

        StageList[currentStageIndex].SetActive(false);
        currentStageIndex += _index;
        if (currentStageIndex >= StageList.Length)
        {
            currentStageIndex -= _index;
        }
        else if (currentStageIndex < 0)
        {
            currentStageIndex -= _index;
        }

        StageList[currentStageIndex].SetActive(true);

        Color currentColor = previousButton.color;

        if (currentStageIndex == 0)
        {
            ;
            currentColor.a = 0;
            previousButton.color = currentColor;
        }

        else if (currentStageIndex == StageList.Length - 1)
        {

            currentColor.a = 0;
            nextButton.color = currentColor;
        }

        else
        {
            currentColor.a = 255;
            nextButton.color = currentColor;
            previousButton.color = currentColor;
        }





    }
}
