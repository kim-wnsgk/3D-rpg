using UnityEngine;
using UnityEngine.UI;


public class MessageFeedManager : MonoBehaviour
{
    
    private static MessageFeedManager instance;

   
    public static MessageFeedManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                
                instance = FindObjectOfType<MessageFeedManager>();
            }

            return instance;
        }
    }

   
    [SerializeField]
    private GameObject messagePrefab = default;


    
    public void WriteMessage(string message)
    {
        
        if (message != string.Empty)
        {
            
            GameObject messageObject = Instantiate(messagePrefab, transform);

            
            messageObject.GetComponent<Text>().text = message;

            messageObject.transform.SetAsFirstSibling();

            
            Destroy(messageObject, 2);
        }
    }

   
    public void WriteMessage(string message, Color color)
    {
        
        if (message != string.Empty)
        {
           
            GameObject messageObject = Instantiate(messagePrefab, transform);

            
            Text textObject = messageObject.GetComponent<Text>();

            
            textObject.text = message;
            textObject.color = color;

            
            messageObject.transform.SetAsFirstSibling();

            
            Destroy(messageObject, 2);
        }
    }
}