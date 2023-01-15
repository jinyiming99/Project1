using GameFrameWork;

namespace Game.GameStatus
{
    public class GameFSM : FSMMachine<GameFrameWork.IState, GameStatusEnum>
    {
        public GameFSM()
        {
            Create();
        }

        private void Create()
        {
            m_dic.Add(GameStatusEnum.Game_Init, new GameInitStatus());
            m_dic.Add(GameStatusEnum.Game_MainPanel, new GameMainPanelStatus());
            m_dic.Add(GameStatusEnum.Game_GamePlay, new GamePlayStatus());
        }

        public void Release()
        {
            m_curState.Release();
        }
    }
}