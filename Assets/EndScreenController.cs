using UnityEngine;
using TMPro;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    private int Score;
    private int[] HighScores = new int[6];
    public TMP_Text HighScore;
    public TMP_Text ScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score = PlayerPrefs.GetInt("NewScore"); // retrieves from the NewScore Key
        HighScores[0] = PlayerPrefs.GetInt("HighScore");
        for (int i = 1; i <= 4; i++)
        {
            HighScores[i] = PlayerPrefs.GetInt("Score" + i);
        }
        HighScores[5] = Score;
        ScoreText.text = "Your Score: " + Score;
        UpdateHighScore();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateHighScore()
    {
            System.Array.Sort(HighScores);
            PlayerPrefs.SetInt("HighScore", HighScores[5]);
            int j = 1;
            for (int i = 4; i > 0; i--)
            {
                PlayerPrefs.SetInt("Score"+j, HighScores[i]);
                j++;
            }
            if(Score == HighScores[5])
            {
                HighScore.gameObject.SetActive(true);
            }
            
    }
    public void RetryGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitToMain()
    {
        SceneManager.LoadScene("Title");
    }
}
