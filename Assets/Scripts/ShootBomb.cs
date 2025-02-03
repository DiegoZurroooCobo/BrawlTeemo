using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ShootBomb : MonoBehaviour
{
    GameObjectPool bombPool;
    Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        bombPool = GetComponentInChildren<GameObjectPool>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            GameObject obj = bombPool.GimmeInactiveGameObject();
            if(obj) 
            { 
                obj.SetActive(true);
                Vector3 vectorAttack = transform.position;
                vectorAttack.z += transform.position.z;
                vectorAttack.y += transform.position.y;
                obj.transform.position = vectorAttack;

                Bomb bomb = obj.GetComponent<Bomb>();
                bomb.ResetVelocity();
                bomb.ApplyParabolicThrow(_transform);
            }
        }
    }
}
