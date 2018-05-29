using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour {

    [SerializeField]
    GameObject phase1;

    [SerializeField]
    GameObject phase2;

    [SerializeField]
    GameObject phase3;

    [SerializeField]
    GameObject endingPhase;

    [SerializeField]
    GameObject laserBossPrefab;

    [SerializeField]
    Transform laserSpawnPhase2;

    [SerializeField]
    GameObject spawnMisile1;

    [SerializeField]
    GameObject spawnMisile2;

    Animator anim;

    [SerializeField]
    Animator[] superLasers;

    [SerializeField]
    float timeForSuperLaser = 4f;

    Health health;

    Health healthPhase2;

    Health healthPhase3;


    public int currentPhase = 1;

	void Start () {
        anim = GetComponent<Animator>();
        healthPhase2 = phase2.GetComponentInChildren<Health>();
        healthPhase3 = phase3.GetComponentInChildren<Health>();
        LevelController.checkPoint = true;

        //nos suscribimos al EVENTO onDamage del health y ejecutaremos el DamageOnShip().
        //estamos añadiendo una funcion delegada DamageOnShip() (DELEGATE) al event action onDamage
        healthPhase3.OnDamage += DamageOnShip;

        //Stop FX!
        LevelController.instance.audioController.StopSound(Sounds.environmentSound);
        //Play FX!
        LevelController.instance.audioController.PlaySound(Sounds.finalBossPhase1);
    }
	
	void Update () {

        //PHASE 1
        //accedemos a los hijos de la fase 1 no tienen el componente Enemy, estan destruidos
        //activamos el trigger creado en el esquema de animaciones
        //accedemos a la fase siguiente
        if (currentPhase == 1 && phase1.GetComponentInChildren<Enemy>() == null)
        {
            anim.SetTrigger("ExitPhase1");
            currentPhase = 2;
            //Stop FX!
            LevelController.instance.audioController.StopSound(Sounds.finalBossPhase1);
            //Play FX!
            LevelController.instance.audioController.PlaySound(Sounds.finalBossPhase2);
        }

        //cuando la vida del boss en fase 2 es menor de 2, activamos el escudo para que pueda terminar la animación y así pasar a la fase 3(Ya que las transiciones intro y exit necesitan que la nave este en el centro)
        if (currentPhase == 2 && healthPhase2.currentHealth < 2)
        {
            healthPhase2.isShieldActive = true;
            anim.SetTrigger("ExitPhase2");
            currentPhase = 3;
            StartCoroutine(RandomSuperLaser()); 
            //Stop FX!
            LevelController.instance.audioController.StopSound(Sounds.finalBossPhase2);
            //Play FX!
            LevelController.instance.audioController.PlaySound(Sounds.finalBossPhase3);
        }

        //entramos en fase Ending
        if (currentPhase == 3 && healthPhase3.currentHealth < 2)
        {
            healthPhase3.isShieldActive = true;
            Destroy(spawnMisile1);
            Destroy(spawnMisile2);
            anim.SetTrigger("ExitPhase3");
            currentPhase = 4;
            //FX!
            LevelController.instance.audioController.StopSound(Sounds.finalBossPhase3);
            LevelController.instance.audioController.PlaySound(Sounds.ending);

            //reiniciamos el juego
            StartCoroutine(LevelController.instance.AutoRestart());
        }

    }
    

    IEnumerator RandomSuperLaser()
    {
        while (!LevelController.instance.endOfGame && currentPhase == 3)
        {
            yield return new WaitForSeconds(timeForSuperLaser);
            int random = Random.Range(0, superLasers.Length);
            superLasers[random].SetTrigger("LaserShot");

        }
    }

    //se ejecuta desde la animación finalBoss phase 2
    public void LaserShot()
    {
        Instantiate(laserBossPrefab, laserSpawnPhase2.position,laserBossPrefab.transform.rotation);
    }

    void DamageOnShip()
    {
        
        //Relacion directa para que cuanta menos salud menor sea el intervalo entre disparos. 
        timeForSuperLaser -= timeForSuperLaser / healthPhase3.maxHealth;
        
    }

}
