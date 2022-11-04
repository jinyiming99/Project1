using System;
using Game.Characters;
using Game.Input;
using GameFrameWork;
using GameFrameWork.Network;
using UnityEngine;

namespace GameFrameWork
{
    public abstract class Game : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Release();
    }
    public abstract class Game<FSM> : Game where FSM:IFSMMachine ,new()
    {
        protected FSM m_fsm;
        public override void Init()
        {
            m_fsm = new FSM();
        }

        public override void Release()
        {
            
        }

        private void Update()
        {
            m_fsm?.Update();
        }

        private void OnDestroy()
        {
            Release();
        }


    }
}