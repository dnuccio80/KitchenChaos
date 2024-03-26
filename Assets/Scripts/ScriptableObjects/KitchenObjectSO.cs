using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{

    [SerializeField] public Transform prefab;
    [SerializeField] public Sprite sprite;
    [SerializeField] public string objectName;


}
