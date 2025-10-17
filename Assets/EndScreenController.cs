using UnityEngine;
using TMPro;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    private int Score;
    public TMP_Text HighScore;
    public TMP_Text ScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score = PlayerPrefs.GetInt("NewScore"); // retrieves from the NewScore Key
        ScoreText.text = "Your Score: " + Score;
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            HighScoreShow(Score);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void HighScoreShow(int High)
        {
            PlayerPrefs.SetInt("HighScore", Score);
            HighScore.gameObject.SetActive(true);
        }
    public void RetryGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitToMain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
