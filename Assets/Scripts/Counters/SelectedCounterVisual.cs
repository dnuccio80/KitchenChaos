using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounterVisual;

    private void Start()
    {
        //Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnClearCounterInteractionChangeArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        } else
        {
            Hide();
        }
    } 

    private void Show()
    {
        foreach(GameObject visual in selectedCounterVisual)
        {
            visual.SetActive(true);
        }
        
    }

    private void Hide()
    {
        foreach (GameObject visual in selectedCounterVisual)
        {
            visual.SetActive(false);
        }
    }
}
