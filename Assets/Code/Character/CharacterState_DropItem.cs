using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterState_DropItem : CharacterState
    {
        public CharacterState_DropItem(Character character, CharacterFSM characterFSM) : base(character, characterFSM) { }

        public override void Enter()
        {
            character.animator.SetLayerWeight(character.animator.GetLayerIndex("Hold"), 0f);
            character.holdingItem.EnableSimulation();
            character.holdingItem = null;

            characterFSM.SetState(character.movementState);
        }
    }

}
