using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnGameReset;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float countdownToStartTimer;
    private float countdownToStartTimerMax = 1f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 300f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToStart;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

        state = State.CountdownToStart;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }

    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();

    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                countdownToStartTimer = countdownToStartTimerMax;
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                } 
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }


    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGameTimerNormalized()
    {
        return (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetGame()
    {
        isGamePaused = false;
        state = State.WaitingToStart;
        OnStateChange?.Invoke(this, EventArgs.Empty);
        OnGameReset?.Invoke(this, EventArgs.Empty);
    }

}
