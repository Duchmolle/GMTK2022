using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] private List<Direction> sequenceDirection;
    private int iterator;


    protected override void Sequence()
    {
        //base.Sequence();
    }

    protected override void ComputeSequence()
    {
        for (int i = 0; i <= numberOfStep; i++)
        {
            if (iterator >= sequenceDirection.Count)
            {
                iterator = 0;
            }

            movingSequence[i] = nextCellPos;

            direction = sequenceDirection[iterator];
            nextCellPos += GetDirection(movementValue);

            iterator++;
        }
    }



}
