using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameObjectPool : MonoBehaviourPunCallbacks
{
    [Tooltip("Object that will go to the pool")]
    public GameObject objectToPool; //Objecto (en este caso tuberias) que añadimos a la pool
    [Tooltip("Initial pool size")]
    public uint poolSize; //unit = "unisigned int"
    [Tooltip("If true, size increments")]
    public bool shouldExpand = false; //Opcion de expandir la lista, por defecto viene falso, es lo mejor

    private List<GameObject> _pool; //lista de GameObjects

    private void Awake()
    {
        _pool = new List<GameObject>(); //instanciamos la lista

        for (int i = 0; i <= poolSize; i++) //Instancia X objectos al inicio
        {
            AddGameObjectToPool(); // llama al metodo AddGameObjectToPool 
        }
    }

    public GameObject GimmeInactiveGameObject() // metodo GameObject que da gameObject 
    {
        foreach (GameObject obj in _pool) //por cada objeto en la pool 
        {
            if (obj.GetComponent<PoolObject>().readyToUse) //Si el objecto NO esta activado (desatcivado)
            {
                return obj; //Si el objetco no es activo lo damos
            }
        }

        if (shouldExpand) //Si deberia de expandirse la pool se instancia un nuevo objecto
        {
            return AddGameObjectToPool(); // se devuelve el metodo 
        }

        return null; //Si no encontramos nada devolvemos NULL, osea nada.
    }

    private GameObject AddGameObjectToPool() //añadir GameObject a la pool
    {
        GameObject clone = PhotonNetwork.Instantiate("Prefabs/" + objectToPool.name, transform.position, Quaternion.identity);
        clone.SetActive(false); // desactivamos el objecto para que no se utilice de primeras, consume menos recuros asi
        _pool.Add(clone); // lo guardamos en la lista

        return clone;
    }
}
