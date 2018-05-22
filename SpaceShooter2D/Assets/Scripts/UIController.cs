using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [Header("UI")]
    public Text scoreText;

    [SerializeField]
    GameObject gameOverPanel;

    Animator anim;
   
    [Header("Lives")]
    [SerializeField]
    GameObject livesLeft3;

    [SerializeField]
    GameObject livesLeft2;

    [SerializeField]
    GameObject livesLeft1;

    [SerializeField]
    GameObject noLives;

    void Start () {
        gameOverPanel.SetActive(false);
        anim = GetComponent<Animator>();
        anim.SetBool("IsInPause", false);

    }

    void Update () {
		
	}

    public void RefreshLives(int lives)
    {
        livesLeft3.SetActive(false);
        livesLeft2.SetActive(false);
        livesLeft1.SetActive(false);
        noLives.SetActive(false);
        if (lives == 3)
            livesLeft3.SetActive(true);
        if (lives == 2)
            livesLeft2.SetActive(true);
        if (lives == 1)
            livesLeft1.SetActive(true);
        if (lives == 0)
            noLives.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main_menu");
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        anim.SetBool("IsInPause",true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        anim.SetBool("IsInPause", false);

    }
}
