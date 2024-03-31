using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter, IKitchenObjectParent
{
    public static PlatesCounter Instance {  get; private set; }

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > spawnPlateTimerMax )
        {
            spawnPlateTimer = 0f;

            if(GameManager.Instance.IsGamePlaying() && plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {

            if(plateSpawnedAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                plateSpawnedAmount--;

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }


        }

    }


}
