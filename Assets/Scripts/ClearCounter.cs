using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
   [SerializeField] private Transform counterTopPoint;
   public void Interact()
   {
      Debug.Log("!Interact!");
      Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
      kitchenObjectTransform.localPosition = Vector3.zero;

    Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().KitchenObjectsSO().objectName);  
   }
}
