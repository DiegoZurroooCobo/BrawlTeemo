using System.Collections;
using System.Collections.Generic;
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
                character = new Teemo("Beemo", 10, 100);
                break;

            case 1:
                character = new Ziggs("bzzigss", 25, 100);
                break;
        }

        Instantiate(character.GetGO(), new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
