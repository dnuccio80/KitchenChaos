using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    private void Start()
    {
        GameManager.Instance.OnGameReset += GameManager_OnGameReset;
    }

    private void GameManager_OnGameReset(object sender, EventArgs e)
    {
        if(GetKitchenObject() != null)
        {
            GetKitchenObject().DestroySelf();
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no a kitchenObject here
            if(player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }           

        } else
        {
            
            if(!player.HasKitchenObject())
            {
                // Player has not a kitchenObject

                // Give the KitchenObject to the player
                GetKitchenObject().SetKitchenObjectParent(player);

            } else
            {
                // Player is carrying something
                
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();


                    }
                } else
                {
                    // Player is carrying something, but not a plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }

                    }
                }
            }


        }
    }
}
