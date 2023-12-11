using UnityEngine;
using UnityEngine.UI;




public class QuestScript : MonoBehaviour
{
    
    public Quest MyQuest { get; set; }

    
    private bool markedComplete = false;


    
    public void Select()
    {
        
        GetComponent<Text>().color = Color.cyan;

       
        QuestWindow.MyInstance.ShowDescription(MyQuest);
    }

   
    public void DeSelect()
    {
        GetComponent<Text>().color = Color.white;
    }

    
    public void IsComplete()
    {
        
        if (MyQuest.IsComplete && !markedComplete)
        {
            
            GetComponent<Text>().text += " (완료)";

            
            MessageFeedManager.MyInstance.WriteMessage(string.Format("{0} (완료)", MyQuest.MyTitle));

            
            markedComplete = true;
        }
        
        else if (!MyQuest.IsComplete)
        {
            
            GetComponent<Text>().text = string.Format("[{0}] {1}", MyQuest.MyLevel, MyQuest.MyTitle);

            
            markedComplete = false;
        }
    }
}