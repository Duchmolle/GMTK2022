using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] private List<Direction> enemyRoute;
    private int iterator;

    private void Start()
    {
        //ComputeSequence();
    }

    protected override void Sequence()
    {
        //base.Sequence();
    }

    public override void ComputeSequence()
    {
        directionSequence = new Direction[GameManager.Instance.totalOfTicks];

        for (int i = 0; i < GameManager.Instance.totalOfTicks; i++)
        {
            if (iterator >= enemyRoute.Count)
            {
                iterator = 0;
            }

            movingSequence.Add(nextCellPos);
            directionSequence[i] = enemyRoute[iterator];

            direction = enemyRoute[iterator];
            iterator++;

            if ((CheckNextRightTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.DROITE) || 
                (CheckNextLeftTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.GAUCHE) || 
                (IsGrounded() && directionSequence[i] == Direction.BAS))
            {
                continue;
            }
            nextCellPos += GetDirection(movementValue);
        }
        
        base.ComputeSequence();
    }



}
