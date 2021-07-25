using Patterns;

namespace Character
{
    public class CharacterState : State
    {
        public Character character;
        public CharacterFSM characterFSM;

        public CharacterState(Character character, CharacterFSM characterFSM)
        {
            this.character = character;
            this.characterFSM = characterFSM;
        }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}