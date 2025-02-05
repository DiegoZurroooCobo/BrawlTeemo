using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInstantiation : MonoBehaviour
{
    public GameObject playerPrefab;

    Character character;
    public int playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.instance.playerIndex[playerIndex])
        {
            case 0:
                character = new Teemo("Prefabs/Beemo", 10, 100);
                break;

            case 1:
                character = new Ziggs("Prefabs/bziggs", 25, 100);
                break;
        }
        //EL PREFAB NO SE PUEDE CARGAR DE PRIMERAS DEBIDO A QUE SI LO CARGAS 2 VECES DA ERROS PQ NO HAY QUE LEERLO 2 VECES
        if (PlayerManager.localPlayerInstance == null)
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

            PhotonNetwork.Instantiate(character.GetprefabPath(), new Vector3(0f, 5f, 0f), Quaternion.identity, 0);//Eto va debido a que lo cargamos con la escena ya cargada y emtpmnces ya va bien
        }
        else

        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    //Instantiate(character.GetGO(), new Vector3(0, 0, 0), Quaternion.identity);
}
}
