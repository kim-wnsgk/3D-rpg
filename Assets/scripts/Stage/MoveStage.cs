using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveStage : MonoBehaviour
{
    public void school()
    {
        LoadingManager.LoadScene("school");
    }

    public void Move1()
    {
        LoadingManager.LoadScene("Stage1");
    }

    public void Move2()
    {
        LoadingManager.LoadScene("Stage2");
    }
}
