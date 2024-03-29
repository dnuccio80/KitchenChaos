using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private SoundListSO soundListSO;

    private void Start()
    {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        CuttingCounter.OnObjectDropedHere += CuttingCounter_OnObjectDropedHere;
        CuttingCounter.OnObjectPicked += CuttingCounter_OnObjectPicked;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        ClearCounter.OnAnyObjectPicked += ClearCounter_OnAnyObjectPicked;
        ClearCounter.OnAnyObjectDroped += ClearCounter_OnAnyObjectDroped;
        PlatesCounter.Instance.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        ContainerCounter.OnObjectPicked += ContainerCounter_OnObjectPicked;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySoundArray(soundListSO.trashArray, trashCounter.gameObject.transform.position);

    }

    private void ContainerCounter_OnObjectPicked(object sender, System.EventArgs e)
    {
        ContainerCounter containerCounter = (ContainerCounter)sender;
        PlaySoundArray(soundListSO.objectPickUpArray, containerCounter.gameObject.transform.position);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        PlaySoundArray(soundListSO.objectPickUpArray, PlatesCounter.Instance.transform.position);
    }

    private void ClearCounter_OnAnyObjectDroped(object sender, System.EventArgs e)
    {
        ClearCounter clearCounter = (ClearCounter)sender;
        PlaySoundArray(soundListSO.objectDropArray, clearCounter.gameObject.transform.position);
    }

    private void ClearCounter_OnAnyObjectPicked(object sender, System.EventArgs e)
    {
        ClearCounter clearCounter = (ClearCounter)sender;
        PlaySoundArray(soundListSO.objectPickUpArray, clearCounter.gameObject.transform.position);
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

    private void CuttingCounter_OnObjectPicked(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySoundArray(soundListSO.objectPickUpArray, cuttingCounter.gameObject.transform.position);
    }

    private void CuttingCounter_OnObjectDropedHere(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySoundArray(soundListSO.objectDropArray, cuttingCounter.gameObject.transform.position);
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


}
