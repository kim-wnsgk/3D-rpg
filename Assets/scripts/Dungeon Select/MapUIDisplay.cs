using UnityEngine;
using UnityEngine.UI;using UnityEngine.SceneManagement;



public class MapUIDisplay : MonoBehaviour
{
    [Header("UI Elements")]
  
   
    [SerializeField] private Map[] maps = default;

    
    private int index;

    public void delAll()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].Clearstar = 0;
        }
    }
    public void Teleport(int _index)
    {
        index = _index;

        if (index > 0)
        {
            if (maps[_index - 1].Clearstar > 0)
            {
                string original_str = maps[_index].sceneToLoad.ToString();
                string resultStr1 = original_str.Replace(" (UnityEngine.SceneAsset)", "");


                SceneManager.LoadScene(resultStr1);
               
            }
        }
        else if (index == 0)
        {
            string original_str = maps[_index].sceneToLoad.ToString();
            string resultStr1 = original_str.Replace(" (UnityEngine.SceneAsset)", "");


            SceneManager.LoadScene(resultStr1);
        }
        
    }

    

    
}