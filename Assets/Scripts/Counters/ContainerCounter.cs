using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    public static event EventHandler OnObjectPicked;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        //if (!Player.Instance.HasKitchenObject())
        //{

        //    KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        //    OnObjectPicked?.Invoke(this, EventArgs.Empty);
        //    OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        //}

    }

}
