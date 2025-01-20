using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ziggs : Character
{
    public Ziggs(string name, float damage, Mesh mesh) : base(name, damage, mesh)
    {
    }
    public override float Attack()
    {
        return damage;
    }
    public void ThrowBomb()
    {
        Debug.Log("Ziggs throws a bomb");
        {

        }
    }
}
