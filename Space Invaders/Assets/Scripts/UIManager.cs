using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static int Score = 0;
    public static int highScore = 0;
    public Text points;
    public Text ScoreT;
    public Text HighScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        Score = 0;
        points.text = "Score: " + Score;
    }                              
                                   
    // Update is called once per frame
    void Update()                  
    {
        if (highScore < Score)
        {
            highScore = Score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        points.text = "Score: " + Score;
        ScoreT.text = "Score: " + Score;
        HighScore.text = "High Score: " + highScore;
    }

    public void PlayAgain()
    {        
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        highScore = 0;
        Score = 0;
        PlayerPrefs.SetInt("highScore", highScore);
    }
}
