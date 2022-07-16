using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] private List<Direction> sequenceDirection;
    private int iterator;

    protected override void Sequence()
    {
        if(iterator >= sequenceDirection.Count)
        {
            iterator = 0;
        }

        direction = sequenceDirection[iterator];
        iterator++;
        //base.Sequence();
    }
}
