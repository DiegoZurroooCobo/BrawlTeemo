using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;
public class ShotBullet : MonoBehaviourPunCallbacks
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
        if (!photonView.IsMine )
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = bulletPool.GimmeInactiveGameObject();

            if (obj)
            {
                obj.transform.position =new  Vector3(transform.position.x,transform.position.y+2,transform.position.z);
                obj.transform.rotation =transform.rotation;
                obj.GetComponent<Bullet>().SetDirection(transform.forward);
                obj.SetActive(true); //quitar el boli del estuche, ya no esta disponible en la poool
            }
        }
    }
}
