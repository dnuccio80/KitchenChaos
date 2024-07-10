using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectedUI : MonoBehaviour
{

    [SerializeField] private Button newGameButton;

    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log("ClientId: " + clientId);
        Debug.Log("ServerClientId: " + NetworkManager.ServerClientId);
        if(clientId == NetworkManager.ServerClientId)
        {
            //Server is shutting down
            Debug.Log("se desconectó el servidor");
            Show();
        }
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
