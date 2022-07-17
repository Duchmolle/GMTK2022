using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Canvas canvas;
    public Transform sequence;
    public List <Transform> slots;
    public List<int> slotsValuesList;
    public List <Drop> freeSlots;

    public Movement.Direction[] playerDirectionsSequence = new Movement.Direction[4];

    public bool checkSlotList;
    //public bool freeSlotSpace;
    private void Start()
    {
        checkSlotList = false;
        foreach(Transform child in sequence)
        {
            slots.Add(child);
        }
        for (int i = 0; i < slots.Count; i++)
        {
            Drop slotScript = slots[i].GetComponent<Drop>();
            freeSlots.Add(slotScript);
        }
    }
    private void Update()
    {
        if (checkSlotList)
        {


            if(slotsValuesList.Count > 0)
            {
                slotsValuesList.Clear();
            }
            for(int i = 0; i < slots.Count; i++)
            {
                if(slots[i].childCount > 0)
                {
                    //if (slots[i].childCount > 1)
                    //{
                    //    Drop slotScript = slots[i].GetComponent<Drop>();
                    //    if (slotScript.isOccupied == false)
                    //    {
                    //        freeSlotPos = slots[i];
                    //    }
                    //}
                    DiceRandomizer dieScript = slots[i].GetChild(1).GetComponent<DiceRandomizer>();
                    Drop slotDrop = slots[i].GetComponent<Drop>();
                    playerDirectionsSequence[i] = slotDrop.slotDirection;

                    int value = dieScript.diceValue;
                    slotsValuesList.Add(value);

                }
            }
            checkSlotList = false;
            

        }
        
    }

    public Transform CheckSlotSpace()
    {
        Transform freeSlot = null;
        for (int i = 0; i < slots.Count; i++)
        {

            Drop slotScript = slots[i].GetComponent<Drop>();
            if (slotScript.isOccupied == false)
            {
                freeSlot = slots[i];

                break;
            }

        }
        return freeSlot;

    }

}
