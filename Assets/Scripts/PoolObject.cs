using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PoolObject : MonoBehaviourPunCallbacks, IPunObservable 
{
    public bool readyToUse = true; 

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)//este metodo es pu metodo nuestro parar añadir a photon
    {
        if (stream.IsWriting) // este es el que escribe la informacion  
        {
            stream.SendNext(readyToUse); //
            gameObject.SetActive(!readyToUse);
        }
        else // este es el que recibe la informacion 
        {
            readyToUse = (bool)stream.ReceiveNext();
            gameObject.SetActive(!readyToUse);
        }
    }
}
