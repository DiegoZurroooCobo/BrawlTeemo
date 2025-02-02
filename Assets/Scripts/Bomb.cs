using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed, throwForce;
    public float maxTime;

    private float currentTime;
    private Vector3 dir;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
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
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        } 
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * dir;
    }
    public void ApplyParabolicThrow(Transform ownerTransform)
    {
        rb.AddForce((ownerTransform.forward + Vector3.up) * throwForce);
    }
}
