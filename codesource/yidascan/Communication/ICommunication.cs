using System;
namespace ProduceComm
{
    public enum CommunicationType
    {
        Network,
        SerialPort
    }

    interface ICommunication
    {
        string GetIPOrCom();
        int GetPortOrBaudRate();
        bool Open(string ip, int port);
        void Write(byte[] sendData);
        byte[] Read(int length, int? timeout = null);
        void Close();
    }
}
