using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    public KitchenObjectsSO KitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }
}