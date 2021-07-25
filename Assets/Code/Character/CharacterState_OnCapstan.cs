using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterState_OnCapstan : CharacterState
    {
        private Capstan capstan;
        public CharacterState_OnCapstan(Character character, CharacterFSM characterFSM) : base(character, characterFSM) { }
        public override void Enter()
        {
            capstan = character.interactionFocus as Capstan;
            character.animator.SetLayerWeight(character.animator.GetLayerIndex("Hold"), 1f);
            Transform closestHandle = capstan.GetClosestHandle(character.transform.position);
            character.rb.isKinematic = true;
            character.transform.parent = closestHandle.parent;
            character.transform.position = closestHandle.position;
            character.transform.rotation = closestHandle.rotation;
        }

        public override void Update()
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            if (hAxis != 0f || vAxis != 0f)
            {
                capstan.StartInteraction();
            }
            else
            {
                capstan.StopInteraction();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                characterFSM.SetState(character.movementState);
            }
        }

        public override void Exit()
        {
            character.animator.SetLayerWeight(character.animator.GetLayerIndex("Hold"), 0f);
            capstan.StopInteraction();
            character.rb.isKinematic = false;
            character.transform.parent = null;
        }
    }
}
