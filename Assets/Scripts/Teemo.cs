using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teemo : Character
{
    public Teemo(string name, float damage, Mesh mesh) : base(name, damage, mesh)
    {
    
    }
    public override float Attack()
    {
        return damage;
    }
}
