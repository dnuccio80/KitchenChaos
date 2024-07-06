using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject[] stoveCounterVisualArray;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChange;
        Hide();
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        (showVisual ? (Action) Show : Hide)();

    }

    private void Show()
    {
        foreach(GameObject visual in stoveCounterVisualArray)
        {
            visual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visual in stoveCounterVisualArray)
        {
            visual.SetActive(false);
        }
    }
}
