using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform enemy;
    public Bullet bullet;
    public float enemySpeed;

    //Shoot
    public GameObject enemy_bullet;
    private float timeBtwShots;
    public float StartTimeBtwShots;

    void Start()
    {
        StartTimeBtwShots = Random.Range(2f, 6f);
        timeBtwShots = StartTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameOver == false)
        {
            enemy.position += Vector3.down * enemySpeed;
        }        

        if (enemy.position.y < -6)
        {
            bullet.EnemyRespawn();
            Destroy(gameObject);
        }
       
        if (timeBtwShots <= 0)
        {
            Instantiate(enemy_bullet, enemy.position, Quaternion.identity);
            timeBtwShots = StartTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


}
