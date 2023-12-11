using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class QuestGiver : NPC
{
    [SerializeField]
    public TypingEffect typingEffect = default;
    public GameObject talkpanel;

    [SerializeField]
    private int questGiverId = default;
   
   
    public int MyQuestGiverId { get => questGiverId; }

    
    [SerializeField]
    private Quest[] quests = default;

    [SerializeField] 
    private QuestionObjectData QuestionObjectData = default;

    [SerializeField]
    private TypingEffect TypingEffect = default;

    public Quest[] MyQuests { get => quests; }

    [Header("Questing Status")]

    
    [SerializeField]
    private SpriteRenderer statusRenderer = default;

    [SerializeField]
    
    private Sprite question = default;

    
    [SerializeField]
    private Sprite questionSilver = default;

    [SerializeField]
    private Sprite exclamation = default;

    [SerializeField] KeyCode InteractionKeyCode = KeyCode.E;

  


    private bool isInRange;
    private bool isopened;
    private bool isstory=false;
    private bool isselected = false;
    private bool ended = false;

    
    [SerializeField] bool showAndHideMouse = true;

    private int currentIndex = 1;



    private List<string> completedQuests = new List<string>();

    public GameObject choiceObject;
    public GameObject[] choicePanel;
    public GameObject[] choiceCursor;

    
    public int result; // 선택한 선택창.
    
    

    public List<string> MyCompletedQuests
    {
        get => completedQuests;
        set
        {
            completedQuests = value;

            foreach (string title in completedQuests)
            {
                for (int i = 0; i < quests.Length; i++)
                {
                    
                    if (quests[i] != null && quests[i].MyTitle.ToLower() == title.ToLower())
                    {
                        quests[i] = null;
                    }
                }
            }
        }
    }



    

    private void Start()
    {
        resetbool();
        foreach (Quest quest in quests)
        {
            
            if (quest != null)
            {
                quest.MyQuestGiver = this;
            }
        }
    }

    private void resetbool()
    {
        
        isstory = false;
        isselected = false;
        ended = false;
    }

   private void Update()
    {
        
        if (isInRange && Input.GetKeyDown(InteractionKeyCode) && isopened!=true && !isstory)
        {
            isopened = true;
            isstory = true;
            talkpanel.SetActive(true);
            TypingEffect.EffectStart("");
            if (currentIndex == 1)
            {
                TypingEffect.EffectStart(QuestionObjectData.previoustalk[0]);
            }
            
            isselected = true;
            



        }
        /*else if (isInRange && Input.GetKeyDown(InteractionKeyCode) && isopened != true && !isstory)
        {
            isopened = true;
           

            talkpanel.SetActive(false);
            Interact();
            ShowMouseCursor();



        }*/
        else if (Input.GetKeyDown(InteractionKeyCode) && isopened && !isstory)
        {
           
            StopInteract();
            HideMouseCursor();
            isopened = false;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && isopened && isstory && isselected)
        {
            if (currentIndex < 20)
            {
                
                TypingEffect.EffectStart(QuestionObjectData.previoustalk[currentIndex]);
                currentIndex++;
                if (currentIndex > QuestionObjectData.previoustalk.Length-1)
                {
                    currentIndex = 20;
                }
            }
            else if (currentIndex == 20)
            {
                TypingEffect.EffectStart(QuestionObjectData.questtalk[0]);
                choicePanel[0].SetActive(true);
                choicePanel[1].SetActive(true);
                TypingEffect.EffectStartLeft(QuestionObjectData.accept[0]);
               
                TypingEffect.EffectStartRight(QuestionObjectData.reject[0]);
                isselected=false;
            }
            else if (currentIndex > 20 && currentIndex <= 40 && !ended)
            {
                

                
                TypingEffect.EffectStart(QuestionObjectData.acceptaftertalk[currentIndex - 21]);
                if (currentIndex == 21)
                {

                    choicePanel[0].SetActive(false);
                    choicePanel[1].SetActive(false);
                }
                
                currentIndex++;
                if (currentIndex-21 > QuestionObjectData.acceptaftertalk.Length - 1)
                {
                    currentIndex = 20 + QuestionObjectData.acceptaftertalk.Length - 1;
                    ended = true;
                }

            }
            else if ( currentIndex == 20+ QuestionObjectData.acceptaftertalk.Length - 1 && ended)
            {
                
                isstory = false;

                talkpanel.SetActive(false);
                resetbool();
                Interact();
                ShowMouseCursor();
            }
            else if (currentIndex > 40 && currentIndex <= 60 && !ended)
            {
                Debug.Log(currentIndex);
                if (currentIndex == 41)
                {
                    choicePanel[0].SetActive(false);
                    choicePanel[1].SetActive(false);
                }
            
                TypingEffect.EffectStart(QuestionObjectData.rejectaftertalk[currentIndex-41]);
                currentIndex++;
                if (currentIndex-41 > QuestionObjectData.rejectaftertalk.Length - 1)
                {
                    currentIndex = 40 + QuestionObjectData.rejectaftertalk.Length - 1;
                    ended = true;
                }
            }
            else if(currentIndex == 40+ QuestionObjectData.rejectaftertalk.Length - 1 && ended)
            {
                currentIndex = 61;
                talkpanel.SetActive(false);
                resetbool();
                isstory = false;

            }
            else if (currentIndex > 60) {
                TypingEffect.EffectStart(QuestionObjectData.returntalk[currentIndex-61]);
                currentIndex++;
                if (currentIndex-61 > QuestionObjectData.returntalk.Length-1)
                {
                    
                    currentIndex = 20;
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) && currentIndex == 20)
        {
            if (result > 0)
                result--;
            else
                result = 1;
            Selection();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) && currentIndex == 20)
        {
            if (result < 1)
                result++;
            else
                result = 0;
            Selection();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 20) // 엔터를 눌러서 질문이 끝날 때
        {
            Debug.Log("HERE");
            if(result == 0)
            {
                currentIndex = 21;
                isselected = true;
            }
            else
            {
                currentIndex = 41;
                isselected = true;
            }
            
        }
    }


    

    public void UpdateQuestStatus()
    {
        
        int count = 0;

        
        foreach (Quest quest in quests)
        {
           
            if (quest != null)
            {
                
                if (QuestWindow.MyInstance.HasQuest(quest) && quest.IsComplete)
                {
                    
                    statusRenderer.sprite = question;

                    
                    break;
                }
               
                else if (!QuestWindow.MyInstance.HasQuest(quest))
                {
                    
                    statusRenderer.sprite = exclamation;

                   
                    break;
                }
                
                else if (QuestWindow.MyInstance.HasQuest(quest) && !quest.IsComplete)
                {
                    
                    statusRenderer.sprite = questionSilver;

                   
                }

                
            }
            else
            {
                
                count++;

               
                if (count == quests.Length)
                {
                    statusRenderer.enabled = false;

                    
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            
        }
    }

    public void ShowMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HideMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Selection() // 현재 선택되어있는 커서 빼고 다 지우는 함수
    {
        for (int i = 0; i < choicePanel.Length; i++)
        {
            choiceCursor[i].SetActive(false);
        }
        choiceCursor[result].SetActive(true);
    }

    public void effectpr()
    {

    }


    


}