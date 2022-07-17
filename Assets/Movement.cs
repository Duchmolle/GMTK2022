using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private bool isMoving;
    private Vector2 startPos;
    private Vector2 endPos;

    protected static int numberOfStep = 4;
    private static float moveTime = 0.4f;

    [SerializeField] protected Tilemap mainTilemap;
    [SerializeField] private Vector3Int beginningPos;
    [SerializeField] protected int movementValue;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Color sequenceFeedbackColor;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[1];
    private RaycastHit2D[] groundHitBuffer = new RaycastHit2D[1];

    protected Vector3Int[] movingSequence = new Vector3Int[numberOfStep + 1];

    protected Direction[] directionSequence = new Direction[numberOfStep + 1];

    private Vector3Int endCellPos;
    private Vector3Int startCellPos;

    Animator animator;

    protected Vector3Int nextCellPos;

    public enum Direction
    {
        GAUCHE,
        DROITE,
        HAUT,
        BAS
    }

    protected Direction direction;

    private void Start()
    {
        transform.position = mainTilemap.GetCellCenterWorld(beginningPos);

        nextCellPos = beginningPos;

        ComputeSequence();
        DrawSequence();

        StartCoroutine(DoSequence());
    }

    private IEnumerator MoveToNextCell(Vector3Int currentCell, Vector3Int nextCell)
    {
        float nextMove = 0f;
        while (nextMove < moveTime)
        {
            transform.position = Vector2.Lerp(mainTilemap.GetCellCenterWorld(currentCell), mainTilemap.GetCellCenterWorld(nextCell), nextMove / moveTime);
            nextMove += Time.deltaTime;
            yield return null;
        }

        transform.position = mainTilemap.GetCellCenterWorld(nextCell);
    }

    private IEnumerator Fall() //Degueu
    {
        Vector2 currentPos = transform.position;

        while(!IsGrounded())
        {
            Vector2 nextPos = currentPos + Vector2.down;
            transform.position = Vector2.Lerp(currentPos, nextPos, 0.01f);
            currentPos = nextPos;
            yield return new WaitForSeconds(0.03f);
        }

        endCellPos = mainTilemap.WorldToCell(transform.position);
        transform.position = mainTilemap.GetCellCenterWorld(endCellPos);
    }

    private void DrawSequence()
    {
        for(int i = 0; i < movingSequence.Length; i++)
        {
            mainTilemap.SetTileFlags(movingSequence[i], TileFlags.None);
            mainTilemap.SetColor(movingSequence[i], sequenceFeedbackColor);
        }
    }

    protected virtual void ComputeSequence()
    {

    }

    protected virtual void Sequence()
    {

    }

    protected virtual IEnumerator DoSequence()
    {
        startCellPos = movingSequence[0];

        for(int i = 0; i < numberOfStep; i++)
        {

            StartCoroutine(MoveToNextCell(movingSequence[i], movingSequence[i+1]));

            yield return new WaitForSeconds(1);
        }

        endCellPos = movingSequence[numberOfStep];

        if(!IsGrounded())
        {
            StartCoroutine(Fall());
        }
    }

    protected Vector3Int GetDirection(int movementValue)
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

    protected bool CheckNextRightTile(Vector3 cellTransformPosition)
    {
        int col = Physics2D.RaycastNonAlloc(cellTransformPosition, Vector2.right, hitBuffer, 1, whatIsObstacle);
        return col > 0;
    }

    protected bool CheckNextLeftTile(Vector3 cellTransformPosition)
    {
        int col = Physics2D.RaycastNonAlloc(cellTransformPosition, Vector2.left, hitBuffer, 1, whatIsObstacle);
        return col > 0;
    }

    private bool IsGrounded()
    {
        int groundCol = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, groundHitBuffer, 1, whatIsGround);
        return groundCol > 0;
    }

    private void OnDrawGizmos()
    {
        if(IsGrounded())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawRay(transform.position, Vector2.down * 1);
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
