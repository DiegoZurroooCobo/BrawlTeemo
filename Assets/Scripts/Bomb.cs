using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float throwForce;
    public float maxTime;
    public float damage = 25f;

    private float currentTime;
    private Vector3 dir;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            ResetVelocity();
            GetComponent<PoolObject>().readyToUse = true;
        }
    }
    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }
    public void ApplyParabolicThrow(Transform ownerTransform)
    {
        rb.AddForce((ownerTransform.forward + Vector3.up) * throwForce);
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
