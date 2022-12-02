using UnityEngine;
using GameFrameWork;
using GameFrameWork.SingleInstance;

namespace Game.Input
{
    public class PCInput : MonoSingleInstanceDontDestroy<KeyInputPC>
    {
    }

    public class KeyInputPC :InputComponent<ControlEventEnum>
    {
        //private InputComponent<ControlEventEnum> component = new InputComponent<ControlEventEnum>();

        

        public void Save()
        {
            
        }

        public void Load()
        {
            
        }

        public override void Init()
        {
            Add( KeyCode.A, ControlEventEnum.OnMoveLeft);
            Add( KeyCode.D, ControlEventEnum.OnMoveRight);
            Add( KeyCode.S, ControlEventEnum.OnMoveBack);
            Add( KeyCode.W, ControlEventEnum.OnMoveFront);
        }

        public override void CallBack(ControlEventEnum e)
        {
            FrameWork.GetFrameWork().EventController.OnEvent<ControlEventEnum>(GameEventEnum
                .OnInputEvent,e);
        }
     
    }
}