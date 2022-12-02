using System;
using Game.Characters;
using Game.Input;
using GameFrameWork;
using GameFrameWork.Network;
using UnityEngine;

namespace GameFrameWork
{
    public abstract class Game
    {
        FrameWorkComponents _components;
        public FrameWorkComponents Components => _components;
        
        internal void SetComponents(FrameWorkComponents components)
        {
            _components = components;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();
        public abstract void Start();
        public abstract void Update();

        public abstract void Pause();

        public abstract void Resume();
        public abstract void Release();
    }
}