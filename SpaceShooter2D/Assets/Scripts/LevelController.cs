using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public AudioController audioController;

    public WaveController waveController;

    public PlayerShip player;

    public UIController uiController;

    public bool endOfGame;

    public static int totalScore;

    public static bool checkPoint = false;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        uiController.gameObject.SetActive(true);
        totalScore = 0;

    }

    void Start () {

    }

    void Update () {

        
	}

    //ADD SCORE
    public void AddScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        uiController.scoreText.text = string.Format("Score: {0}", totalScore);
    }
}
