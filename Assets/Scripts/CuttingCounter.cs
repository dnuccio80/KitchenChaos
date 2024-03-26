using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no a kitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        }
        else
        {

            if (!player.HasKitchenObject())
            {
                // Give the KitchenObject to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            } 
        }

    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            // There is a kitchenObject to cut here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            
        }
    }
}
