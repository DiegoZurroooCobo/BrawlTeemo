using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject localPlayerInstance;
    public float health;
   

    private void Awake()
    {
        if (photonView.IsMine)
        {
            localPlayerInstance = gameObject;
        }
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        CameraWork _camerawork = gameObject.GetComponent<CameraWork>(); //se coge el componente de Camerawork 

        if (_camerawork) // si camera work 
        {
            if (photonView.IsMine) // si yo tengo PhotonView
            {
                _camerawork.OnStartFollowing(); //el camera comienza a seguir al personaje 
            }

        }
        else
        {
            Debug.LogError("El componente CameraWork en el prefab ", this); // si da error sale un debug 
        }

#if UNITY_5_4_OR_NEWER
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }
#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode) // metodo OnSceneLoaded 
    {
        CalledOnLevelWasLoaded(scene.buildIndex); // llama al metodo CalledOnLevelWasLoaded
        GetComponentInChildren<GameObjectPool>()?.DelayInstantiation(); // la ? significa que si se cumple lo de la izquierda hace lo de la derecha 
    }
    void OnLevelWasLoaded(int level)
    {
        CalledOnLevelWasLoaded(level);
    }
    void CalledOnLevelWasLoaded(int level) // Metodo calledOnLevelWasLoaded

    {
        //Comprueba si estamos fuera de la arena, y si es el caso, nos spawnea en la arena en una safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f)) //hace un raycast hacia abajo para comprobar si estamos encima del escenario o no. Si no, cambia la posicion inicial 
        {
            transform.position = new Vector3(0f, 5f, 0f); // posicion inicial al comenzar 
        }
    }
    public override void OnDisable() // metodo OnDisable 
    {
        //siempre llama la base para quitar los callbacks 
        base.OnDisable();
        if ("Room for 2" == SceneManager.GetActiveScene().name)
        {
            GameManager.instance.Victory();
            Destroy(gameObject);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            if (health <= 0f)
            { 
                
               PhotonNetwork.LoadLevel("Losing scene");
            
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            health -= bullet.GetDamage();
        }
      
    }
#endif
}
