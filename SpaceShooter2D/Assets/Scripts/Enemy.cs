using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]

public class Enemy : MonoBehaviour {

    [Header("Movement")]

    [SerializeField]
    [Range(0, 5)]
    float speedY;

    [SerializeField]
    [Range(0, 5)]
    float speedMoveTowards;

    [SerializeField]
    [Range(0, 50)]
    float speedRotateAround = 0f;

    [SerializeField]
    [Range(-4, 4)]
    float horizontalAmplitude = 1f;

    [SerializeField]
    bool lookingAtPlayer = false;

    [SerializeField]
    bool isBoss = false;

    GameObject player;

    [System.NonSerialized]
    public Health health;

    protected AudioController audioController;

    [SerializeField]
    int points = 50;

    GameObject scoreTextPrefab;

    // Use this for initialization
    void Awake () {
        player = LevelController.instance.player.gameObject;
        audioController = LevelController.instance.audioController;
        health = GetComponent<Health>();
        //nos suscribimos al EVENTO onDamage del health y ejecutaremos el DAmageOnShìp.
        //estamos añadiendo una funcion delegada DamageOnShip() (DELEGATE) al event action onDamage
        health.OnDamage += DamageOnShip;

        //Carga del asset directamente desde el project
        scoreTextPrefab = Resources.Load("Score Animated Canvas") as GameObject;
    }
    void Update()
    {

        if (LevelController.instance.endOfGame)
            return;

        if (!health.isDieCalled)
        {
            Movement();
        }

    }

    //Funcion suscrita al EVENT onDAmage
    void DamageOnShip()
    {
        //añadimos puntuación por destrución por laser
        if (health.currentHealth <= 0)
        {
            InstantiateScoreText(points);
            LevelController.instance.AddScore(points);
        }
        
        GetComponent<Animator>().SetTrigger("Hit");
        //HIT FX
        LevelController.instance.audioController.PlaySound(Sounds.hitSound);

    }

    protected void Movement()
    {
        if (lookingAtPlayer == true)
        {
            //LookAt2D direccion a la que quiere mirar
            Vector2 direction = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, transform.TransformDirection(Vector3.forward));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        //Traslacion en Y
        transform.position += Vector3.down * speedY * Time.deltaTime;

        //Movimiento sinusoidal en eje X
        transform.position += Vector3.right * Mathf.Sin(Time.time) * Time.deltaTime * horizontalAmplitude;

        //Rotacion alrededor del player
        transform.RotateAround(player.transform.position, player.transform.forward, speedRotateAround * Time.deltaTime);

        //movimiento hacia el player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedMoveTowards * Time.deltaTime);

        //destruir enemy al salirse del mapa
        if (transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detectamos si ha habido colision con el player
        PlayerCollision(collision);
    }

    protected void PlayerCollision(Collision2D collision)
    {
        //Averiguamos si hemos colisionado con el player. mediante Tag. (La otra manera es por Componente)
        if (collision.gameObject.tag == "Player" && !isBoss)
        {
            //destruimos la nave enemiga
            health.Die();

            //añadimos puntuación x2 por destrución con impacto
            LevelController.instance.AddScore(points * 2);
            InstantiateScoreText(points * 2);

            //Obtenemos el componente Health del Player
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            if (playerHealth != null)//Control de seguridad health != null 
            {
                playerHealth.Damage(1);
            }
        }

    }

    void InstantiateScoreText(int points)
    {
        if (scoreTextPrefab != null)
        {
            GameObject scoreObject = Instantiate(scoreTextPrefab, transform.position, Quaternion.identity) as GameObject;
            scoreObject.GetComponentInChildren<Text>().text = "+" + points.ToString();
            Destroy(scoreObject, 2);
        }
    }
}
