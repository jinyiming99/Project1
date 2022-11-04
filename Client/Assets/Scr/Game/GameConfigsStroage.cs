using GameFrameWork;
using GameFrameWork.Network;

namespace Scr.Game
{
    public class GameConfigsStroage :IFrameWorkConfigsStroage
    {

        public override FrameWorkConfig GetFrameWorkConfig()
        {
            return new FrameWorkConfig();
        }
    }
}