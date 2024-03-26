using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject[] stoveCounterVisualArray;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
        Hide();
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (showVisual) Show();
        else Hide();
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
