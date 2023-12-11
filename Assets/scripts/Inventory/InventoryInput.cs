using UnityEngine;

public class InventoryInput : MonoBehaviour
{
	[SerializeField] GameObject characterPanelGameObject;
	[SerializeField] GameObject equipmentPanelGameObject;
    [SerializeField] GameObject skillPanelGameObject;
    [SerializeField] GameObject makingPanelGameObject;
    [SerializeField] GameObject DungeonPanelGameObject;
    //[SerializeField] GameObject QuestPanelGameObject;

    [SerializeField] KeyCode[] toggleCharacterPanelKeys;
	[SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] KeyCode[] toggleSkillKeys;
    [SerializeField] KeyCode[] toggleMakingKeys;
    [SerializeField] KeyCode[] toggleDungeonKeys;
    //[SerializeField] KeyCode[] toggleQuestKeys;
    [SerializeField] bool showAndHideMouse = true;

	void Update()
	{
		ToggleCharacterPanel();
		ToggleInventory();
        ToggleSkill();
        ToggleMaking();
        ToggleDungeon();
        //ToggleQuest();
    }

	private void ToggleCharacterPanel()
	{
		for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
		{
			if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
			{
				characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

				if (characterPanelGameObject.activeSelf)
				{
					equipmentPanelGameObject.SetActive(true);
					ShowMouseCursor();
				}
				else
				{
					HideMouseCursor();
				}

				break;
			}
		}
	}

    private void ToggleSkill()
    {
        for (int i = 0; i < toggleSkillKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleSkillKeys[i]))
            {
                skillPanelGameObject.SetActive(!skillPanelGameObject.activeSelf);

                if (skillPanelGameObject.activeSelf)
                {
                    
                    ShowMouseCursor();
                }
                else
                {
                    HideMouseCursor();
                }

                break;
            }
        }
    }

    private void ToggleMaking()
    {
        for (int i = 0; i < toggleMakingKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleMakingKeys[i]))
            {
                makingPanelGameObject.SetActive(!makingPanelGameObject.activeSelf);

                if (makingPanelGameObject.activeSelf)
                {

                    ShowMouseCursor();
                }
                else
                {
                    HideMouseCursor();
                }

                break;
            }
        }
    }

    private void ToggleDungeon()
    {
        for (int i = 0; i < toggleDungeonKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleDungeonKeys[i]))
            {
                DungeonPanelGameObject.SetActive(!DungeonPanelGameObject.activeSelf);

                if (DungeonPanelGameObject.activeSelf)
                {

                    ShowMouseCursor();
                }
                else
                {
                    HideMouseCursor();
                }

                break;
            }
        }
    }
    /*
    private void ToggleQuest()
    {
        for (int i = 0; i < toggleQuestKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleQuestKeys[i]))
            {
                QuestPanelGameObject.SetActive(!QuestPanelGameObject.activeSelf);

                if (QuestPanelGameObject.activeSelf)
                {

                    ShowMouseCursor();
                }
                else
                {
                    HideMouseCursor();
                }

                break;
            }
        }
    }*/

    private void ToggleInventory()
	{
		for (int i = 0; i < toggleInventoryKeys.Length; i++)
		{
			if (Input.GetKeyDown(toggleInventoryKeys[i]))
			{
				if (!characterPanelGameObject.activeSelf)
				{
					characterPanelGameObject.SetActive(true);
					equipmentPanelGameObject.SetActive(false);
					ShowMouseCursor();
				}
				else if (equipmentPanelGameObject.activeSelf)
				{
					equipmentPanelGameObject.SetActive(false);
				}
				else
				{
					characterPanelGameObject.SetActive(false);
					HideMouseCursor();
				}
				break;
			}
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

	public void ToggleEquipmentPanel()
	{
		equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
	}

    public void ToggleSkillPanel()
    {
       skillPanelGameObject.SetActive(!skillPanelGameObject.activeSelf);
    }

    /*
    public void ToggleQuestPanel()
    {
        QuestPanelGameObject.SetActive(!QuestPanelGameObject.activeSelf);
    }

    */
}
