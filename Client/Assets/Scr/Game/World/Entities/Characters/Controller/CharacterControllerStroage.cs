using System.Collections.Generic;

namespace Game.Characters
{
    public class CharacterControllerStroage
    {
        //private Dictionary<long, IController> _controllers = new Dictionary<long, IController>(16);

        private static ICharacterController<MainCharacter> _sMainCharacterCharacterController = null;

        public static ICharacterController<MainCharacter> SMainCharacterCharacterController
        {
            get
            {
                if (_sMainCharacterCharacterController == null)
                {
                    _sMainCharacterCharacterController = CreateMainCharacterController();
                }
                return _sMainCharacterCharacterController;
            }
        }

        private static ICharacterController<MainCharacter> CreateMainCharacterController()
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            var c = new MainCharacterPCCharacterController();
            c.Init();
            return c;
            #elif UNITY_IOS
            return null;
            #endif
        }

        public static void Release()
        {
            if (_sMainCharacterCharacterController != null)
            {
                _sMainCharacterCharacterController.Release();
            }
        }
        
        
    }
}