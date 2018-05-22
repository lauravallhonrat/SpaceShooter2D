using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    
    public Transform[] enemiesWaves;
    int numOfWave = 0;
   
    void Start () {

        //control de check point, si es true, activamos la ultima oleada de enemies sino activa la primera oleada por defecto
        if (LevelController.checkPoint)
            enemiesWaves[enemiesWaves.Length - 1].gameObject.SetActive(true);
        else 
            enemiesWaves[0].gameObject.SetActive(true);
    }

    void Update()
    {
        if (LevelController.instance.endOfGame)
            return;
        int numOfEnemies = enemiesWaves[numOfWave].childCount;

        if (numOfEnemies == 0)
        {
            DropEnemies();
            return;
        }

    }

    public void DropEnemies()
    {
        numOfWave++;
        //Si la cantidad de waves es mayor a la wave actual la activamos...
        if (enemiesWaves.Length > numOfWave )
        {
            enemiesWaves[numOfWave].gameObject.SetActive(true);
        }
        else//Si no es que no quedan waves y hemos acabado el nivel.
        {
            Debug.Log("End level");
            LevelController.instance.endOfGame = true;
        }
    }
}

