using GameFrameWork;

namespace Game.GameStatus
{
    public class GameFSM : FSMMachine<GameData, GameFrameWork.IState<GameData>, GameStatusEnum>
    {
        public GameFSM()
        {
            Create();
        }

        private void Create()
        {
            m_dic.Add(GameStatusEnum.Game_Init, new GameInitStatus());
            m_dic.Add(GameStatusEnum.Game_MainPanel, new GameMainPanelStatus());
            ChangeState(GameStatusEnum.Game_Init);
        }

        public void Release()
        {
            m_curState.Release(m_data);
        }
    }
}