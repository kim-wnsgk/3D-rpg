using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QgQuestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Quest MyQuest {  get; set; }

   
    public void Select()
    {
        QuestGiverWindow.MyInstance.ShowQuestInfo(MyQuest);
    }
}
