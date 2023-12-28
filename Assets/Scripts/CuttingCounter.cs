using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter
{
   [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         //then is no kitchen object
         if (player.HasKitchenObject())
         {
            //player is carrying something
            if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) 
            {
               //player is carrying something that can be cut
               player.GetKitchenObject().SetKitchenObjectParent(this);
            }
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
      if (HasKitchenObject()&& HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
      {
         // There is a kitchen object and it can be cut
         KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
         GetKitchenObject().DestroySelf();
         KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
      }
   }
   
   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
   {
      foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSoArray)
      {
         if (cuttingRecipeSo.input == inputKitchenObjectSo)
         {
            return true;
         }
      }

      return false;
   }

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
   {
      foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSoArray)
      {
         if (cuttingRecipeSo.input == inputKitchenObjectSo)
         {
            return cuttingRecipeSo.output;
         }
      }

      return null;
   }
}

