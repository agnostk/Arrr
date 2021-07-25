using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {

        [Header("Component References")]
        public Rigidbody rb;
        public Animator animator;

        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public GameObject dashParticlesPrefab;
        public float dashSpeed = 25f;
        public float dashTime = 0.30f;
        public LayerMask shipPlaneLayerMask;

        [Header("Action Settings")]
        public LayerMask interactableLayerMask;
        public float interactionDistance = 1f;
        public Interactable interactionFocus;
        public Transform itemHolder;
        public Item holdingItem;


        [Header("Character States")]
        public CharacterState_Dash dashState;
        public CharacterState_Movement movementState;
        public CharacterState_OnCapstan onCapstanState;
        public CharacterState_GrabItem grabItemState;
        public CharacterState_DropItem dropItemState;

        private CharacterFSM characterFSM;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            characterFSM = new CharacterFSM();
            movementState = new CharacterState_Movement(this, characterFSM);
            dashState = new CharacterState_Dash(this, characterFSM);
            onCapstanState = new CharacterState_OnCapstan(this, characterFSM);
            grabItemState = new CharacterState_GrabItem(this, characterFSM);
            dropItemState = new CharacterState_DropItem(this, characterFSM);
            characterFSM.SetState(movementState);
        }

        void Update()
        {
            UpdateInteractionFocus();
            (characterFSM.CurrentState as CharacterState).Update();
        }

        void FixedUpdate()
        {

            (characterFSM.CurrentState as CharacterState).FixedUpdate();
        }

        public Vector3 GetShipNormal()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f, shipPlaneLayerMask))
            {
                return hit.normal;
            }
            else
            {
                return Vector3.up;
            }
        }

        public void UpdateInteractionFocus()
        {
            Vector3 startPos = transform.position + Vector3.down * 0.95f;
            Debug.DrawRay(startPos, transform.forward * interactionDistance, Color.green);
            if (Physics.Raycast(startPos, transform.forward, out RaycastHit hit, interactionDistance, interactableLayerMask))
            {
                interactionFocus = hit.collider.GetComponent<Interactable>();
                interactionFocus = interactionFocus == null ? hit.collider.GetComponentInParent<Interactable>() : interactionFocus;
            }
            else
            {
                interactionFocus = null;
            }
        }
    }
}
