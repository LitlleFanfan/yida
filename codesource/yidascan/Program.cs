using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace yidascan {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            bool created = true;
            var mutex = new Mutex(true, "ABCDEFG_MUTEX_ROBOT_CONTROL_YIDA_X", out created);

            if (!created) {
                MessageBox.Show("已经有一个程序在运行。");                
            } else {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
                mutex.ReleaseMutex();
            }

        }
    }
}
