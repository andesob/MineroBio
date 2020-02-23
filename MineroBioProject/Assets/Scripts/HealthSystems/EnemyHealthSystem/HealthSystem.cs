using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    private int health;
    private int maxHealth;
    public HealthSystem(int health)
    {
        this.maxHealth = health;
        this.health = health;
    }

    public int getHealth()
    {
        return this.health;
    }
    public float getHealthPercent()
    {
        return (float) health / maxHealth;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) { health = 0; }
        if (OnHealthChanged != null) { OnHealthChanged(this, EventArgs.Empty); }
    }

    public void heal(int heal)
    {
        health += heal;
        if(health > maxHealth){ health = 100; }
        if (OnHealthChanged != null) { OnHealthChanged(this, EventArgs.Empty); }
    }

}
