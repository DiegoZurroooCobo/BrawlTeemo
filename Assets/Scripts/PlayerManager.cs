using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        CameraWork _camerawork = this.GetComponent<CameraWork>();

        if(_camerawork) 
        {
            if(photonView.IsMine) 
            { 
                _camerawork.OnStartFollowing();
            }

        }
        else 
        { 
            Debug.LogError("El componente CameraWork en el prefab ", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
