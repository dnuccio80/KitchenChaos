using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string PLAYER_PREFS_CURRENT_RECORD = "CurrentRecordRecipesDelivered";

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI newRecordText;
    [SerializeField] private TextMeshProUGUI currentRecordText;
    [SerializeField] private Button newGameButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetGame();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDeliveredSuccess().ToString();
            CheckRecord();
        } else
        {
            Hide();
        }
    }


    private void Show()
    {
        gameObject.SetActive(true);
        newGameButton.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void CheckRecord()
    {
        int currentRecord = PlayerPrefs.GetInt(PLAYER_PREFS_CURRENT_RECORD, 0);
        int currentRecipesDeliveredSuccess = DeliveryManager.Instance.GetRecipesDeliveredSuccess();


        if ( currentRecipesDeliveredSuccess > currentRecord)
        {
            // There is a new record 
            newRecordText.text = "BEST NEW RECORD!";
            currentRecordText.text = "CURRENT RECORD: " + currentRecipesDeliveredSuccess + " RECIPES DELIVERED";
            PlayerPrefs.SetInt(PLAYER_PREFS_CURRENT_RECORD, currentRecipesDeliveredSuccess);
            PlayerPrefs.Save();   
        }
        else
        {
            // There is no a new record
            newRecordText.text = "";
            currentRecordText.text = "CURRENT RECORD: " + currentRecord + " RECIPES DELIVERED";
        }


    }
}
