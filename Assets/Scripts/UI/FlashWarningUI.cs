using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWarningUI : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    private float warningBurnSoundTimer;
    private float warningBurnSoundTimerMax = .2f;


    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burningWarningTime = .5f;

        if (stoveCounter.IsFried() && e.progressNormalized > burningWarningTime)
        {
            Show();
            PlayWarningSound();
        }
        else Hide(); 

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void PlayWarningSound()
    {
        warningBurnSoundTimer -= Time.deltaTime;

        if (warningBurnSoundTimer <= 0)
        {
            SoundManager.Instance.PlayWarningStoveSound(stoveCounter.transform.position);
            warningBurnSoundTimer = warningBurnSoundTimerMax;
        }
    }


}
