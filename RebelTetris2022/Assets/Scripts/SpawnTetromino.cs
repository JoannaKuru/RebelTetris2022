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

    public float fallTime = 0.8f;

    //private AudioSource AS;

    public TextMeshProUGUI score;
    public TextMeshProUGUI lines;

    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
        //AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void NewTetromino()
    {
        GameObject Obj = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
        Obj.GetComponent<TetrisBlock>().fallTime = fallTime;
    }

    //public void NewPolice()
    //{
    //    Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    //}

    //public void UpdateUI()
    //{
    //    //score.text = currentScore.ToString();
    //}

    public void UpdateScore(int numberOfLinesThisTurn)
    {
        if (numberOfLinesThisTurn > 0)
        {
            if (numberOfLinesThisTurn == 1)
            {
                currentScore += score1Line;
                currentLineCount++;
                //ClearedOneLine();
            }
            else if (numberOfLinesThisTurn == 2)
            {
                currentScore += score2Lines;
                currentLineCount = currentLineCount + 2;
            }
            else if (numberOfLinesThisTurn == 3)
            {
                currentScore += score3Lines;
                currentLineCount = currentLineCount + 3;
            }
            else if (numberOfLinesThisTurn == 4)
            {
                currentScore += score4Lines;
                currentLineCount = currentLineCount + 4;
            }
            numberOfLinesThisTurn = 0;
        }
        score.text = currentScore.ToString();
        lines.text = currentLineCount.ToString();

        //if (currentScore >= 300)
        //{
        //    fallTime *= 0.3f;
        //    //AS.Play();
        //}
        //else if (currentScore >= 100)
        //{
        //    fallTime *= 0.3f;
        //}
        //else if (currentScore >= 300)
        //{
        //    fallTime *= 0.3f;
        //}
        //else if (currentScore >= 500)
        //{
        //    fallTime *= 0.3f;
        //}
        //else if (currentScore >= 700)
        //{
        //    fallTime *= 0.3f;
        //}
        //else if (currentScore >= 900)
        //{
        //    fallTime *= 0.3f;
        //}
        //else if (currentScore >= 1000)
        //{
        //    fallTime *= 0.3f;
        //}
        //else { }
    }
}