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
        target = GetComponent<PlayerManager>();


        playerNameText.text = photonView.Owner.NickName;
        

    }


    // Update is called once per frame
    void Update()
    {
        //if (playerHealthSlider != null)
        //{
        //    playerHealthSlider.value = target.health;
        //    Debug.Log("playerHealth no es null");

        //if(target == null) 
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}

    }
}
