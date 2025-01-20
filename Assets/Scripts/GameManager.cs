using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public void Start()
    {
        if (playerPrefab == null)
        {
            //Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);

        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}",SceneManager.GetActiveScene().name);
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() : {0}", other.NickName); // esto no se ve si eres el jugador conectado 

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // se llama antes que OnPlayerLeftRoom 
            LoadArena();
        }
    }
    public void SelectCharacter(int value)
    {

    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // se ve cuando se desconecta otro jugador 

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // se llama antes que OnPlayerLeftRoom
            LoadArena();
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region Private Methods

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient) // primero se comprueba si estamos en el master client
        {
            Debug.LogError("se intenta cargar un nivel pero no estamos en el master client :(");
            return;
        }
        Debug.LogFormat("PhotonNetwork : LoadingLevel {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for  " + PhotonNetwork.CurrentRoom.PlayerCount); //Cargamos el nivel con Photon en vez de Unity

    }

    #endregion


}
