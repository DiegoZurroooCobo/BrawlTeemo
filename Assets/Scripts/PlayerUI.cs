using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshProUGUI playerNameText;

    [SerializeField]
    private Slider playerHealthSlider;

    private PlayerManager target;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<PlayerManager>(); // target coge el componente de player Manager 

        playerNameText.text = photonView.Owner.NickName; // el nombre del personaje se iguala a la variable del nombre que se ha puesto el jugador 
    }


    // Update is called once per frame
    void Update() // el slider de la vida se iguala a la vida del personaje 
    {
        playerHealthSlider.value = target.health / 100;
    }
}
