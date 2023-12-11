using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class QuestGiverWindow : Window
{
    
    private static QuestGiverWindow instance;

    
    public static QuestGiverWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                
                instance = FindObjectOfType<QuestGiverWindow>();
            }

            return instance;
        }
    }

    
    private readonly List<GameObject> quests = new List<GameObject>();

    
    private QuestGiver questGiver;

 

    
    private Quest selected;

    
    [SerializeField]
    private GameObject questPrefab = default;

    
    [SerializeField]
    private Transform questArea = default;

    
    [SerializeField]
    private GameObject questDescription = default;

    [Header("Buttons")]
    
    [SerializeField]
    private GameObject acceptButton = default;

   
    [SerializeField]
    private GameObject backButton = default;

    
    [SerializeField]
    private GameObject completeButton = default;

    [Header("Public Variables")]
    public Inventory ItemContainer;
    public Character character;



    public void ShowQuests(QuestGiver questGiverRef)
    {
        
        questGiver = questGiverRef;

        
        foreach (GameObject questObject in quests)
        {
            
            Destroy(questObject);
        }

        foreach (Quest quest in questGiver.MyQuests)
        {
            
            if (quest != null)
            {
                
                GameObject go = Instantiate(questPrefab, questArea);


                go.GetComponent<Text>().text = string.Format("<size=40>[{0}] {1} <color=#ffbb04><size=30>!</size></color></size>", quest.MyLevel, quest.MyTitle);



                go.GetComponent<QuestGiverScript>().MyQuest = quest;


                
                quests.Add(go);

                
                if (QuestWindow.MyInstance.HasQuest(quest) && quest.IsComplete)
                {
                    
                    go.GetComponent<Text>().text = string.Format("<size=40>[{0}] {1} <color=#ffbb04><size=30>?</size></color></size>", quest.MyLevel, quest.MyTitle);
                    
                   

                }
                
                else if (QuestWindow.MyInstance.HasQuest(quest))
                {
                    
                    Color color = go.GetComponent<Text>().color;

                    
                    color.a = 0.75f;

                    
                    go.GetComponent<Text>().color = color;

                    
                    go.GetComponent<Text>().text = string.Format("<size=40>[{0}] {1} <color=#c0c0c0ff><size=30>?</size></color></size>", quest.MyLevel, quest.MyTitle);
                    
                    
                }
            }
        }

        
        questDescription.SetActive(false);

        
        questArea.gameObject.SetActive(true);
    }

    
    public override void Open(NPC npcRef)
    {
        
        questDescription.SetActive(false);
        

        ShowQuests((npcRef as QuestGiver));
       

        base.Open(npcRef);
    }

    
    public override void Close()
    {
       
        completeButton.SetActive(false);

        
        base.Close();
    }

    
    public void ShowQuestInfo(Quest quest)
    {
        
        if (quest != null)
        {
            
            if (QuestWindow.MyInstance.HasQuest(quest) && quest.IsComplete)
            {
                
                acceptButton.SetActive(false);

                
                completeButton.SetActive(true);
            }
            
            else if (!QuestWindow.MyInstance.HasQuest(quest))
            {
                
                acceptButton.SetActive(true);
            }

           
            backButton.SetActive(true);

           
            questArea.gameObject.SetActive(false);

            
            questDescription.GetComponent<Text>().text = quest.GetDescription();

           
            questDescription.SetActive(true);

            
            
            
            selected = quest;

        }
    }

    
    public void AcceptQuest()
    {
        QuestWindow.MyInstance.AcceptQuest(selected);

        
        Back();
    }

    
    public void Back()
    {
        
        acceptButton.SetActive(false);
        backButton.SetActive(false);
        completeButton.SetActive(false);

        
        ShowQuests(questGiver);
    }

    
    public void CompleteQuest()
    {
        
        if (selected.IsComplete)
        {
            
            for (int i = 0; i < questGiver.MyQuests.Length; i++)
            {
               
                if (selected == questGiver.MyQuests[i])
                {
                    
                    questGiver.MyQuests[i] = null;

                    
                    questGiver.MyCompletedQuests.Add(selected.MyTitle);

                    
                    selected.MyQuestGiver.UpdateQuestStatus();
                }
            }

            
            foreach (CollectObjective collectObjective in selected.MyCollectObjectives)
            {
                
                ItemContainer.ItemCountChangedEvent -= new ItemCountChanged(collectObjective.UpdateItemCount);

                
                collectObjective.Complete();
            }

            
            foreach (KillObjective killObjective in selected.MyKillObjectives)
            {
                
                character.KillConfirmedEvent -= new KillConfirmed(killObjective.UpdateKillCount);
            }

            
            character.IncreaseExp(character.CalculateXP(selected));

            
            QuestWindow.MyInstance.RemoveQuest(selected.MyQuestScript);

            
            Back();
        }
    }

}
