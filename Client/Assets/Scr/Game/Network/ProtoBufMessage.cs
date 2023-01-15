using System;
using System.Reflection;
using Google.Protobuf;

namespace GameFrameWork.Network.MessageBase
{
    public class ProtoBufMessageDistribute : MessageDistribute
    {
        private string spaceName = "Game.Network.Processor";

        public ProtoBufMessageDistribute(string space)
        {
            spaceName = space;
        }
        public override void CreateMessageDic()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            System.Type[] types = asm.GetTypes();
            foreach (System.Type type in types)
            {
                if (type.Namespace == spaceName && !type.IsAbstract)
                {
                    var t = Activator.CreateInstance(type) as IMessageProcessor;
                    m_createrDic.Add(t.MessageID,t); 
                }
            }
        }
    }
}