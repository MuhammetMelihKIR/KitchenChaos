using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObject", menuName = "KitchenObject")]
public class KitchenObjectsSO : ScriptableObject
{
   public Transform prefab;
   public Sprite sprite;
   public string objectName;
}
