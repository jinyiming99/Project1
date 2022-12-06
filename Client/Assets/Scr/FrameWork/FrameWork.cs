using Game;

namespace GameFrameWork
{
    public class FrameWork
    {
        private static FrameWork s_frameWork;
        
        public static FrameWork CreateFrameWork<T>(FrameWorkConfig config) where T :Game ,new ()
        {
            if (s_frameWork == null)
            {
                s_frameWork = new FrameWork(config);
                s_frameWork._game = new T();
            }
            return s_frameWork;
        }

        public static FrameWork GetFrameWork()
        {
            return s_frameWork;
        }

        internal FrameWork(FrameWorkConfig config)
        {
            _config = config;
        }


        private FrameWorkConfig _config;
        public FrameWorkConfig Config => _config;
        /// <summary>
        /// 游戏对象
        /// </summary>
        private Game _game;

        public Game CurGame => _game;
        /// <summary>
        /// 框架组件
        /// </summary>
        private FrameWorkComponents _components;
        public FrameWorkComponents Components => _components;

        public void Init()
        {
            
            _components = new FrameWorkComponents();
            _components.Init();
            _components.LifecycleComponent.StartAction += Start;
            _components.LifecycleComponent.UpdateAction += Update;
            _components.LifecycleComponent.DestroyAction += Release;
            _components.LifecycleComponent.GamePauseAction += Pause;
            _components.LifecycleComponent.GameResumeAction += Resume;
            
            _game.SetComponents(_components);
            _game.Init();
        }

        public void Start()
        {
            _game.Start();
        }

        public void Update()
        {
            _game.Update();
        }
        
        public void Resume()
        {
            _game.Resume();
        }
        
        public void Pause()
        {
            _game.Pause();
        }

        public void Release()
        {
            _game.Release();
            _components.Release();
            _components = null;
        }
    }
}