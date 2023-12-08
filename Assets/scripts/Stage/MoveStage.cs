using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveStage : MonoBehaviour
{

    public void school()
    {
        SceneManager.LoadScene("school");
    }
    public void Move1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void Move2()
    {
        SceneManager.LoadScene("Stage2");
    }
}
