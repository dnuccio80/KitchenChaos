using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance {  get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "soundEffectsVolume";

    public event EventHandler<OnVolumeChangedArgs> OnVolumeChanged;

    public class OnVolumeChangedArgs : EventArgs
    {
        public float volume;
    }

    [SerializeField] private SoundListSO soundListSO;

    private float volume;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnAnyObjectDropedHere += BaseCounter_OnAnyObjectDropedHere;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        PlatesCounter.Instance.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnObjectTrashed;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        Player player = (Player)sender; 
        PlaySoundArray(soundListSO.objectPickUpArray, player.gameObject.transform.position);
    }

    private void BaseCounter_OnAnyObjectDropedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySoundArray(soundListSO.objectDropArray, baseCounter.gameObject.transform.position);
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySoundArray(soundListSO.trashArray, trashCounter.gameObject.transform.position);

    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        PlaySoundArray(soundListSO.objectPickUpArray, PlatesCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = (DeliveryManager)sender;
        PlaySoundArray(soundListSO.deliveryFailArray, deliveryManager.gameObject.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = (DeliveryManager)sender;
        PlaySoundArray(soundListSO.deliverySuccessArray, deliveryManager.gameObject.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySoundArray(soundListSO.chopSoundArray, cuttingCounter.gameObject.transform.position);
    }

    private void PlaySoundArray(AudioClip[] clips, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(clips[UnityEngine.Random.Range(0, clips.Length)], position, volume * volumeMultiplier);
    }
    public void PlayFootStep(Vector3 position, float volumeMultiplier)
    {
        AudioSource.PlayClipAtPoint(soundListSO.footStepArray[UnityEngine.Random.Range(0, soundListSO.footStepArray.Length)], position, volume * volumeMultiplier);
    }

    public float GetVolume()
    {
        return volume;
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if (volume > 1) volume = 0;

        OnVolumeChanged?.Invoke(this, new OnVolumeChangedArgs
        {
            volume = volume
        });

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

}
