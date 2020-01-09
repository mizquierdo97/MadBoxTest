using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    //LIVES
    public int maxLives = 3;
    int lives;

    //TIME
    float time = 0.0f;

    //UI STUFF
    public Text livesText;
    public Text timeText;
    public Text finalScoreText;
    public GameObject finalScore;

    public bool isGameActive = true;

    //EVENTS
    public UnityEvent OnResetLevel;

    //SINGLETON
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    private void Awake()
    {
        //Initializa Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive)
            time += Time.deltaTime;
        UpdateTime();
    }

    //Show/Hide Final Score Image
    public void ShowScore()
    {
        finalScore.SetActive(true);
        UpdateFinalScore();
        isGameActive = false;
    }
    public void HideScore()
    {
        finalScore.SetActive(false);
        ResetLevel();
    }
    //------

    public void ResetLevel()
    {
        isGameActive = true;
        lives = maxLives;
        UpdateLives();
        time = 0.0f;
        OnResetLevel.Invoke();
    }
    public void RemoveLive()
    {
        lives--;
        UpdateLives();
        if (lives <= 0)
            ResetLevel();
    }
    //Update the score Texts
    void UpdateLives()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    void UpdateTime()
    {
        timeText.text = CalculateTimeString();
    }
    void UpdateFinalScore()
    {
        finalScoreText.text = "Time: " + CalculateTimeString() + "    " + "Lives: " + lives.ToString();
    }
    //-----
    string CalculateTimeString()
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        
        return minutes + ":" + seconds;
    }
}
