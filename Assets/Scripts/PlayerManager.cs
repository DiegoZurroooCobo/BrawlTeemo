using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
      CameraWork cameraWork = this.GetComponent<CameraWork>();
        if(cameraWork) 
        {
            if (photonView.IsMine)
            {
                cameraWork.OnStartFollowing();
            }
            else
            {
                Debug.LogError("El componente CameraWork en el prefab", this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
