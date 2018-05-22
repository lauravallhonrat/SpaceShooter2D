using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
     float _speed = 10.0f;

    
    void Start () {
		
	}

	void Update () {

        if (LevelController.instance.endOfGame)
            return;

        LaserMovement();
	}

	
	public void LaserMovement()
	{
        //sets the movement from the laser and when it goes off the map the game object is destroyed
		transform.Translate(Vector3.up * _speed * Time.deltaTime);

		if (transform.position.y >= 6.0f)
			Destroy(this.gameObject);
        //RECORDAR AÑADIR EL LASER PLAYER
       // PoolSystem.AddElementToPool(PoolTypes.laserEnemy, gameObject);
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(this.gameObject);
       // PoolSystem.AddElementToPool(PoolTypes.laserEnemy, gameObject);

        //accedemos al componente health, mediante collision, del objeto colisionado
        Health health = collision.gameObject.GetComponent<Health>();
        
        

        if (health!=null)//Control de seguridad health != null 
        {
            health.Damage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();



        if (health != null)//Control de seguridad health != null 
        {
            health.Damage(1);
        }
    }
}
