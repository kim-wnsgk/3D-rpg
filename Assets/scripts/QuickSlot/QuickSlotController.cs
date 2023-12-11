using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private BaseItemSlot[] quickSlots;  // 퀵슬롯들 (4개)
    [SerializeField] private Transform tf_parent;  // 퀵슬롯들의 부모 오브젝트

    private int selectedSlot;  // 선택된 퀵슬롯의 인덱스 (0~3)
    [SerializeField] private GameObject go_SelectedImage;  // 선택된 퀵슬롯 이미지

    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<BaseItemSlot>();
        selectedSlot = 0;
    }

    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.W))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.E))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.R))
            ChangeSlot(3);
    }

    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
        Execute();
    }

    private void SelectedSlot(int _num)
    {
        // 선택된 슬롯
        selectedSlot = _num;

        // 선택된 슬롯으로 이미지 이동
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }

    private void Execute()
    {
    }

        /*
        if (quickSlots[selectedSlot].Items != null)
        {
            if (quickSlots[selectedSlot].Item. == Item.ItemType.Equipment)
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName));
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "맨손"));
            else
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "맨손"));
        }
        else
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("HAND", "맨손"));
        }
    */
}
