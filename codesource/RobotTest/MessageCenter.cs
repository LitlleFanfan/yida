﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yidascan {
    public class MessageCenter {
        private readonly Queue<string> que;

        private const string FMT_MESSAGE = "[{0}] [{1}] {2}";
        private const string FMT_DATE = "yyyy-MM-dd HH:mm:ss";

        public MessageCenter() {
            que = new Queue<string>();
        }

        /// <summary>
        /// 消息加入消息中心队列。
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="group">可选消息分组</param>
        public void Push(string message, string group = "normal") {
            if (string.IsNullOrWhiteSpace(message)) { return; }

            var s = string.Format(FMT_MESSAGE,
                                  DateTime.Now.ToString(FMT_DATE),
                                  group,
                                  message);
            lock (que) {
                que.Enqueue(s);
            }
        }

        /// <summary>
        /// 从消息中心取所有消息。
        /// </summary>
        /// <returns>返回List对象，如果没有消息，List的长度为0，</returns>
        public List<string> GetAll() {
            List<string> lst;
            lock (que) {
                lst = que.ToList();
                que.Clear();
            }
            return lst;
        }

        /// <summary>
        /// 清空消息队列。
        /// </summary>
        public void Clear() {
            lock (que) {
                que.Clear();
            } // end of lock.
        }
    }
}
