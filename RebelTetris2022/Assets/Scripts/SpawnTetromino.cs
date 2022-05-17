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

    public TextMeshProUGUI score;
    public TextMeshProUGUI lines;
    public TextMeshProUGUI level;

    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
        allAudios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void NewTetromino()
    {
        GameObject Obj = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
        Obj.GetComponent<TetrisBlock>().fallTime = fallTime;
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

            if (currentScore >= 200 * scoreMultiplyer)
            {
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
}