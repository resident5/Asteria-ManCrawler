using System.Collections;
using UnityEngine;

public class Player : Character
{

    public enum PlayerState
    {
        DEFAULT,
        GRABBED,
        FUCKED
    }

    public PlayerState pState;

    public int damage;
    public int lustdamage;

    private void Start()
    {
        pState = PlayerState.DEFAULT;
        currentHealth = maxHealth;
        currentLust = 0;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player has lost");
        }
        else if (currentLust >= MaxLust)
        {
            Debug.Log("Player is horny");
        }
    }

    public void TakeDamage(bool health, float damage)
    {
        if (health)
        {
            currentHealth -= damage;
        }
        else
        {
            currentLust += damage;
        }
        if (currentHealth <= 0)
        {
            Debug.Log("Boss Dead");
        }
        else if (currentLust >= 100)
        {
            Debug.Log("Boss Horny");
        }
    }
}
