using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterState_GrabItem : CharacterState
    {
        public CharacterState_GrabItem(Character character, CharacterFSM characterFSM) : base(character, characterFSM) { }

        public override void Enter()
        {
            character.animator.SetLayerWeight(character.animator.GetLayerIndex("Hold"), 1f);
            if (character.interactionFocus.interactionType == InteractionType.DEPOSIT)
            {
                GameObject item = character.interactionFocus.GetComponent<Deposit>().GetItem();
                character.holdingItem = item.GetComponent<Item>();
            }
            else
            {
                character.holdingItem = character.interactionFocus.GetComponent<Item>();
            }
            character.holdingItem.transform.parent = character.itemHolder;
            character.holdingItem.transform.position = character.itemHolder.position;
            character.holdingItem.DisableSimulation();

            characterFSM.SetState(character.movementState);

        }
    }
}
