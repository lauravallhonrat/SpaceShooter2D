using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public AudioController audioController;

    public WaveController waveController;

    public PlayerShip player;

    public UIController uiController;

    public Laser laser;

    public bool endOfGame;

    public static int totalScore;

    public static bool checkPoint = false;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        uiController.gameObject.SetActive(true);

        //actualizar la puntuación si estamos en una segunda vuelta del juego (para evitar que salga puntuación 0)
        AddScore(0);
    }

    void Start () {

    }

    void Update () {
        if(endOfGame)
           totalScore = 0;
        
	}

    //ADD SCORE
    public void AddScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        uiController.scoreText.text = string.Format("Score: {0}", totalScore);
    }

    public IEnumerator AutoRestart()
    {
        //incrementamos la velocidad del laser al finalizar el juego (incremento lineal del juego)
        laser._speed --; 
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene("Game");

    }
}
