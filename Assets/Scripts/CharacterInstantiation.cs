using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInstantiation : MonoBehaviour
{
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
                character = new Ziggs("Prefabs/Bziggs2", 25, 100);
                break;
        }
        //EL PREFAB NO SE PUEDE CARGAR DE PRIMERAS DEBIDO A QUE SI LO CARGAS 2 VECES DA ERROS PQ NO HAY QUE LEERLO 2 VECES
    PhotonNetwork.Instantiate(character.GetprefabPath(),new Vector3(0f, 5f, 0f), Quaternion.identity, 0);//esto para instanciar el compnente con photon debiddo a que si no a la parte online

        //Instantiate(character.GetGO(), new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
