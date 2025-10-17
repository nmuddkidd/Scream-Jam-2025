
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class title : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text scores;
    void Start()
    {
        String high = PlayerPrefs.GetInt("HighScore").ToString();
        String one = PlayerPrefs.GetInt("Score1").ToString();
        String two = PlayerPrefs.GetInt("Score2").ToString();
        String three = PlayerPrefs.GetInt("Score3").ToString();
        String four = PlayerPrefs.GetInt("Score4").ToString();

        scores.text = "--Scores--\n" + high + "\n" + one + "\n" + two + "\n" + three + "\n" + four + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
