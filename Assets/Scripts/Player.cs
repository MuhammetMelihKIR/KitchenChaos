using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private GameInput gameInput;
   private bool isWalking;
   private void Update()
   {
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();
      Vector3 moveDir = new Vector3(inputVector.x,0f,inputVector.y);

      float moveDistance = moveSpeed* Time.deltaTime;
      float playerRadius = 0.7f;
      float playerHeight = 2f;
      bool canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up* playerHeight, playerRadius, moveDir,moveDistance);

      if (!canMove) {
         // cannot move towards the moveDir
         // Attempt only x movement
         
         Vector3 moveDirX = new Vector3(moveDir.x,0f,0f);
         canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up* playerHeight, playerRadius, moveDirX,moveDistance);

         if (canMove) {
            // can move only x
            moveDir = moveDirX;
         }
         else {
            // cannot move only x
            
            // Attempt only z movement
            Vector3 moveDirZ = new Vector3(0F,0f,moveDir.z);
            canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up* playerHeight, playerRadius, moveDirZ,moveDistance);
            
            if (canMove) {
               // can move only z
               moveDir= moveDirZ;
            }
            else
            {
               // cannot move 
               
            }
         }
      }
      if (canMove) {
         transform.position += moveDir * moveDistance;
      }
      
      isWalking = moveDir != Vector3.zero;

      float rotateSpeed = 10f;
      transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime * rotateSpeed);

   }
   
   public bool IsWalking()
   {
      return isWalking;
   }  
}
