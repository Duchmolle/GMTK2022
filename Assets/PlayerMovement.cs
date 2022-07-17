using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public override void ComputeSequence()
    {
        movingSequence.Clear();
        nextCellPos = endCellPos;
        for (int i = 0; i < numberOfStep; i++)
        {
            for (int j = 0; j < GameManager.Instance.slotsValuesList[i]; j++)
            {
                movingSequence.Add(nextCellPos);

                directionSequence[i] = GameManager.Instance.playerDirectionsSequence[i];
                direction = directionSequence[i];

                if ((CheckNextRightTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.DROITE) ||
                    (CheckNextLeftTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.GAUCHE) ||
                    (IsGrounded() && directionSequence[i] == Direction.BAS))
                {
                    continue;
                }

                nextCellPos += GetDirection(movementValue);
            }

        }

        base.ComputeSequence();
    }

}
