using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {
    public Text pointsText;
    public Text highScore;
	public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = ScoreManager.instance.score + " POINTS";
        highScore.text = "Highscore" + ScoreManager.instance.highScore + "POINTS";

    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
