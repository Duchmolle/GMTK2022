using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] private List<Direction> enemyRoute;
    private int iterator;


    protected override void Sequence()
    {
        //base.Sequence();
    }

    protected override void ComputeSequence()
    {
        for (int i = 0; i <= numberOfStep; i++)
        {
            if (iterator >= enemyRoute.Count)
            {
                iterator = 0;
            }

            movingSequence[i] = nextCellPos;
            directionSequence[i] = enemyRoute[iterator];

            direction = enemyRoute[iterator];
            iterator++;

            if ((CheckNextRightTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.DROITE) || 
                (CheckNextLeftTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.GAUCHE))
            {
                continue;
            }
            nextCellPos += GetDirection(movementValue);
        }
    }



}
