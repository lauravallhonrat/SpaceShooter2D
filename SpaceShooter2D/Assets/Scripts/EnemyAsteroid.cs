using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroid : Enemy
{

    [SerializeField]
    float rotationSpeed = 30f;

    //FixedUpdate se utiliza para actualizar las fisicas, es igual que el update pero occurre en intervalos regulares de tiempo, no en cada frame
    void FixedUpdate()
    {
        if (LevelController.instance.endOfGame)
            return;

        if (!health.isDieCalled)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //Deteccion de colsion entre player y asteroid
        EnemyShip enemy = collision.gameObject.GetComponent<EnemyShip>();
        if (enemy != null)
        {
            //damos un punto de damage si el emeny colisiona con el asteroide
            health.Damage(1);
            enemy.health.Die();
        }

        //Detectamos si ha habido colision con el player
        PlayerCollision(collision);
    }
}
