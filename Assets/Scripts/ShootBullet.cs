using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;
    public class ShotBullet : MonoBehaviour
    {
        GameObjectPool bulletPool;

        // Start is called before the first frame update
        void Start()
        {
            bulletPool = GetComponentInChildren<GameObjectPool>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject obj = bulletPool.GimmeInactiveGameObject();

                if (obj)
                {
                    obj.SetActive(true); //quitar el boli del estuche, ya no esta disponible en la poool
                    obj.transform.position = transform.position;
                    obj.GetComponent<Bullet>().SetDirection(transform.forward);
                }
            }
        }
    }
