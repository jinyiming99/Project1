using System;
using Game.Characters;
using Game.Input;
using GameFrameWork;
using GameFrameWork.Network;
using UnityEngine;

namespace GameFrameWork
{
    public abstract class Game<FSM,Data> : IGame
                                            where FSM: IFSMMachine, new()
                                            where Data: new()
    {
        protected FSM _fsm;
        protected Data _data;
        public Data data => _data;
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            _data = new Data();
            _fsm = new FSM();
        }

        public virtual void Start()
        {

        }

        public virtual void Resume()
        {
            
        }

        public virtual void Release()
        {
            _fsm.Release();
        }

        public virtual void Update()
        {
            _fsm.Update();
        }

        public virtual void Pause()
        {
            
        }
        
    }
}