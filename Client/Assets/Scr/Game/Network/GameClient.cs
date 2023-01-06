﻿using System.Net.Sockets;
using GameFrameWork;
using GameFrameWork.Network;
using GameFrameWork.Network.MessageBase;
using Google.Protobuf;

namespace Scr.Game.Network
{
    public class GameClient
    {
        private static ProtoBufMessageDistribute s_messageDistribute;
        public static GameClient CreateClient(string ip, int port)
        {
            if (s_messageDistribute == null)
            {
                s_messageDistribute = new ProtoBufMessageDistribute("");
                s_messageDistribute.CreateMessageDic();
            }
            NetworkWorker worker = FrameWork.GetFrameWork().Components.Network.CreateTcpConnect(ip, port);
            GameClient gameClient = new GameClient(worker)
            {
                _messageDistribute = s_messageDistribute,
            };
            return gameClient;
        }

        private NetworkWorker m_worker;
        private ProtoBufMessageDistribute _messageDistribute;
        internal GameClient(NetworkWorker worker)
        {
            m_worker = worker;
            m_worker.OnReceiveMessageCallback = ReveiceMessage;
            m_worker.OnDisconnectCallback = DisconnectCallback;
        }

        public void SendAsync(int @enum,Google.Protobuf.IMessage message)
        {
            var size = message.CalculateSize();
            var array = message.ToByteArray();
            MessageBase @base = new MessageBase();
            @base.m_cmd = (int)MessageCommand.ProtoBuf_Message;
            @base.m_messageID = (short)@enum;
            @base.m_data = array;
            @base.m_length = size;
            if (m_worker != null)
            {
                m_worker.SendAsync(@base.GetData());
            }
        }

        public void ReveiceMessage(MessageBase messageBase)
        {
            if (messageBase.m_cmd == (int) MessageCommand.ProtoBuf_Message)
            {
                _messageDistribute.Find(messageBase.m_messageID, out var creater);
                creater.CreateMessage(messageBase);
                creater.Processor();
                creater.ReleaseMessage();
            }
        }

        public void DisconnectCallback(SocketError error)
        {
            
        }
    }
}