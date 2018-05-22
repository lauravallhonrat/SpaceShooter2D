using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {

    [System.NonSerialized]
    public UIController uiController;

    //[System.NonSerialized]
    public int currentHealth;

    [System.NonSerialized]
    public bool isDieCalled = false;

    public bool isShieldActive = false;

    public int maxHealth;

    //Sintaxis basica de un EVENTO
    public event Action OnDamage;

    public static event Action OnDie;


    AudioController audioController;


    void Awake () {
        currentHealth = maxHealth;
        audioController = LevelController.instance.audioController;

    }
	
	void Update () {
		
	}

    public void Damage(int damage)
    {
        if (!isShieldActive)
        {
            //Restamos la vida
            currentHealth -= damage;

            //Control de muerte
            if (currentHealth <= 0 && isDieCalled == false)
            {
                Die();
            }

            //SI alguien esta suscrito a este EVENT lo ejecutamos
            if (OnDamage != null)
                OnDamage();

        }
    }

    public void ExtraLife( )
    {
        if (currentHealth < maxHealth)
            currentHealth += 1;
    }

    //efecto muerte con delay para que se cumpla el bool ---> TODO: hacerlo con eventos
    public void Die()
    {
        if (OnDie != null)
            OnDie();

        isDieCalled = true;

        //Destruimos el objeto al cabo de 3 segundos (para que se vea bien la explosion)
        Destroy(gameObject, 3f);

        //Desactivamos el collider, para que las balas lo atraviesen desde el momento 0 de su muertwe
        GetComponent<Collider2D>().enabled = false;

        //accedemos al animator y activamos el trigger muerte
        GetComponent<Animator>().SetTrigger("Dead");
        audioController.PlaySound(Sounds.explosionSound);
        
       
    }
}
