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
                if(slots[i].childCount > 0)
                {
                    //if(slots[i].childCount > 1)
                    //{
                    //    Transform child = slots[i].GetChild(1).transform;
                    //    for(int j = 0; j < slots.Count; i++)
                    //    {
                    //        Drop slotScript = slots[j].GetComponent<Drop>();
                    //        if (!slotScript.isOccupied)
                    //        {
                    //            child.transform.parent = slots[j];
                    //        }
                    //    }
                    //}
                    DiceRandomizer dieScript = slots[i].GetChild(0).GetComponent<DiceRandomizer>();
                    int value = dieScript.diceValue;
                    slotsValuesList.Add(value);

                }
            }
            checkSlotList = false;
            
        }
        
    }
}
