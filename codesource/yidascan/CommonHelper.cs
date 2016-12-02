using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace commonhelper {
    public static class CommonHelper {
        /// <summary>
        /// 显示用户确认窗口。
        /// </summary>
        /// <param name="question">窗口提示正文。</param>
        /// <returns>用户按下Yes按钮 ，则返回true, 否则返回false。</returns>
        public static bool Confirm(string question) {
            var r = MessageBox.Show(question, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return r == DialogResult.Yes;
        }

        /// <summary>
        /// 关闭指定名字的进程。但不会关闭本进程。        
        /// </summary>
        /// <param name="name">进程名，注意，名字不含扩展名，不含路径。</param>
        public static void KillInstance(string name) {
            foreach (var process in Process.GetProcessesByName(name)) {
                if (process.Id != Process.GetCurrentProcess().Id) {
                    process.Kill();
                }
            }
        }
    }
}

