using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBomb : MonoBehaviour
{
    GameObjectPool bombPool;
    // Start is called before the first frame update
    void Start()
    {
        bombPool = GetComponentInChildren<GameObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            GameObject obj = bombPool.GimmeInactiveGameObject();

            if(obj) 
            { 
                obj.SetActive(true);
                obj.transform.position = transform.position;
            }
        }
    }
}
