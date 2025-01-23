using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teemo : Character
{
    public Teemo(string name, float damage, int health) : base(name, damage,Resources.Load<Mesh>("Prefabs/beemo2__"),health)
    {
    
    }
    public override float Attack()
    {
        return damage;
    }
}
