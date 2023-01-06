using Game;
using GameFrameWork;
using GameFrameWork.UI;
using Scr.Game.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Scr.Game.UI
{
    public class MainPanel : UIControllerBase
    {
        [SerializeField] private Button m_serverBtn;
        [SerializeField] private Button m_clientBtn;
        [SerializeField] private InputField m_ipField;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_serverBtn.onClick.AddListener(() =>
            {
                FrameWork.GetGame<HangGame>().CreateServer();
            });
            
            m_clientBtn.onClick.AddListener(() =>
            {
                FrameWork.GetGame<HangGame>().CreateClient(m_ipField.text,GameNetworkDefine.GamePort);
            });
        }

        public override void ShowPanel(params object[] args)
        {
            base.ShowPanel(args);
            
        }
    }
}