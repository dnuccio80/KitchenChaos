using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{

    [SerializeField] private GameInput gameInput;

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeOptionsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [Header("Key Rebinding System - Buttons")]
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [Header("Key Rebinding System - Texts")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeOptionsButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Interact); });
        interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Interact_Alternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Pause); });


    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        float multiplierVisual = 10f;

        soundEffectsText.text = "Sound Effects: " + (Mathf.Round(SoundManager.Instance.GetVolume() * multiplierVisual));
        musicText.text = "Music: " + (Mathf.Round(MusicManager.Instance.GetVolume() * multiplierVisual));

        moveUpText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Up);
        moveUpText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Up);
        moveDownText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Down);
        moveLeftText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Left);
        moveRightText.text = gameInput.GetBindingText(GameInput.Bindings.Move_Right);
        interactText.text = gameInput.GetBindingText(GameInput.Bindings.Interact);
        interactAlternateText.text = gameInput.GetBindingText(GameInput.Bindings.Interact_Alternate);
        pauseText.text = gameInput.GetBindingText(GameInput.Bindings.Pause);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }
    
    private void RebindBinding(GameInput.Bindings binding)
    {
        ShowPressToRebindKey();
        gameInput.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

}
