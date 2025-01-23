using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character
{
    public float health = 100;
    private GameObject gO;
    private string name;
    protected float damage;
    //protected float JumpForce;

    public Character(string name, float damage,GameObject gO,int health)
    {
        this.name = name;
        this.damage = damage;
        this.gO = gO;
    }

    public string GetName() { return name; }
    public float GetDamage() { return damage; }

    public abstract float Attack(); // Metodo abstracto que heredan los objetos

    public GameObject GetGO() { return gO; } 
}
