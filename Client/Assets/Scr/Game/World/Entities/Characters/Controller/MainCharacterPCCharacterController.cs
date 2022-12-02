using Game.Input;
using GameFrameWork;

namespace Game.Characters
{
    public class MainCharacterPCCharacterController :ICharacterController<MainCharacter>
    {
        private MainCharacter _mainCharacter;
        

        public void Init()
        {
            FrameWork.GetFrameWork().EventController.AddEvent<ControlEventEnum>(GameEventEnum.OnInputEvent, (code) =>
            {
                SwitchInputControl(code);
            });
        }

        private void SwitchInputControl(ControlEventEnum @enum)
        {
            if (_mainCharacter == null) return;
            switch (@enum)
            {
                case ControlEventEnum.OnMoveLeft:
                    _mainCharacter.MoveLeft();
                    break;
                case ControlEventEnum.OnMoveRight:
                    _mainCharacter.MoveRight();
                    break;
                case ControlEventEnum.OnMoveFront:
                    _mainCharacter.MoveForward();
                    break;
                case ControlEventEnum.OnMoveBack:
                    _mainCharacter.MoveBack();
                    break;
                case ControlEventEnum.OnFire:
                    break;
                case ControlEventEnum.OnSkill1:
                    break;
                case ControlEventEnum.OnSkill2: 
                    break;
                case ControlEventEnum.OnSkill3:
                    break;  
            }
        }

        public void BindObject(MainCharacter t)
        {
            _mainCharacter = t;
        }

        public void Release()
        {
            _mainCharacter = null;
        }
    }
}