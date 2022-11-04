using Scr.Game.Characters;

namespace Game.Characters
{
    public class CharacterManager
    {
        private MainCharacter _mainCharacter;
        public CharacterManager()
        {
            
        }

        public void Init()
        {
            
        }

        public void CreateMainCharacter()
        {
            _mainCharacter = GameObjectUnit.CreateNewGameObject<MainCharacter>();
            _mainCharacter.Init();
        }

        public MainCharacter GetMainCharacter()
        {
            return _mainCharacter;
        }
    }
}