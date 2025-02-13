using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxTime;
    public float damage = 10f;

    private float currentTime;
    private Vector3 _dir;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            GetComponent<PoolObject>().readyToUse = true;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = speed * _dir;
    }

    public void SetDirection(Vector3 value)
    {
        _dir = value;
    }

    public void SetDamage(float value) 
    { 
        damage = value; 
    }
    public float GetDamage()
    {
        return damage;  
    }



}
