using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Die", menuName = "Dices/Normal Die")]
public class NormalDieData : ScriptableObject
{
    public int[] Numberfaces;
    public Sprite[] SpriteNumber;
}
