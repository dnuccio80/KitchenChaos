using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [Header("Keyboard Keys")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [Header("Gamepad Keys")]
    [SerializeField] private TextMeshProUGUI gamepad_moveText;
    [SerializeField] private TextMeshProUGUI gamepad_interactText;
    [SerializeField] private TextMeshProUGUI gamepad_interactAlternateText;
    [SerializeField] private TextMeshProUGUI gamepad_pauseText;

    [SerializeField] private GameInput gameInput;

    private void Start()
    {

        gameInput.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        GameManager.Instance.OnGameReset += GameManager_OnGameReset;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnGameReset(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }

    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        moveUpText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Up);
        moveDownText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Down);
        moveLeftText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Left);
        moveRightText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Right);
        interactText.text = gameInput.GetBindingText(GameInput.Bindings.Interact);
        interactAlternateText.text = gameInput.GetBindingText(GameInput.Bindings.Interact_Alternate);
        pauseText.text = gameInput.GetBindingText(GameInput.Bindings.Pause);

        //gamepad_moveText.text = gameInput.GetBindingText(GameInput.Bindings.move);
        gamepad_interactText.text = gameInput.GetBindingText(GameInput.Bindings.Gamepad_Interact);
        gamepad_interactAlternateText.text = gameInput.GetBindingText(GameInput.Bindings.Gamepad_InteractAlternate);
        gamepad_pauseText.text = gameInput.GetBindingText(GameInput.Bindings.Gamepad_Pause);


    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
