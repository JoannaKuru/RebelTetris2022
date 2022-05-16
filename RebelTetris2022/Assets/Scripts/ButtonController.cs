using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private string gameScene = "GameScene";
    [SerializeField]
    private string menuScene = "MenuScene";
    [SerializeField]
    private string quideScene = "QuideScene";

    // A new game starts when the button is pressed
    public void BtnNewGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    // The game stops and Menu scene opens when the button is pressed
    public void BtnExitGame()
    {
        SceneManager.LoadScene(menuScene);
    }

    // A new game starts again when the button is pressed
    public void BtnRestartGame()
    {
        SceneManager.LoadScene(gameScene);
    }    
    
    // The Quide scene is opened when the button is pressed
    public void BtnOpenQuide()
    {
        SceneManager.LoadScene(quideScene);
    }
}
