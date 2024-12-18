using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hsTxt;
    public TextMeshProUGUI bestTimeTxt;

    void Start()
    {
        if (PlayerPrefs.GetInt("Highscore1") == 0)
        {
            hsTxt.text = string.Empty;
        }
        else
        {
            hsTxt.text = PlayerPrefs.GetInt("Highscore1").ToString();
        }

        if (PlayerPrefs.GetFloat("HighScore") == 0)
        {
            bestTimeTxt.text = string.Empty;
        }
        else
        {
            bestTimeTxt.text = FormatScore(PlayerPrefs.GetFloat("HighScore"));
        }
    }
    public void PlayGame()
    {

        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlaySpeedrun()
    {
        GameManager.gameMode = 0;
        SceneManager.LoadScene("Game");
    }

    public void PlaySurvival()
    {
        GameManager.gameMode = 1;
        SceneManager.LoadScene("Game");
    }

    public void DeleteHS()
    {
        PlayerPrefs.DeleteAll();
    }

    public string FormatScore(float score)
    {
        int minutes = Mathf.FloorToInt(score / 60);
        int seconds = Mathf.FloorToInt(score % 60);
        int milliseconds = Mathf.FloorToInt(((score * 100) % 100));
        string scoreTxt = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        return scoreTxt;
    }
}
