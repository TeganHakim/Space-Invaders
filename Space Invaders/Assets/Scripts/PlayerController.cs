using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Bullet bullet_Scr;
    public float speed;
    public float horizontalInput;
    //The screen size will change so i made this a editable field so you can change it to fit your screen
    public int ScreenRestrict = 50;
    public float position;   

    public KeyCode turnRight;
    public KeyCode turnLeft;
    public float turnSpeed;

    public GameObject bullet;
    public Transform player;
    public Transform ShotPoint;
    public float fireRate = 0.55f;
    private float nextFire;

    public static int lives = 3;
    public static bool gameOver;
    public GameObject Died;

    //Hearts
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    //Sounds
    public AudioSource laser_Sound;
    public AudioSource explosion_Sound;

    //Particle System    
    public ParticleSystem explosion_Particle;

    //Speed Powerup   
    private float Startdelay_speed = 8f;
    private float Invokingdelay_speed;
    public GameObject speedPowerup;

    //Health Powerup
    private float Startdelay_health = 12f;
    private float Invokingdelay_health;
    public GameObject healthPowerup;

    private void Start()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        lives = 3;

        Invokingdelay_speed = Random.Range(9f, 15f);
        InvokeRepeating("SpeedPowerup", Startdelay_speed, Invokingdelay_speed);

        Invokingdelay_health = Random.Range(13f, 21f);
        InvokeRepeating("HealthPowerup", Startdelay_health, Invokingdelay_health);
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        if (transform.position.x < -ScreenRestrict)
        {
            transform.position = new Vector3(-ScreenRestrict, transform.position.y, transform.position.z);
        }
        if (transform.position.x > ScreenRestrict)
        {
            transform.position = new Vector3(ScreenRestrict, transform.position.y, transform.position.z);
        }

        //Rotate
        if (Input.GetKey(turnRight))
        {
            transform.Rotate(Vector3.back * 1 * turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(turnLeft))
        {
            transform.Rotate(Vector3.back * -1 * turnSpeed * Time.deltaTime);
        }
        if (transform.position.y != position)
        {
            transform.position = new Vector3(transform.position.x, position, transform.position.z);
        }

        //Shoot
        if (!gameOver)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
            {
                laser_Sound.Play();
                Instantiate(bullet, new Vector3(ShotPoint.position.x, ShotPoint.position.y, ShotPoint.position.z), player.rotation);
                nextFire = Time.time + fireRate;
            }
        }        

        if (lives == 0)
        {
            gameOver = true;
            Died.SetActive(true);            
        }
        if (lives == 1)
        {
            gameOver = false;
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        if (lives == 2)
        {
            gameOver = false;
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        if (lives == 3)
        {
            gameOver = false;
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }

    }

    void ChangeFireRate()
    {
        fireRate = 0.55f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            explosion_Sound.Play();            
            explosion_Particle.Play();
            bullet_Scr.EnemyRespawn();
            Destroy(collision.collider.gameObject);
            lives = lives - 1;
        }
        
        if (collision.collider.tag == "Speed_Powerup")
        {
            Destroy(collision.collider.gameObject);
            fireRate = 0.25f;
            Invoke("ChangeFireRate", 5f);
        }

        if (collision.collider.tag == "Health_Powerup")
        {
            Destroy(collision.collider.gameObject);
            lives = 3;
        }
    }

    void SpeedPowerup()
    {
        Instantiate(speedPowerup, new Vector3(Random.Range(-6, 6), player.position.y, player.position.z), Quaternion.identity);
    }
    void HealthPowerup()
    {
        Instantiate(healthPowerup, new Vector3(Random.Range(-6, 6), player.position.y, player.position.z), Quaternion.identity);
    }

}
