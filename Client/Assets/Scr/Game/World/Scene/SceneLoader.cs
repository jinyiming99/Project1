using System;
using GameFrameWork;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Game.World.Scene
{
    public class SceneLoader : ResourceLoader
    {
        private Action<UnityEngine.SceneManagement.Scene> _action;

        public void LoadScene(string name,Action<UnityEngine.SceneManagement.Scene> action)
        {
            _action = (obj) =>
            {
                action(obj);
            };
            _loadHandle = LoadResource(name);
        }
        
        protected override AsyncOperationHandle LoadResource(string name)
        {
            return FrameWork.GetFrameWork().Components.ResourceManager.LoadScene(name, LoadSceneMode.Single, (obj) =>
            {
                if (_isDel)
                    return;
                _action?.Invoke(obj);
            });
        }
    }
}