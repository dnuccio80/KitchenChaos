using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    public static event EventHandler OnAnyObjectDropedHere;
    
    public static void ResetStaticDataManager()
    {
        OnAnyObjectDropedHere = null;
    }


    [SerializeField] protected Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate()");

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;

        if(kitchenObject != null)
        {
            OnAnyObjectDropedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
