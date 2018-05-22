using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    AudioController audioController;

    void Start () {
        audioController.PlaySound(Sounds.mainMenuSound);
    }
	
	void Update () {
		
	}


    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
