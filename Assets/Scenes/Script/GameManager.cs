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
    public Transform dicesWindow;
    public List <Transform> slots;
    public List<int> slotsValuesList;

    public Movement.Direction[] playerDirectionsSequence = new Movement.Direction[4];

    public bool checkSlotList;
    private void Start()
    {
        checkSlotList = false;
        foreach(Transform child in sequence)
        {
            slots.Add(child);
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
                if(slots[i].childCount > 2)
                {

                    DiceRandomizer dieScript = slots[i].GetChild(2).GetComponent<DiceRandomizer>();
                    Drop slotDrop = slots[i].GetComponent<Drop>();
                    playerDirectionsSequence[i] = slotDrop.slotDirection;

                    int value = dieScript.diceValue;
                    slotsValuesList.Add(value);

                }
                if(slots[i].GetChild(2) != null)
                {
                    Transform dieTransform = slots[i].GetChild(2);
                    DiceRandomizer dieScript = dieTransform.GetComponent<DiceRandomizer>();
                    dieScript.isRolled = false;
                    dieTransform.parent = dicesWindow;

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

    public void LaunchSequence()
    {
        int diceCount = 0;
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].childCount > 2)
            {
                diceCount += 1;
            }
        }

        if (diceCount == slots.Count)
        {
            checkSlotList = true;
        }
        else
        {
            Debug.LogWarning("Dices must all be placed into the sequence windows !");
        }
    }

}
