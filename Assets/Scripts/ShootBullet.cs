using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;
public class ShootBullet : MonoBehaviourPunCallbacks
{
    GameObjectPool bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponentInChildren<GameObjectPool>(); // pilla el componente GameObjectPool 
    }

    // Update is called once per frame
    void Update()
    {
#if  UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
#elif UNITY_ANDROID
        foreach(Touch touch in Input.touches) 
        {
            if (touch.phase == TouchPhase.Began)
            {
                Shoot();
            }
        }
#endif
    }

    private void Shoot()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) // si photon.View no es mio Y PhotonNetwork esta conectado 
        {
            return;
        }

        GameObject obj = bulletPool.GimmeInactiveGameObject(); // se iguala el obj al metodo GimmeInactiveGameObject de bullet pool 

        if (obj) // si obj 
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 3f); // cambia la posicion del obj a un nuevo Vector3
            obj.transform.rotation = transform.rotation; // cambia la rotacion del obj 
            obj.GetComponent<Bullet>().SetDirection(transform.forward); // Settea la direccion del objeto usando el metodo SetDirection() hacia adelante  
            obj.SetActive(true); //quitar el boli del estuche, ya no esta disponible en la poool

            obj.GetComponent<PoolObject>().readyToUse = false; // setea el readyToUse del poolobject a false
        }
    }
}
