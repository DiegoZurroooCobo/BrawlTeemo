using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class SetPlayerInputField : MonoBehaviour
{
    const string playerNameKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        string defaultName = "nombre default :p";
        TMP_InputField  _InputField =  GetComponent<TMP_InputField>();

        if(_InputField) 
        { 
            //Playerprefs = settings del usuario
            if(PlayerPrefs.HasKey(playerNameKey)) 
            { 
                defaultName = PlayerPrefs.GetString(playerNameKey);
                _InputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName() 
    {
        TMP_InputField _InputField = GetComponent<TMP_InputField>();
        string playerName = _InputField.text; // lo que ha introducido el usuariio

        if(!string.IsNullOrEmpty(playerName)) 
        { 
            PlayerPrefs.SetString(playerNameKey, playerName);
            PhotonNetwork.NickName = playerName;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
