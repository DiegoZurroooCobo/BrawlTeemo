using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ShootBomb : MonoBehaviourPunCallbacks
{
    GameObjectPool bombPool;

    // Start is called before the first frame update
    void Start()
    {
        bombPool = GetComponentInChildren<GameObjectPool>(); // pilla el componente GameObjectPool 
    }

    // Update is called once per frame
    void Update() 
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetButtonDown("Fire1")) // si se pulsa el boton de dispara 
        {
            Shoot();
        }

#elif UNITY_ANDROID
        foreach(Touch touch in Input.touches) 
        { 
            if(touch.phase == TouchPhase.Began) 
            {
                Shoot();
            }
        }   
#endif

    }

    public void Shoot() 
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) // si photonview no es mio y photonNetwork esta conectado 
        {
            return;
        }

        GameObject obj = bombPool.GimmeInactiveGameObject(); // se iguala el obj al metodo GimmeInactiveGameObject de bullet pool 
        if (obj)
        {
            obj.SetActive(true); // se activae el objeto 

            obj.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 3f); // se instancia en una posicion
            obj.transform.rotation = transform.rotation; // se iguala la rotacion

            Bomb bomb = obj.GetComponent<Bomb>();
            bomb.ResetVelocity();
            bomb.ApplyParabolicThrow(transform); // se usa el metodo tiroParabolico para el disparo de la bomba 
            obj.GetComponent<PoolObject>().readyToUse = false; // la variable readyToUse del pool object se setea a false 
        }
    }
}
