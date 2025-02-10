using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character
{
    private string prefabPath;
    //protected float JumpForce;

    public Character(string prefabPath)
    {
        this.prefabPath = prefabPath;
    }

    public string GetprefabPath() { return prefabPath; }

    public virtual void Attack() 
    {
        return;
    }

}
