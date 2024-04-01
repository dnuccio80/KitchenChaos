using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string DELIVERY_SUCCESS = "Delivery\r\nSuccess";
    private const string DELIVERY_WRONG = "Delivery\r\nWrong";
    private const string DELIVERY_MADE = "DeliveryMade";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI deliveryText;

    [Header("Utilities")]
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += GameManager_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        backgroundImage.color = Color.green;
        iconImage.sprite = successSprite;
        deliveryText.text = DELIVERY_SUCCESS;
        Show();
        animator.SetTrigger(DELIVERY_MADE);

    }
    private void GameManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        backgroundImage.color = Color.red;
        iconImage.sprite = failedSprite;
        deliveryText.text = DELIVERY_WRONG;
        Show();
        animator.SetTrigger(DELIVERY_MADE);

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
