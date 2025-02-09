using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text playerNameText;

    [SerializeField]
    private Slider playerHealthSlider;

    private PlayerManager target;
    private Character character;

    [SerializeField]
    private Vector3 screenOffSet = new Vector3(0f, 30, 0f);

    private float characterControllerHeight = 0f;
    private Transform targetTransform;
    private Renderer targetRenderer;
    private CanvasGroup _canvasGroup;
    private Vector3 targetPosition;

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        _canvasGroup = GetComponent<CanvasGroup>();
        character = GetComponent<Character>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetTarget (PlayerManager _target)
    {
        if(target == null) 
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        target = _target;

        targetTransform = target.GetComponent<Transform>();
        targetRenderer = target.GetComponent<Renderer>();
        CharacterController charController = _target.GetComponent<CharacterController>();

        if (charController != null)
        {
            characterControllerHeight = charController.height;
        }

        if(playerNameText != null) 
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = character.health;
        }

        if(target == null) 
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void LateUpdate()
    {
        if (targetRenderer != null)
        {
            _canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
        }

        if(targetTransform != null) 
        { 
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffSet;
        }
    }
}
