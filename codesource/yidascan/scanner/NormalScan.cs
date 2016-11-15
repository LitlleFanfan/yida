using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProduceComm.Scanner
{
    public class NormalScan
    {
        public delegate void DataArrivedEventHandler(string type, string data);

        public delegate void ErrEventHandler(Exception ex);

        public delegate void HostErrEventHandler(Exception ex);

        private bool stoped;

        private long long_0;

        private long long_1;

        public List<string> mlsAckData;

        private ICommunication icom;

        public event DataArrivedEventHandler OnDataArrived;

        public event ErrEventHandler OnError;

        public event HostErrEventHandler OnHostErr;

        public long ScanCounter
        {
            get
            {
                return this.long_0;
            }
            set
            {
                this.long_0 = value;
            }
        }

        public long TestScanCounter
        {
            get
            {
                return this.long_1;
            }
            set
            {
                this.long_1 = value;
            }
        }

        public NormalScan(ICommunication _icom)
        {
            icom = _icom;
            this.long_0 = 0L;
            this.mlsAckData = new List<string>();
        }

        public bool Open()
        {
            bool re = false;
            try
            {
                re = icom.Open();
            }
            catch (Exception ex)
            {
                OnError(new Exception("创建连接失败！", ex));
                re = false;
            }
            return re;
        }

        public void Close()
        {
            try
            {
                Thread.Sleep(1);
                icom.Close();
            }
            catch (Exception ex)
            {
                OnError(new Exception("关闭连接失败！", ex));
            }
        }

        public void _StartJob()
        {
            this.long_0 = 0L;
            this.stoped = false;
            Task.Factory.StartNew(() =>
            {
                while (!this.stoped)
                {
                    string data = Encoding.Default.GetString(GetReply());
                    if (!string.IsNullOrEmpty(data))
                    {
                        this.long_0 += 1L;
                        OnDataArrived("", data);
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        public void _StopJob()
        {
            this.stoped = true;
        }

        public void TriggerScan()
        {
            lock (this)
            {
                this.long_1 += 1L;
                DateTime now = DateTime.Now;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(now.Year);
                stringBuilder.Append(now.Month.ToString().PadLeft(2, '0'));
                stringBuilder.Append(now.Day.ToString().PadLeft(2, '0'));
                stringBuilder.Append(now.Minute.ToString().PadLeft(2, '0'));
                stringBuilder.Append(now.Second.ToString().PadLeft(2, '0'));
                stringBuilder.Append(now.Millisecond.ToString());
                stringBuilder.Append(this.long_1.ToString());
                OnDataArrived("", stringBuilder.ToString());
            }
        }

        private byte[] GetReply()
        {
            try
            {
                return icom.Read(1024);
            }
            catch
            {
                return new byte[] { };
            }
        }
    }
}
