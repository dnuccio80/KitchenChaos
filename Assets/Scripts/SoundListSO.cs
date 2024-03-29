using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundListSO : ScriptableObject
{

    [SerializeField] public AudioClip[] chopSoundArray;
    [SerializeField] public AudioClip[] deliveryFailArray;
    [SerializeField] public AudioClip[] deliverySuccessArray;
    [SerializeField] public AudioClip[] footStepArray;
    [SerializeField] public AudioClip[] objectDropArray;
    [SerializeField] public AudioClip[] objectPickUpArray;
    [SerializeField] public AudioClip stoveFrying;
    [SerializeField] public AudioClip[] trashArray;
    [SerializeField] public AudioClip[] warningArray;

}
