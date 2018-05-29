using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Enemy {

    [Header("Shoot System")]
    [SerializeField]
    float shootingRate = 3f;
    float nextFire = 0f;

    [SerializeField]
    Transform laserSpawn;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    PoolTypes weaponType = PoolTypes.laserEnemy;
  
    // Use this for initialization
    void Start () {
        nextFire = Time.time + shootingRate;
    }

    //FixedUpdate se utiliza para actualizar las fisicas, es igual que el update pero occurre en intervalos regulares de tiempo, no en cada frame
    //Late Update se ejecuta al final del frame (lo usamos porque enla clase base ya hay un update normal)
    void LateUpdate () {

        if (LevelController.instance.endOfGame)
            return;

        if (!health.isDieCalled)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            //Play FX!
            audioController.PlaySound(Sounds.laserSound);

            //se spawnea el objeto des del pool system
            PoolSystem.SpawnObject(laserPrefab, weaponType, laserSpawn.position, transform.rotation);
            //Instantiate(laserPrefab, laserSpawn.position,transform.rotation);
            nextFire = Time.time + shootingRate;
        }
    }
}
