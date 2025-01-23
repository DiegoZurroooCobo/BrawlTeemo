using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject localPlayerInstance;
    private uint _playerIndex = 0;
    private Character character;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.localPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);

        switch (GameManager.instance.playerIndex[_playerIndex])
        {
            case 0:
                character = new Teemo("Beemo", 10, 100);
                break;

            case 1:
                character = new Ziggs("bzzigss", 25, 100);
                break;



        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraWork _camerawork = this.GetComponent<CameraWork>();

        if (_camerawork)
        {
            if (photonView.IsMine)
            {
                _camerawork.OnStartFollowing();
            }

        }
        else
        {
            Debug.LogError("El componente CameraWork en el prefab ", this);
        }

#if UNITY_5_4_OR_NEWER
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }
#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        CalledOnLevelWasCalled(scene.buildIndex);
    }

    private void CalledOnLevelWasCalled(int buildIndex)
    {
        throw new NotImplementedException();
    }
    public override void OnDisable()
    {
        //siempre llama la base para quitar los callbacks 
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif

#if !UNITY_5_4_OR_NEWER
   
    private void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level); 
    }

#endif

    void CalledOnLevelWasLoaded(int level)
    {
        //Comprueba si estamos fuera de la arena, y si es el caso, nos spawnea en la arena en una safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
    }


}
