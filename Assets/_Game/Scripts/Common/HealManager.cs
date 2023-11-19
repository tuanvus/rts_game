using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealManager : MonoBehaviour
{
    public Action OnDie;
    // Start is called before the first frame update
    public int maxHealth = 100;  // Máu tối đa
    public int currentHealth;     // Máu hiện tại

    public void Initialization()

    {
        currentHealth = maxHealth;

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
    }
}
