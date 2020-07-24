using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet
    public Transform bulletTransform;    
    public float bulletSpeed;

    //Enemy
    public GameObject enemyRespawn;
           
    void FixedUpdate()
    {
        bulletTransform.Translate(Vector3.up * bulletSpeed);
        
        if (bulletTransform.position.y > 6.5f)
        {
            Destroy(gameObject);
        }

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {            
            Destroy(other.gameObject);
            EnemyRespawn();
            Destroy(gameObject);
            UIManager.Score += 1;            
        }
    }

    public void EnemyRespawn()
    {
        Vector3 enemyRespawnPosition = new Vector3(Random.Range(-7, 7), 3.6f, 0);
        Instantiate(enemyRespawn, enemyRespawnPosition, Quaternion.identity);
    }
}
