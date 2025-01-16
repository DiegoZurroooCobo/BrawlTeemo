using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom() 
    {
        PhotonNetwork.LeaveRoom();
    }
}
