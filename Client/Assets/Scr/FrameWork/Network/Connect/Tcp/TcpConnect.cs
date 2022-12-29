using System;
using System.Net;
using System.Net.Sockets;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public enum ConnectErrorStatus
    {
        Send_Error,
        Reveice_Error,
    }
    public delegate void ConnectErrorCallBack(ConnectErrorStatus status,SocketError error);
    public delegate void ReveiceDataCallBack(byte[] datas,int length);
    
    public class TcpConnect : IConnect
    {
        private Socket m_socket;

        private string m_ip;

        public string IP
        {
            set => m_ip = value;
            get => m_ip;
        }

        private int m_port;

        public int Port
        {
            get => m_port;
            set => m_port = value;
        }

        private bool m_isConnected;

        private ConnectErrorCallBack m_failedCallback;
        private ReveiceDataCallBack m_reveiceCallback;

        public ConnectErrorCallBack FailedCallBack
        {
            set => m_failedCallback = value;
        }

        public ReveiceDataCallBack ReveiceCallback
        {
            set => m_reveiceCallback = value;
        }
        
        private byte[] m_receiveData = new byte[1024 * 1024];

        public static TcpConnect CreateConnect(Socket socket)
        {
            TcpConnect connect = new TcpConnect();
            connect.m_socket = socket;
            if (socket.Connected)
                connect.m_isConnected = true;
            return connect;
        }
        private TcpConnect()
        {
            
        }

        public void ConnectAsync(string ip,int port,Action<bool> action)
        {
            m_ip = ip;
            m_port = port;
            try
            {
                m_socket.BeginConnect(IP, Port, (result)=>
                {
                    m_socket.EndConnect(result);
                    if (result.IsCompleted)
                        action?.Invoke(m_socket.Connected);
                    else
                        action?.Invoke(false);
                }, null);
            }
            catch (Exception e)
            {
                action?.Invoke(false);
            }
            
        }

        public void BindReveiceCallback(ReveiceDataCallBack back)
        {
            m_reveiceCallback = back;
        }
        

        public long GetHashCode()
        {
            if (m_socket != null)
                return m_socket.GetHashCode();
            return 0L;
        }

        public string GetMac()
        {
            if (m_socket != null)
            {
                var endpoint = (IPEndPoint) m_socket.RemoteEndPoint;
                return endpoint.Address.ToString();
            }

            return string.Empty;
        }

        public void Dispose()
        {
            if (m_socket != null)
            {
                m_socket.Shutdown(SocketShutdown.Both);
                m_socket.Close();
                m_socket = null;
            }

            m_isConnected = false;
        }
        
        public void SendAsync(byte[] data,int length,int offer)
        {
            if (m_socket != null)
            {
                int sendLength = 0;
                m_socket.BeginSend(data, offer, length, SocketFlags.None,out var sendError,  (iar) =>
                {
                    if (m_isConnected)
                    {
                        FrameWork.GetFrameWork()?.Components?.ThreadWorker?.Post(() =>
                        {
                            int size = m_socket.EndSend(iar, out var errorCode);
                            if (size == 0 || errorCode != SocketError.Success)
                            {
                                Dispose();
                                return;
                            }

                            if (size < length)
                            {
                                SendAsync(data, length, offer);
                            }
                        });
                        
                    }
                },null);
                if (sendError != SocketError.Success)
                {
                    Dispose();
                }
            }
        }

        public void BeginReceive()
        {
            ReceiveAsync();
        }

        public void ReceiveAsync()
        {
            if (m_socket != null)
            {
                m_socket.BeginReceive(m_receiveData, 0, m_receiveData.Length, SocketFlags.None, out var receiveErrorCode,(iar) =>
                {
                    ///这里是异步操作
                    if (!m_isConnected) return;
                    //进入工作线程
                    FrameWork.GetFrameWork()?.Components?.ThreadWorker?.Post(() =>
                    {
                        int size = m_socket.EndReceive(iar, out var errorCode);
                        if (size == 0 || errorCode != SocketError.Success)
                        {
                            Dispose();
                            m_failedCallback?.Invoke(ConnectErrorStatus.Reveice_Error,errorCode);
                        }
                        else ///读取成功,丢回给上级
                        {
                        
                            m_reveiceCallback?.Invoke(m_receiveData,size);
                            ReceiveAsync();
                        }
                    });
                    
                },null);
                
                if (receiveErrorCode != SocketError.Success)
                {
                    m_failedCallback?.Invoke(ConnectErrorStatus.Reveice_Error,receiveErrorCode);
                    Dispose();
                }
            }
        }

        public int ReceiveSteamSize()
        {
            if (m_socket != null)
            {
                return m_socket.Available;
            }

            return 0;
        }

        public bool IsConnected => m_isConnected;
    }
}