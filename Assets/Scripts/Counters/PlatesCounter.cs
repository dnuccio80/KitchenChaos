using System;
using Unity.Netcode;
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

    private void Start()
    {
        GameManager.Instance.OnGameReset += GameManager_OnGameReset;
    }

    private void GameManager_OnGameReset(object sender, EventArgs e)
    {
        plateSpawnedAmount = 0;
        spawnPlateTimer = 0;

        foreach(Transform child in counterTopPoint)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (!IsServer) return;

        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > spawnPlateTimerMax )
        {
            spawnPlateTimer = 0f;

            if(GameManager.Instance.IsGamePlaying() && plateSpawnedAmount < plateSpawnedAmountMax)
            {
                SpawnPlateServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlateServerRpc()
    {
        SpawnPlateClientRpc();
    }

    [ClientRpc]
    private void SpawnPlateClientRpc()
    {
        plateSpawnedAmount++;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {

            if(plateSpawnedAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                InteractLogicServerRpc();
            }


        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc()
    {
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        

        plateSpawnedAmount--;

        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }




}
