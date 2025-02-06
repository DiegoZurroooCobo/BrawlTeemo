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
        bombPool = GetComponentInChildren<GameObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1")) 
        {
            GameObject obj = bombPool.GimmeInactiveGameObject();
            if(obj) 
            { 
                obj.SetActive(true);
                Vector3 vectorAttack = transform.position;
                vectorAttack.z+=5; 
                vectorAttack.y += 5;
                obj.transform.rotation = transform.rotation;
                obj.transform.position = vectorAttack;
                //vectorAttack.y += transform.position.y;

                Bomb bomb = obj.GetComponent<Bomb>();
                bomb.ResetVelocity();
                bomb.ApplyParabolicThrow(transform);
            }
        }
    }
}
