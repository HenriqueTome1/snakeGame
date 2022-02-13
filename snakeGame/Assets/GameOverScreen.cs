using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            RestartBtn();
        }
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("SnakeGame");
    }

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
