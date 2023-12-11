using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



[Serializable]
public class Quest
{
    
    [SerializeField]
    private string title = default;

   
   
    public string MyTitle { get => title; set => title = value; }

   
    [SerializeField]
    private string key = default;

    [SerializeField] public Character character;
    
    public string MyKey { get => key; }

    
    [SerializeField]
    private string description = default;

    
    public string MyDescription { get => description; set => description = value; }

    
    public QuestScript MyQuestScript { get; set; }

   
    public QuestGiver MyQuestGiver { get; set; }

   
    [SerializeField]
    private int level = default;

    
    public int MyLevel { get => level; }

  
    [SerializeField]
    private int xp = default;

    
    public int MyXp { get => xp; }

    
    [SerializeField]
    private CollectObjective[] collectObjectives = default;

    
    public CollectObjective[] MyCollectObjectives { get => collectObjectives; }

    
    [SerializeField]
    private KillObjective[] killObjectives = default;

    


    public KillObjective[] MyKillObjectives { get => killObjectives; set => killObjectives = value; }

    
    public bool IsComplete
    {
        get
        {
            
            foreach (Objective collectObjective in collectObjectives)
            {
                if (!collectObjective.IsComplete)
                {
                    
                    return false;
                }
            }
            
            foreach (Objective killObjective in killObjectives)
            {
                if (!killObjective.IsComplete)
                {
                    // Retourne que c'est KO
                    return false;
                }
            }
           
            return true;
        }
    }

   
    public string GetDescription()
    {
        // Description de la quête
        string questDescription = string.Empty;
        questDescription += string.Format("<color=#992F2F><b><size=40>{0}</size></b></color>", MyTitle);
        questDescription += string.Format("\n\n<color=#FFFFFF><size=25>{0}</size></color>", MyDescription);
        questDescription += string.Format("\n\n<color=#95AD40><size=30>XP : {0}</size></color>", character.CalculateXP(this));


        return questDescription;
    }

    
}



[Serializable]
public abstract class Objective
{

    [Header("Public Variables")]
    public Inventory Inventory;

    [SerializeField]
    private string title = default;

   
    public string MyTitle { get => title; }

    
    [SerializeField]
    private string key = default;

    
    public string MyKey { get => key; }

    [SerializeField]
    public Item[] items = default;

   
    [SerializeField]
    private int amount = default;

   
    public int MyAmount { get => amount; }

    
    private int currentAmount;

   
    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }

    
    public bool IsComplete { get => currentAmount >= amount; }


    
    public string GetObjectiveMessage()
    {
        string message = string.Empty;

        
        if (MyCurrentAmount <= MyAmount)
        {
            
            message += string.Format("<size=40>{0} : {1}/{2}</size>", MyTitle, MyCurrentAmount, MyAmount);
        }

       
        return message;
    }

    
    public void RefreshObjectives(bool displayMessage = false)
    {
        
        if (displayMessage)
        {
            
            MessageFeedManager.MyInstance.WriteMessage(GetObjectiveMessage());
        }

        
        QuestWindow.MyInstance.CheckCompletion();

        
        QuestWindow.MyInstance.UpdateSelected();
    }
}


[Serializable]
public class CollectObjective : Objective
{
    
    public void UpdateItemCount(Item item)
    {
        
        if (MyKey.ToLower() == item.ID.ToLower())
        {

            MyCurrentAmount = Inventory.ItemCount(MyKey);

            
            RefreshObjectives(true);
        }
    }

    
    public void UpdateItemCount()
    {
        
        MyCurrentAmount = Inventory.ItemCount(MyKey);

        
        RefreshObjectives();
    }

    
    public void Complete()
    {
        
        bool completeMission = Inventory.RemoveItem2(MyKey, MyAmount);


        if (completeMission == true)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Inventory.AddItem(items[i]);
            }
            
        }
    }

    
}




[Serializable]
public class KillObjective : Objective
{
    
    public void UpdateKillCount(Character character)
    {
        
        if (MyKey.ToLower() == character.MyType.ToLower())
        {
            
            if (MyCurrentAmount < MyAmount)
            {
                
                MyCurrentAmount++;

                
                RefreshObjectives(true);
            }
        }
    }
}