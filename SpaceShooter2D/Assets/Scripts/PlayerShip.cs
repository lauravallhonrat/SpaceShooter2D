//TODO: cambiar primera letra mayúscula de los métodos
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//name spaces libraries used

public class PlayerShip : MonoBehaviour {//monobehaviour permite el comportamiento como componente de nuestras clases, exponer propiedades en el inspector, etc

    [SerializeField]
    private GameObject _laserPrefab;//created new field on unity on player for dragging the laser prefab

    [SerializeField]
    private GameObject _tripleShotPrefab;
    //setting the variable to private but allows to edit on the inspector from unity
    [SerializeField]
    private float _speedPlayer = 10.0f;
    [SerializeField]
    private float _fireRate = 0.15f;

    private Transform _transform;

    private float _nextFire = 0.0f;

     bool canTripleShot = false;

    Animator anim;

    AudioController audioController;

    [SerializeField]
    GameObject shield;

    [SerializeField]
    GameObject thurster;

    [System.NonSerialized]
    public Health health;


    // Use this for initialization
    private void Start ()
	{

        Initialize();

    }

    // Update is called once per frame
    private void Update ()
	{
        if (LevelController.instance.endOfGame)
            return;

        if (health.isDieCalled)
            LevelController.instance.endOfGame = true;

        PlayerMovements();
		MapLimitsY();
        MapLimitsX();

        //detect that space key is pressed or touch happend
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))//detects space and left click mouse or screen touch
            Shoot();
    }

    void Initialize()
    {

        //Mejor rendimiento el uso de transform así, sobre todo cuando se usa dentro de Update
        _transform = GetComponent<Transform>();

        anim = GetComponent<Animator>();

        health = GetComponent<Health>();

        audioController = LevelController.instance.audioController;

        //Set the default position
        //  _transform.position = new Vector3(0, 0, 0);

        //nos suscribimos al EVENTO onDamage del health y ejecutaremos el DamageOnShip().
        //estamos añadiendo una funcion delegada DamageOnShip() (DELEGATE) al event action onDamage
        health.OnDamage += DamageOnShip;

        //UI vida actual al iniciar el juego
        LevelController.instance.uiController.RefreshLives(health.currentHealth);
    }

    void DamageOnShip()
    {
        //llamamos a la funcion RefreshLives del uiController y le pasamos como parametro la vida actual del player
        LevelController.instance.uiController.RefreshLives(health.currentHealth);
       // transform.DOShakePosition(1, new Vector3(0.1f, 0, 0), 40, 90, false);

        anim.SetTrigger("Hit");

        if (health.isDieCalled)
        {
            LevelController.instance.uiController.GameOver();
        }
        //HIT FX
        LevelController.instance.audioController.PlaySound(Sounds.hitSound);
        
    }

    private void PlayerMovements()
	{
		//moving the player
		float horizontalInput = Input.GetAxis("Horizontal") + Input.acceleration.x;
		float verticalInput = Input.GetAxis("Vertical") + Input.acceleration.y;

        //Animacion de giro de la nave
        anim.SetFloat("Inclination", horizontalInput);

        //new Vector3(5,0,0) * 5 * 1 -->moving to the right with the right inputs
        _transform.Translate(Vector3.right * _speedPlayer * horizontalInput * Time.deltaTime);
		_transform.Translate(Vector3.up * _speedPlayer * verticalInput * Time.deltaTime);
	}

    private void MapLimitsY()
	{
        //x = Mathf.Clamp(x, -10, 10);
		//set the map limits of the player
		if (_transform.position.y > 4.2f)
		{
			_transform.position = new Vector2(_transform.position.x, 4.2f);
            return;
		}
		if (_transform.position.y < -4.2f) // TODO: Sugiero que el límite esté en la parte superior de la pantalla. 
		{
			_transform.position = new Vector2(_transform.position.x, -4.2f);
            return;
        }
    }

    private void MapLimitsX()
    {
        if (_transform.position.x > 9.5f)
        {
            _transform.position = new Vector3(-9.5f, _transform.position.y, 0);
            return;
        }
        if (_transform.position.x < -9.5f)
        {
            _transform.position = new Vector3(9.5f, _transform.position.y, 0);
            return;
        }
    }

    private void Shoot()
    {
        //creates a spawn of the laser to the player position
        //Quaternion.identity-->default position
        //sets the laser to the player position above with no rotation
        //countdown on laser so player is not spaming
        if (Time.time > _nextFire) 
        {
            //setting triple shot
            if (canTripleShot == true)
            {
                //Play FX!
                audioController.PlaySound(Sounds.tripleShotLaserSound);
                Instantiate(_tripleShotPrefab, _transform.position, Quaternion.identity);
            }
            else
            {
                //Play FX!
                audioController.PlaySound(Sounds.laserSound);

                //instanciamos el laser del player mediante Poolsystem
                PoolSystem.SpawnObject(_laserPrefab, PoolTypes.laserPlayer, _transform.position + new Vector3(0, 0.9f, 0));
                //Instantiate(_laserPrefab, _transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);

            }
            _nextFire = Time.time + _fireRate;

        }

    }

    //POWER UPS EFFECTS!

    public void TripleShootPowerUpOn(float duration)
    {
        canTripleShot = true;
        StartCoroutine(TrippleShootCountDown(duration));
        //Play FX!
        LevelController.instance.audioController.PlaySound(Sounds.tripleShootSound);
    }

    public void SpeedPowerUpOn(float duration)
    {
        _speedPlayer *= 2f;
        StartCoroutine(SpeedCountDown(duration));
        thurster.SetActive(true);
        //Play FX!
        LevelController.instance.audioController.PlaySound(Sounds.speedUpSound);

    }

    public void ShieldPowerUpOn(float duration)
    {
        health.isShieldActive = true;
        shield.SetActive(true);
        StartCoroutine(ShieldCountDown(duration));
        //Play FX!
        LevelController.instance.audioController.PlaySound(Sounds.shieldSound);
    }

    public void ExtraLifePowerUpOn()
    {
        health.ExtraLife();
        LevelController.instance.uiController.RefreshLives(health.currentHealth);
        LevelController.instance.audioController.PlaySound(Sounds.extraLifeSound);
    }


    public IEnumerator ShieldCountDown(float duration)
    {
        //ponemos el escudo en blanco al empezar la duración
        shield.GetComponent<SpriteRenderer>().color = Color.white;

        //hacer 2 paradas de duración (de la mitad de duración) del shield
        yield return new WaitForSeconds(duration/2);

        //poner en rojo

        shield.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(duration/2);

        //desactivamos el escudo
        health.isShieldActive = false;
        shield.SetActive(false);

    }

    public IEnumerator SpeedCountDown(float duration)
    {
        
        yield return new WaitForSeconds(duration);
        _speedPlayer /= 2f;
        thurster.SetActive(false);
    }

    //hold 10 seconds to trigger the triple shot with coroutine 
    public IEnumerator TrippleShootCountDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canTripleShot = false;
    }

}
