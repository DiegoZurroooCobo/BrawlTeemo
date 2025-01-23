using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    //APUNTES
    //APUNTES
    //APUNTES
    //APUNTES
    //APUNTES
    //APUNTES
    //APUNTES
    //APUNTES
    //APUNTES
    public UnityEvent<int, float, string> events;//puede tener todas las vairables que quiera el array rollo
    // Start is called before the first frame update
    void Start()
    {
        events.AddListener(explosion);
        events.Invoke(44,1,"duro");
    }
    public void explosion(int ent, float dec,string mo)
    {
     Debug.Log("explosion"+ent+dec);
    }
}
