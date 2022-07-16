using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private bool isMoving;
    private Vector2 startPos;
    private Vector2 endPos;

    private static int numberOfStep = 4;
    private static float moveTime = 0.4f;

    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Vector3Int beginningPos;
    [SerializeField] private int movementValue;
    [SerializeField] private LayerMask whatIsObstacle;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[1];

    private Vector3Int nextCellPos;

    public enum Direction
    {
        GAUCHE,
        DROITE,
        HAUT,
        BAS
    }

    public Direction direction;

    private void Start()
    {
        transform.position = mainTilemap.GetCellCenterWorld(beginningPos);

        nextCellPos = beginningPos;

        StartCoroutine(DoSequence());
    }

    protected IEnumerator MoveToNextCell(Vector3Int dir)
    {
        Vector3Int currentCellPos = nextCellPos;

        nextCellPos = currentCellPos + dir;

        float nextMove = 0f;
        while (nextMove < moveTime)
        {
            transform.position = Vector2.Lerp(mainTilemap.GetCellCenterWorld(currentCellPos), mainTilemap.GetCellCenterWorld(nextCellPos), nextMove / moveTime);
            nextMove += Time.deltaTime;
            yield return null;
        }

        transform.position = mainTilemap.GetCellCenterWorld(nextCellPos);
    }

    IEnumerator DoSequence()
    {
        for(int i = 0; i <= numberOfStep; i++)
        {
            if (CheckNextTile())
            {
                movementValue = -movementValue;
            }

            StartCoroutine(MoveToNextCell(GetDirection(movementValue)));

            yield return new WaitForSeconds(1);
        }
    }

    private Vector3Int GetDirection(int movementValue)
    {
        Vector3Int dir = new Vector3Int(0, 0, 0);

        switch(direction)
        {
            case Direction.GAUCHE:
                dir.x = -movementValue;
                break;

            case Direction.DROITE:
                dir.x = movementValue;
                break;

            case Direction.HAUT:
                dir.y = movementValue;
                break;

            case Direction.BAS:
                dir.y = -movementValue;
                break;
        }

        return dir;
    }

    private bool CheckNextTile()
    {
        int col = Physics2D.RaycastNonAlloc(transform.position, new Vector2(GetDirection(movementValue).x, GetDirection(movementValue).y), hitBuffer, 1, whatIsObstacle);
        return col > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, new Vector2(GetDirection(movementValue).x, GetDirection(movementValue).y) * 1);
    }


    //IEnumerator Move(Vector2 direction)
    //{
    //    isMoving = true;
    //    float nextMove = 0f;
    //    startPos = transform.position;
    //    endPos = startPos + direction;

    //    while(nextMove < moveTime)
    //    {
    //        transform.position = Vector2.Lerp(startPos, endPos, nextMove / moveTime);
    //        nextMove += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.position = endPos;
    //}
}
