using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private EnemyMaster enemy;
    private float timer;

    public float projectileSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyMaster>();
        transform.position = GameObject.FindWithTag("EnemyProjectileSpawn").transform.position;
        transform.rotation = GameObject.Find("EnemyProjectileSpawn").transform.parent.rotation;

        transform.position = transform.position + transform.forward * 2f;

        gameObject.transform.parent = GameObject.FindWithTag("ProjectileParent").transform;
        timer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile hits the player, deal damage to the player
        if (collision.gameObject.tag == "HurtBox")
        {
            PlayerHealth player = GameObject.FindWithTag("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
            player.TakeDamage(enemy.rangedDmg);

            Destroy(gameObject);
        }
    }
}
