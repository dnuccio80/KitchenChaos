using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarUIAnimation : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burningWarningTime = .5f;
        bool show = e.progressNormalized > burningWarningTime;

        if(stoveCounter.GetFriedStateIsActive()) animator.SetBool(IS_FLASHING, show); 

    }

}
