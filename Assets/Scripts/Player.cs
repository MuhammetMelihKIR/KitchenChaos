using System;
using UnityEngine;
public class Player : MonoBehaviour
{
   public static Player Instance { get; private set; }
   public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
   public class OnSelectedCounterChangedEventArgs : EventArgs
   {
      public ClearCounter selectedCounter;
   }
   
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private GameInput gameInput;
   [SerializeField] private LayerMask countersLayerMask;
   
   private bool _isWalking;
   private Vector3 _lastInteractDir;
   private ClearCounter _selectedCounter;

   private void Awake()
   {
      if (Instance != null)
      {
         Debug.LogError("There is more than one Player Instance in the Scene");
      }
      Instance = this;
   }

   private void Start()
   {
      gameInput.OnInteractAction += GameInputOnOnInteractAction;  
   }
   private void GameInputOnOnInteractAction(object sender, EventArgs e)
   {
      if (_selectedCounter != null) {
         _selectedCounter.Interact();
      }
   }

   private void Update()
   {
     HandleMovement();
     HandleInteractions();
   }
   
   public bool IsWalking()
   {
      return _isWalking;
   }

   private void HandleInteractions()
   {
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();
      Vector3 moveDir = new Vector3(inputVector.x,0f,inputVector.y);

      if (moveDir != Vector3.zero) {
         _lastInteractDir= moveDir;
      }
      
      float interactDistance = 2f;
      if (Physics.Raycast(transform.position, _lastInteractDir,out RaycastHit raycastHit ,interactDistance,countersLayerMask))
      {
         if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
            if (clearCounter != _selectedCounter) {
               SetSelectedCounter(clearCounter);
            }   
         }         else {
            SetSelectedCounter(null);
         }
      }else {
         SetSelectedCounter(null);
      }
   }
   private void HandleMovement()
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
      
      _isWalking = moveDir != Vector3.zero;

      float rotateSpeed = 10f;
      transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime * rotateSpeed);

   }
   
   private void SetSelectedCounter(ClearCounter selectedCounter)
   {
      this._selectedCounter = selectedCounter;
      OnSelectedCounterChanged ?.Invoke(this, new OnSelectedCounterChangedEventArgs
      {
         selectedCounter = _selectedCounter
      });
   }
}
