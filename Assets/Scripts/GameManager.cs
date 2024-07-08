using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance {  get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnLocalGamePaused;
    public event EventHandler OnLocalGameUnpaused;
    public event EventHandler OnMultiplayerGamePaused;
    public event EventHandler OnMultiplayerGameUnpaused;
    public event EventHandler OnGameReset;
    public event EventHandler OnLocalPlayerReadyChanged;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private NetworkVariable<State> state = new NetworkVariable<State>(State.WaitingToStart);
    private NetworkVariable<float> countdownToStartTimer = new NetworkVariable<float>(0f);
    private NetworkVariable<float> gamePlayingTimer = new NetworkVariable<float>(0f);
    private NetworkVariable<bool> isGamePaused = new NetworkVariable<bool>(false);
    private float gamePlayingTimerMax = 90f;
    private float countdownToStartTimerMax = 3f;
    private bool isLocalPlayerReady;
    private bool isLocalGamePaused = false;

    private Dictionary<ulong, bool> playerReadyDictionary;
    private Dictionary<ulong, bool> playerPausedDictionary;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

        playerReadyDictionary = new Dictionary<ulong, bool>();
        playerPausedDictionary = new Dictionary<ulong, bool>();
    }

    public override void OnNetworkSpawn()
    {
        state.OnValueChanged += State_OnValueChanged;
        isGamePaused.OnValueChanged += IsGamePaused_OnValueChanged;
    }

    private void IsGamePaused_OnValueChanged(bool previousValue, bool newValue)
    {
        if (isGamePaused.Value)
        {
            Time.timeScale = 0f;
            OnMultiplayerGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnMultiplayerGameUnpaused?.Invoke(this, EventArgs.Empty);

        }
    }

    private void State_OnValueChanged(State previousValue, State newValue)
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state.Value == State.WaitingToStart)
        {
            isLocalPlayerReady = true;
            OnLocalPlayerReadyChanged?.Invoke(this, EventArgs.Empty);
            
            SetPlayerReadyServerRpc();
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;

        bool allClientsAreReady = true;

        foreach(ulong clientID in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if(!playerReadyDictionary.ContainsKey(clientID) || !playerReadyDictionary[clientID]) 
            {
                // This player is not ready
                allClientsAreReady = false;
                break;
            }
        }

        if(allClientsAreReady) state.Value = State.CountdownToStart;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        if (!IsServer) return;

        switch(state.Value)
        {
            case State.WaitingToStart:
                countdownToStartTimer.Value = countdownToStartTimerMax;
                break;
            case State.CountdownToStart:
                countdownToStartTimer.Value -= Time.deltaTime;
                if (countdownToStartTimer.Value < 0f)
                {
                    state.Value = State.GamePlaying;
                    gamePlayingTimer.Value = gamePlayingTimerMax;
                } 
                break;
            case State.GamePlaying:
                gamePlayingTimer.Value -= Time.deltaTime;
                if (gamePlayingTimer.Value < 0f)
                {
                    state.Value = State.GameOver;
                }
                break;
            case State.GameOver:
                break;
        }

    }

    public bool IsGamePlaying()
    {
        return state.Value == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state.Value == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state.Value == State.GameOver;
    }

  
    public bool IsLocalPlayerReady() 
    {
        return isLocalPlayerReady;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer.Value;
    }

    public float GetGameTimerNormalized()
    {
        return (gamePlayingTimer.Value / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isLocalGamePaused = !isLocalGamePaused;

        if(isLocalGamePaused)
        {
            PauseGameServerRpc();
            OnLocalGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            UnpauseGameServerRpc();
            OnLocalGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void PauseGameServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerPausedDictionary[serverRpcParams.Receive.SenderClientId] = true;
        TestGamePausedState();
    }

    [ServerRpc(RequireOwnership = false)]
    private void UnpauseGameServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerPausedDictionary[serverRpcParams.Receive.SenderClientId] = false;
        TestGamePausedState();
    }

    private void TestGamePausedState()
    {
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (playerPausedDictionary.ContainsKey(clientId) && playerPausedDictionary[clientId])
            {
                // This player is paused
                isGamePaused.Value = true;
                return;
            }
        }

        // All player are unpaused  
        isGamePaused.Value = false;


    }

    public void ResetGame()
    {
        isLocalGamePaused = false;
        state.Value = State.WaitingToStart;
        OnGameReset?.Invoke(this, EventArgs.Empty);
    }

}
