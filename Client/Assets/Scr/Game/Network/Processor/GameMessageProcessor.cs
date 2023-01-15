using GameFrameWork.Network.MessageBase;
using Common.Proto;
namespace Game.Network.Processor
{
    public class GameMessageProcessor : MessageProcessor<JoinGameRes>
    {
        public override int MessageID { get => (int)MessageEnum.JoinGameRes; }

        public override void Processor()
        {
            
        }
    }
}