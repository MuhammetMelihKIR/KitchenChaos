using UnityEngine;

public class CuttingCounter : BaseCounter
{
   [SerializeField] private KitchenObjectsSO cutKitchenObjectsSO;
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

   public override void InteractAlternate(Player player)
   {
      if (HasKitchenObject())
      {
         GetKitchenObject().DestroySelf();
         KitchenObject.SpawnKitchenObject(cutKitchenObjectsSO, this);
      }
   }
}

