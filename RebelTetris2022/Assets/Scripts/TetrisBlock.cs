using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Assertions;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousMoveTime = 0.1f;
    private float previousFallTime;
    public float fallTime = 0.8f;
    public static int height = 25;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    private static bool gameEnded = false;

    bool canMove = true;

    private int numberOfLinesThisTurn = 0;

    private AudioSource allAudios;
    [SerializeField]
    public AudioClip gameOverClip;

    [SerializeField]
    private string gameOverScene = "GOverScene";

    // Start is called before the first frame update
    void Start()
    {
        allAudios = GetComponent<AudioSource>();
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && canMove)
        {
            // Move to left
            canMove = false;
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);

            Invoke("ResetMoveTime", previousMoveTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && canMove)
        {
            // Move to right
            canMove = false;
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);

            Invoke("ResetMoveTime", previousMoveTime);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }

        // Accelerates the speed of the Tetromino
        if (Time.time - previousFallTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();

                CheckForLines();

                FindObjectOfType<SpawnTetromino>().UpdateScore(numberOfLinesThisTurn);
                this.enabled = false;

                if (!gameEnded)
                    FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            previousFallTime = Time.time;
        }
    }

    void ResetMoveTime()
    {
        canMove = true;
    }

    // Check if one row is full of Tetrominoes..
    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    // ..and if there is, add the value and return true to CheckForLines()..
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        numberOfLinesThisTurn++;
        return true;
    }

    // ..so that line/lines can be destroyed..
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    // ..and lines above will move down
    void RowDown(int i)
    {
        for (int y = i; y < height; y++) // goes through rows
        {
            for (int x = 0; x < width; x++) // goes through cells
            {
                if (grid[x, y] != null) // if cell is not empty..
                {
                    grid[x, y - 1] = grid[x, y]; // move cell up
                    grid[x, y] = null; // clear cell
                    grid[x, y - 1].transform.position -= new Vector3(0, 1, 0); // move game object up (the visible one)
                }
            }
        }
    }

    // GAME OVER if at row 19 or higher has a tetromino
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;

            if (roundedY >= 19)
            {
                // Play audio
                allAudios.PlayOneShot(gameOverClip);

                // Remove spawner
                Destroy(FindObjectOfType<SpawnTetromino>());
                gameEnded = true;

                // After three seconds goes to Game over scene
                Invoke(nameof(OpenGameOverScene), 3);
            }
        }
    }

    // Loads Game over -scene
    void OpenGameOverScene()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    // Checks if user commands are valid, Tetrominoes doesn't go on/over the grid
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
}
