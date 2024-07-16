using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI lobbyTextName;
    [SerializeField] private TextMeshProUGUI lobbyTextCode;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        readyButton.onClick.AddListener(() =>
        {
            CharacterSelectReady.Instance.SetPlayerReady();
        });
    }

    private void Start()
    {
        Lobby lobby = KitchenGameLobby.Instance.GetLobby();

        lobbyTextName.text = "Lobby Name: " + lobby.Name;
        lobbyTextCode.text = "Lobby Code: " + lobby.LobbyCode;
    }




}
