using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private SoundListSO soundListSO;
        
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
        audioSource.clip = soundListSO.stoveFrying;

    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeArgs e)
    {
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            audioSource.Play();
        }

        if(e.state == StoveCounter.State.Idle || e.state == StoveCounter.State.Burned)
        {
            audioSource.Stop();
        }
    }
}
