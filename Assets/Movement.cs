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
    private static float moveTime = 0.2f;

    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Vector3Int beginningPos;

    private Vector3Int nextCellPos;



    private void Start()
    {
        transform.position = mainTilemap.GetCellCenterWorld(beginningPos);

        nextCellPos = beginningPos;

        //StartCoroutine(Move(new Vector2(3, 0)));        
        StartCoroutine(DoSequence());
    }

    private void MoveToNextCell(int horizontalMovement, int verticalMovement)
    {
        Vector3Int currentCellPos = nextCellPos;

        nextCellPos = currentCellPos + new Vector3Int(horizontalMovement, verticalMovement, 0);

        transform.position = mainTilemap.GetCellCenterWorld(nextCellPos);
    }

    IEnumerator DoSequence()
    {
        for(int i = 0; i <= 4; i++)
        {
            MoveToNextCell(1, 0);
            yield return new WaitForSeconds(2);
        }
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
