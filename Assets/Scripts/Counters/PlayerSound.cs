using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private Player player;
    private float footStepTimer;
    private float footStepTimerMax = .1f;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;

        if( footStepTimer < 0) {
            
            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootStep(player.transform.position, 1f);
            }

            footStepTimer = footStepTimerMax;
        }

        
    }


}
