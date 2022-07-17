using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRandomizer : MonoBehaviour
{

    public NormalDieData dataDie;
    private int index;
    public int diceValue;
    public Sprite dieFace;

    private Image img;
    private Sprite randomImg;
    private int randomIndex;
    public bool isRolled;

    private Drag dragScript;

    //public bool once;
    private void Awake()
    {
        dragScript = GetComponent<Drag>();
    }
    private void Start()
    {
        img = GetComponent<Image>();
        isRolled = false;
        dragScript.enabled = false;
    }
    void Update()
    {

        if (isRolled)
        {
            dragScript.enabled = true;
        }

        //if (once)
        //{
        //    once = false;
        //    RollDice();
        //    img.sprite = dieFace;
        //}
    }

    public int GetDieNumber(NormalDieData dataDie)
    {

        index = Random.Range(0, dataDie.Numberfaces.Length);
        int diceValue = dataDie.Numberfaces[index];

        return diceValue;
    }
    public Sprite GetMatchingSprite(NormalDieData dataDie)
    {

        Sprite dieFace = dataDie.SpriteNumber[index];

        return dieFace;
    }

    public void RollDice(int id)
    { 
        if (!isRolled)
        {
            isRolled = true;
            diceValue = GetDieNumber(dataDie);
            dieFace = GetMatchingSprite(dataDie);
            StartCoroutine(AnimRollDice());
            
            Debug.Log(diceValue);
            Debug.Log(dieFace.name);
            //return diceValue;
        }
    }

    private IEnumerator AnimRollDice()
    {
        for (int i = 0; i < 20; i++)
        {
            randomIndex = Random.Range(0, dataDie.SpriteNumber.Length);
            randomImg = dataDie.SpriteNumber[randomIndex];
            img.sprite = randomImg;

            yield return new WaitForSeconds(0.05f);
        }
        img.sprite = dieFace;
    }
}
