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
    public uint[] playerIndex;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            playerIndex = new uint[2];
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
  
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
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

    public void Victory()
    {
        PhotonNetwork.LoadLevel("Winning scene");
    }

    #endregion

    public void LoadScene(string SceneName) // Te lleva a la escena que te selecciones como la primera
    {
        //AudioManager.instance.PlayAudio(enterClip, "enterClip");
        SceneManager.LoadScene(SceneName);
        // Limpia todos los sonidos que estan sonando 
    }

    public void ExitGame() // Te permite salir del menu del juego 
    {
        Debug.Log("Exit!");
        //AudioManager.instance.PlayAudio(exitClip, "exitClip");
        Application.Quit();
    }

    public void Defeat()
    {
        PhotonNetwork.LoadLevel("Losing scene");

    }


}
