﻿using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    public int damage;

    private int halfHealth;
    private Animator anim;

    private Slider healthBar;

    void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;

        if (health <= 0)
        {
            // TODO instantiate blood effect and particle effect here
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        Enemy ranEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(ranEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
