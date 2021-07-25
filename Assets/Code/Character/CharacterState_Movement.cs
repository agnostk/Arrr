using UnityEngine;

namespace Character
{
    public class CharacterState_Movement : CharacterState
    {
        private Transform camera;
        private Vector3 velocity;
        private Vector3 lastVelocity;
        float hAxis, vAxis;
        private bool isHoldingItem;

        public CharacterState_Movement(Character character, CharacterFSM characterFSM) : base(character, characterFSM) { }

        public override void Enter()
        {
            camera = Camera.main.transform;
            lastVelocity = character.transform.forward;

            isHoldingItem = character.holdingItem != null;

            if (isHoldingItem)
            {
                character.animator.SetLayerWeight(character.animator.GetLayerIndex("Hold"), 1f);
            }
        }

        public override void Update()
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            velocity = Vector3.ProjectOnPlane(camera.up * vAxis + camera.right * hAxis, character.GetShipNormal()).normalized;

            if (velocity.magnitude >= 1f)
            {
                character.animator.SetBool("isWalking", true);
                lastVelocity = velocity;
            }
            else
            {
                character.animator.SetBool("isWalking", false);
            }

            velocity *= character.moveSpeed;
            velocity.y = character.rb.velocity.y;

            if (Input.GetButtonDown("Dash"))
            {
                characterFSM.SetState(character.dashState);
            }


            if (Input.GetButtonDown("GrabOrDrop"))
            {
                if (isHoldingItem)
                {
                    characterFSM.SetState(character.dropItemState);
                }
                else if (character.interactionFocus != null)
                {
                    switch (character.interactionFocus.interactionType)
                    {
                        case InteractionType.CAPSTAN:
                            characterFSM.SetState(character.onCapstanState);
                            break;
                        case InteractionType.DEPOSIT:
                            characterFSM.SetState(character.grabItemState);
                            break;
                        case InteractionType.ITEM:
                            characterFSM.SetState(character.grabItemState);
                            break;
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            character.rb.rotation = Quaternion.LookRotation(lastVelocity, character.GetShipNormal());
            character.rb.velocity = velocity;
        }
    }
}