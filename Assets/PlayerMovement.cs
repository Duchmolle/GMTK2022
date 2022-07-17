using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void ComputeSequence()
    {
        for (int i = 0; i < numberOfStep; i++)
        {
            movingSequence[i] = nextCellPos;
            directionSequence[i] = GameManager.Instance.playerDirectionsSequence[i];

            if ((CheckNextRightTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.DROITE) ||
                (CheckNextLeftTile(mainTilemap.GetCellCenterWorld(nextCellPos)) && directionSequence[i] == Direction.GAUCHE) ||
                (IsGrounded() && directionSequence[i] == Direction.BAS))
            {
                continue;
            }
            nextCellPos += GetDirection(movementValue);
        }
    }

}
