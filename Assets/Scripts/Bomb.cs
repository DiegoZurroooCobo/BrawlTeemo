using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float throwForce;
    public float maxTime;
    public float damage = 25f;
    public float radius = 5f;
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
            GetComponent<PoolObject>().readyToUse = true; //coge pool object y usa la variable ready to use  y lo setea a true
            Explode(); // se llama al metodo de la explosion 
            gameObject.SetActive(false); // se desactiva el objeto
        }
    }

    private void Explode() // metodo para generar la explosion 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // collider como esfera
        foreach (Collider collider in colliders) // por cada collider en todos los colliders
        {
            PlayerManager PM = collider.GetComponent<PlayerManager>(); // coge el componente del playerManager
            if (PM) // si el playerManager 
            { 
                PM.health -= damage; // baja la vida del player manager con el daño de la bomba 
            }
        }
    }

    private void OnDrawGizmos() // metodo onDrawGizmos
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius); // dibuja el gizmo del area de la bomba 
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

    public void OnDisable()
    {
        
    }
}
