using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character
{
    public float health = 100;
    private GameObject gO;
    private string prefabPath;
    protected float damage;
    //protected float JumpForce;

    public Character(string prefabPath, float damage,int health)
    {
        this.prefabPath = prefabPath;
        this.damage = damage;
    }

    public string GetprefabPath() { return prefabPath; }
    public float GetDamage() { return damage; }

    public abstract float Attack(); // Metodo abstracto que heredan los objetos

}
