using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUps
{
    speedUp,
    tripleShoot,
    shield,
    extraLife,
}
public class PowerUp : MonoBehaviour {


    [SerializeField]
    float _speed = 3.0f;

    [SerializeField]
    PowerUps powerUpKind; //this info is on unity on power up ID

    [SerializeField]
    float duration = 10f;

    [SerializeField]
    int points = 100;

    // Update is called once per frame
    void Update () {

        if (LevelController.instance.endOfGame)
            return;
        PowerUpMovement();
	}

    public void PowerUpMovement()
    {
        //sets the movement from the laser and when it goes off the map the game object is destroyed
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
            Destroy(this.gameObject);
    }
    //collision on 2D
    //REMEMBER!! one of the objects, in this case is the PLAYER must be set to rigid body to make a collision, this is set on the unity interface
    void OnTriggerEnter2D(Collider2D other)
    {
        //Comprobacion de si el objeto colisionado es el player por Componente. (La otra manera es por Tag)
        PlayerShip player = other.GetComponent<PlayerShip>();
        if (player != null)
        {
            //añadimos puntuación al cojer el powerUp
            LevelController.instance.AddScore(points);

            Destroy(this.gameObject);

            //POWER UP KIND EFFECT ACTIVATION
            if (powerUpKind == PowerUps.tripleShoot)
                player.TripleShootPowerUpOn(duration);
                    
            if (powerUpKind == PowerUps.speedUp)
                player.SpeedPowerUpOn(duration);

            if (powerUpKind == PowerUps.shield)
                player.ShieldPowerUpOn(duration);

            if (powerUpKind == PowerUps.extraLife)
                player.ExtraLifePowerUpOn();

        }    
       
    }
}
