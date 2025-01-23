using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ziggs : Character
{
    public Ziggs(string prefabPath, float damage,int health) : base(prefabPath, damage, health)
    {
    }
    public override float Attack()
    {
        return damage;
    }
    public void ThrowBomb()
    {
        {
         
        }
        Debug.Log("Ziggs throws a bomb");
    }
}
