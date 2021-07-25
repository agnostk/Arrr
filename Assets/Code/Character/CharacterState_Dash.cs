using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Character
{
    public class CharacterState_Dash : CharacterState
    {
        public CharacterState_Dash(Character character, CharacterFSM characterFSM) : base(character, characterFSM) { }

        public override void Enter()
        {
            GameObject.Instantiate(character.dashParticlesPrefab, character.transform.position, character.transform.rotation);

            character.rb.freezeRotation = true;
            DOTween.To(() => character.rb.velocity, v => character.rb.velocity = v, character.transform.forward * (character.dashSpeed / 2f), character.dashTime / 2f)
                .SetEase(Ease.OutExpo)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => characterFSM.SetState(character.movementState));
        }

        public override void Exit()
        {
            character.rb.freezeRotation = false;
        }
    }
}
