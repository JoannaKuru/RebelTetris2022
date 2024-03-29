using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    public int score1Line = 30;
    public int score2Lines = 80;
    public int score3Lines = 200;
    public int score4Lines = 500;

    private int currentScore = 0;
    private int currentLineCount = 0;
    private int scoreMultiplyer = 1;
    private int levels = 0;

    public float fallTime = 0.8f;

    private AudioSource allAudios;
    [SerializeField]
    public AudioClip lineDestroyClip;
    [SerializeField]
    public AudioClip levelChangeClip;
    [SerializeField]
    public AudioClip policeClip;
    [SerializeField]
    public AudioClip rotateClip;

    public TextMeshProUGUI score;
    public TextMeshProUGUI lines;
    public TextMeshProUGUI level;

    private GameObject previewTetromino;
    private GameObject nextTetromino;

    private bool gameStarted = false;

    private Vector3 previewTetrominoPosition = new Vector3 (18, 15, 0);

    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
        allAudios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void NewTetromino()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            nextTetromino = (GameObject)Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);

            nextTetromino.GetComponent<TetrisBlock>().fallTime = fallTime;
            previewTetromino = (GameObject)Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<TetrisBlock>().enabled = false;
        } else
        {
            previewTetromino.transform.localPosition = transform.position;
            nextTetromino = previewTetromino;
            nextTetromino.GetComponent<TetrisBlock>().enabled = true;

            previewTetromino = (GameObject)Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<TetrisBlock>().enabled = false;
        }
    }

    // Score + defines audio clips
    public void UpdateScore(int numberOfLinesThisTurn)
    {
        if (numberOfLinesThisTurn > 0)
        {
            if (numberOfLinesThisTurn == 1)
            {
                ClearedOneLine();
                allAudios.PlayOneShot(lineDestroyClip);
            }
            else if (numberOfLinesThisTurn == 2)
            {
                ClearedTwoLines();
                allAudios.PlayOneShot(lineDestroyClip);
            }
            else if (numberOfLinesThisTurn == 3)
            {
                ClearedThreeLines();
                allAudios.PlayOneShot(lineDestroyClip);
            }
            else if (numberOfLinesThisTurn == 4)
            {
                ClearedFourLines();
                allAudios.PlayOneShot(lineDestroyClip);
            }

            if (currentScore >= 200 * scoreMultiplyer) // Levels change when score is 200 or more*multiplyer (in the beginning 1) -> falltime-(falltime*0.1f) 
            {                                          // Multiplyer grows ++ by every loop 
                fallTime -= fallTime * 0.1f;
                scoreMultiplyer++;
                //Debug.Log(fallTime);
                UpdateLevel();
            }
            numberOfLinesThisTurn = 0;
        }
        UpdateUI();
    }

    // Updates score to UI
    public void UpdateUI()
    {
        score.text = currentScore.ToString();
        lines.text = currentLineCount.ToString();
    }

    // Updates level to UI
    public void UpdateLevel()
    {
        levels++;
        level.text = levels.ToString();
        allAudios.PlayOneShot(levelChangeClip);
    }

    // Updates score + line count w/ 1 line
    void ClearedOneLine()
    {
        currentScore += score1Line;
        currentLineCount++;
    }

    // Updates score + line count w/ 2 lines
    void ClearedTwoLines()
    {
        currentScore += score2Lines;
        currentLineCount = currentLineCount + 2;
    }

    // Updates score + line count w/ 3 lines
    void ClearedThreeLines()
    {
        currentScore += score3Lines;
        currentLineCount = currentLineCount + 3;
    }

    // Updates score + line count w/ 4 lines
    void ClearedFourLines()
    {
        currentScore += score4Lines;
        currentLineCount = currentLineCount + 4;
    }

    // Plays rotate audio clip
    public void PlayRotateAudioClip()
    {
        allAudios.PlayOneShot(rotateClip);
    }
}