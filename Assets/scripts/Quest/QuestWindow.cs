using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class QuestWindow : Window
{
    
    private static QuestWindow instance;


    
    public static QuestWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("here is");
               
                instance = FindObjectOfType<QuestWindow>();
            }

            return instance;
        }
    }

    
    private Quest selected;

    

    [SerializeField]
    private GameObject questPrefab = default;

    
    [SerializeField]
    private Transform questArea = default;

    
    [SerializeField]
    private Text questDescription = default;

    
    private readonly List<QuestScript> questScripts = new List<QuestScript>();

    
    private List<Quest> quests = new List<Quest>();

    public List<Quest> MyQuests { get => quests; set => quests = value; }

    
    [SerializeField]
    private Text questCount = default;

    [Header("Public Variables")]
    public Inventory ItemContainer;
    public Character character;

    [SerializeField]
    private int maxCount = default;


    
    private void Update()
    {
       
        questCount.text = quests.Count + "/" + maxCount;
    }

    
    public void AcceptQuest(Quest quest)
    {
        
        if (quests.Count < maxCount)
        {
            
            foreach (CollectObjective collectObjective in quest.MyCollectObjectives)
            {

                ItemContainer.ItemCountChangedEvent += new ItemCountChanged(collectObjective.UpdateItemCount);

                
                collectObjective.UpdateItemCount();
            }

            foreach (KillObjective killObjective in quest.MyKillObjectives)
            {

                character.KillConfirmedEvent += new KillConfirmed(killObjective.UpdateKillCount);
            }

            
            GameObject go = Instantiate(questPrefab, questArea);

            

            go.GetComponent<Text>().text = string.Format("<size=40>[{0}] {1}<size=40>!!</size></size>", quest.MyLevel, quest.MyTitle);

           
            QuestScript questScript = go.GetComponent<QuestScript>();

            
            quest.MyQuestScript = questScript;

            
            questScript.MyQuest = quest;

            
            quests.Add(quest);
            questScripts.Add(questScript);

            CheckCompletion();
        }
    }

    
    public void ShowDescription(Quest quest)
    {
       
        if (quest != null)
        {
            
            if (selected != null && selected != quest)
            {
                
                selected.MyQuestScript.DeSelect();
            }

            
            selected = quest;

          
            string description = quest.GetDescription();

            
            if (quest.MyCollectObjectives.Length > 0 || quest.MyKillObjectives.Length > 0)
            {

                string objectivesText = "";


                foreach (Objective collectObjective in quest.MyCollectObjectives)
                {
                    objectivesText += string.Format("\n\n<color=#468FC1><size=20>목표 아이템</size></color>\n");
                    objectivesText += string.Format("<color=#FFFFF><size=30><i>{0} : {1}/{2}</i></size></color>\n", collectObjective.MyTitle, collectObjective.MyCurrentAmount, collectObjective.MyAmount);
                }

                
                foreach (Objective killObjective in quest.MyKillObjectives)
                {
                    objectivesText += string.Format("\n\n<color=#468FC1><size=20>목표 대상</size></color>\n");
                    objectivesText += string.Format("<color=#FFFFF><size=30><i>{0} : {1}/{2}</i></size></color>\n", killObjective.MyTitle, killObjective.MyCurrentAmount, killObjective.MyAmount);
                }

                
                description += objectivesText;
            }

            
            questDescription.text = description;
        }
    }

   
    public void UpdateSelected()
    {
        
        ShowDescription(selected);
    }

   
    public void CheckCompletion()
    {
       
        foreach (QuestScript questScript in questScripts)
        {
            
            questScript.MyQuest.MyQuestGiver.UpdateQuestStatus();

            questScript.IsComplete();
        }
    }

    
    public bool HasQuest(Quest quest)
    {
        
      
        return quests.Exists(q => q.MyTitle == quest.MyTitle);
    }

   
    public void RemoveQuest(QuestScript qs)
    {
        
        questScripts.Remove(qs);

        
        Destroy(qs.gameObject);

        quests.Remove(qs.MyQuest);

       
        questDescription.text = string.Empty;

        
        selected = null;

        
        qs.MyQuest.MyQuestGiver.UpdateQuestStatus();

       
        qs = null;
    }


    
    public void AbandonQuest()
    {
       
        foreach (CollectObjective collectObjective in selected.MyCollectObjectives)
        {

            ItemContainer.ItemCountChangedEvent -= new ItemCountChanged(collectObjective.UpdateItemCount);

           
            collectObjective.Complete();
        }

        
        foreach (KillObjective killObjective in selected.MyKillObjectives)
        {

            character.KillConfirmedEvent -= new KillConfirmed(killObjective.UpdateKillCount);
        }

        
        RemoveQuest(selected.MyQuestScript);
    }

    
    public void TrackQuest()
    {

    }
}