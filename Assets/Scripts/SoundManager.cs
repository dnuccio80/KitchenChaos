using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance {  get; private set; }


    [SerializeField] private SoundListSO soundListSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnAnyObjectDropedHere += BaseCounter_OnAnyObjectDropedHere;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        PlatesCounter.Instance.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
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

    private void PlaySoundArray(AudioClip[] clips, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], position, volume);
    }
    public void PlayFootStep(Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(soundListSO.footStepArray[Random.Range(0, soundListSO.footStepArray.Length)], position, volume);
    }


}
