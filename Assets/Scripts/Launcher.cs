using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject buttonControl /*anti alonso*/, loadingElements;
    #region Private Serializable Fields
    [Tooltip("El nuemro maximo de jugadors por room. Cuando la room esta llena, no se podran unir jugadores nuevos, asi que se crea una nueva room")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    
    #endregion

    #region Private Fields

    // el numero de version de este cliente. Los usuarios son separados por la gameVersion

    string gameVersion = "1.0";

    #endregion

    #region MonoBehaviour Callbacks 

    private void Awake()
    {
        // esto se asegura que todos los metodos de PhotonNetwork.LoadLevel() en el cliente maestro y todos los clientes en la misma room
        // sincronizan sus niveles automaticamente 
        PhotonNetwork.AutomaticallySyncScene = true;    
    }

    private void Start()
    {
        loadingElements.SetActive(false);
        buttonControl.SetActive(true);
    }

    #endregion

    #region Public Methods 
    // Comienza el proceso de conexion
    // Si ya esta conectado, nos intentamos unir a una room aleatoria
    // si no esta conectado ya, conecta esta aplicacion con Photon Cloud Network
    public void Connect() 
    {
        loadingElements.SetActive(true);
        buttonControl.SetActive(false);
        // comprueba si estamos conectados o no, nos unimos si estamos conectados,
        // e iniciamos la conexion al servidor
        if (PhotonNetwork.IsConnected)
        {
            // tenemos que intentar unirnos una room aleatorio.
            // Si falla, recibiremos una notificacion y creara una 
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // primero debemos conectar con el Photon Online Server 
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;    
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks CallBacks 

    public override void OnConnectedToMaster()
    {
        // lo primero que intentamos hcaer es conectarnos a una room ya creada. Si hay una, guay chachi. Si no, nos llamara de vuelta a OnJoinRandomFailed()
       PhotonNetwork.JoinRandomRoom();
       Debug.Log("OnconnectedToMaster fue llamado por el PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("La conexion fallo :O por la razon ", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Se ha llamado a OnJoinRandomFiled(). No hay rooms aleatorias disponibles, asi que se ha creado una ;)");
        // fallo al unirse a una room aleatoria. Puede que no exita o que esten llenas. Se crea una nueva 
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() fue llamado por PUN. Ahora este cliente esta en una room :p");
    }

    #endregion
}
