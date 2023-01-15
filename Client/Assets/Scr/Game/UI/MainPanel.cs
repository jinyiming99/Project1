using Game;
using GameFrameWork;
using GameFrameWork.UI;
using Game.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainPanel : UIBase
    {
        [SerializeField] private Button m_serverBtn;
        [SerializeField] private Button m_clientBtn;
        [SerializeField] private Button m_closeBtn;
        [SerializeField] private InputField m_ipField;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_serverBtn.onClick.AddListener(() =>
            {
                FrameWork.GetGame<HangGame>().gameNetwork.CreateServer();
            });
            
            m_clientBtn.onClick.AddListener(() =>
            {
                FrameWork.GetGame<HangGame>().gameNetwork.CreateClient(m_ipField.text,GameNetworkDefine.GamePort);
            });
            m_closeBtn.onClick.AddListener(()=>
            {
                HangGame.GetFrameWork().Components.UI.CloseUI((int)this._PanelEnum);
            });
        }

        public override void ShowPanel(params object[] args)
        {
            base.ShowPanel(args);
            
        }
    }
}