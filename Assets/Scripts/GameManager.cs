using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager instance;
    public KeyCode Escape;
    public GameObject playerPrefab;
    public uint[] playerIndex;



    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerIndex = new uint[2];
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        if (playerPrefab == null)//TODO ES PARA INSTACIAR
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            //if (PlayerManager.localPlayerInstance == null)
            //{
            //    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

            //    PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            //}
            //else

            //{
            //    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            //} 
        }
        //ahora solo instanciamos si PlayerManager no tiene una referenccia a una instancia existente de localPlayer
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
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
    public void SelectCharacter(int Selection)
    {
        playerIndex[0] = (uint)Selection;
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
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount); //Cargamos el nivel con Photon en vez de Unity

    }

    #endregion


}
