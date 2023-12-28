using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : BaseCounter
{
   [FormerlySerializedAs("kitchenObjectsSo")] [FormerlySerializedAs("kitchenObjectsSO")] [SerializeField] private KitchenObjectSO kitchenObjectSo;
   
   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         //then is no kitchen object
         if (player.HasKitchenObject())
         {
            //player is carrying something
            player.GetKitchenObject().SetKitchenObjectParent(this);
         }
      }
      else
      {
         //player has carriyng anything
         if (player.HasKitchenObject())
         {
            //player
         }
         else
         {
            //player
            GetKitchenObject().SetKitchenObjectParent(player);
         }
      }
      
   }
   
}
