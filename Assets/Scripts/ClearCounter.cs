using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            // Give the KitchenObject to the player
            //GetKitchenObject().SetKitchenObjectParent(player);

        }
    }
}
