using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] spaceships;
    public float radius = 5f;
    public float moveSpeed = 2f;
    public Vector2 centerPosition;
    public float rotationSpeed = 20f;
    private Vector2[] targetPositions;

    private bool isRotating = false;
    private bool moveToGrid = false;
    private float rotationTime = 0f;
    private Vector2[] gridPositions;
    private bool inGridFormation = false;
    private Vector2 gridMoveDirection = Vector2.right;
    public float moveRange = 3f;
    private bool movingRight = true;

    void Start()
    {
        targetPositions = new Vector2[spaceships.Length];
        for (int i = 0; i < spaceships.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / spaceships.Length;
            targetPositions[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + centerPosition;
        }

        gridPositions = new Vector2[spaceships.Length];
        for (int i = 0; i < spaceships.Length; i++)
        {
            int row = i / 4;
            int col = i % 4;
            gridPositions[i] = new Vector2(centerPosition.x + col * 2f, centerPosition.y - row * 2f);
        }
    }

    void Update()
    {
        if (!isRotating)
        {
            MoveToTarget();
        }
        else if (!moveToGrid)
        {
            ShipsAround();
        }
        else if (!inGridFormation)
        {
            MoveToGrid();
        }
        else
        {
            MoveGridUpDown();
        }
    }

    void MoveToTarget()
    {
        bool allReachedTarget = true;
        for (int i = 0; i < spaceships.Length; i++)
        {
            spaceships[i].position = Vector3.MoveTowards(spaceships[i].position, targetPositions[i], moveSpeed * Time.deltaTime);
            if (Vector3.Distance(spaceships[i].position, targetPositions[i]) > 0.1f)
            {
                allReachedTarget = false;
            }
        }

        if (allReachedTarget)
        {
            isRotating = true;
        }
    }

    void ShipsAround()
    {
        rotationTime += Time.deltaTime;

        for (int i = 0; i < spaceships.Length; i++)
        {
            float angle = (rotationTime * rotationSpeed + i * (360f / spaceships.Length)) * Mathf.Deg2Rad;
            Vector2 newPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + centerPosition;
            spaceships[i].position = newPosition;
        }

        if (rotationTime * rotationSpeed >= 360f)
        {
            moveToGrid = true;
        }
    }

    void MoveToGrid()
    {
        bool allInGrid = true;
        for (int i = 0; i < spaceships.Length; i++)
        {
            spaceships[i].position = Vector3.MoveTowards(spaceships[i].position, gridPositions[i], moveSpeed * Time.deltaTime);
            if (Vector3.Distance(spaceships[i].position, gridPositions[i]) > 0.1f)
            {
                allInGrid = false;
            }
        }

        if (allInGrid)
        {
            inGridFormation = true;
        }
    }

    void MoveGridUpDown()
    {
        if (movingRight)
        {
            gridMoveDirection = Vector2.right;
        }
        else
        {
            gridMoveDirection = Vector2.left;
        }

        for (int i = 0; i < spaceships.Length; i++)
        {
            gridPositions[i] += gridMoveDirection * moveSpeed * Time.deltaTime;
            spaceships[i].position = gridPositions[i];
        }

        if (gridPositions[0].x > centerPosition.x + moveRange)
        {
            movingRight = false;
        }
        else if (gridPositions[0].x < centerPosition.x - moveRange)
        {
            movingRight = true;
        }
    }
}
