using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teemo : Character
{
    public Teemo(string prefabPath, float damage, int health) : base(prefabPath, damage,health)
    {
    
       
    }
    public override float Attack()
    {
        return damage;
    }
    
}
