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
        target = GetComponentInParent<PlayerManager>();

        playerNameText.text = photonView.Owner.NickName;
    }


    // Update is called once per frame
    void Update()
    {
        playerHealthSlider.value = target.health / 100;
    }
}
