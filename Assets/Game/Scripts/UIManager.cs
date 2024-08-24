using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    public Text txtScore;
    private int score;
    public GameObject panelGameOver;
    public GameObject winPanel;

    private void Awake()
    {
        instance = this;
    }
    public void updateScore(int value)
    {
        score += value;
        txtScore.text = "Score: " + score.ToString();
    }
    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }
    public void restart()
    {
        panelGameOver.SetActive(false);
        winPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void winGame()
    {
        winPanel.SetActive(true);
    }
}
