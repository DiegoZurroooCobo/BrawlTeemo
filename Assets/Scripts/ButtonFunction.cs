using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public void ExitGame() // Permite presionar el boton de Exit aunque el GM haya sido destruido 
    {
        GameManager.instance.ExitGame();
    }
    public void LoadScene(string name)  // Permite presionar el boton de Play aunque el GM sido destruido 
    {
        GameManager.instance.LoadScene(name);
    }

    public void LauncherScene() 
    { 
        GameManager.instance.LeaveRoom();
    }
}
