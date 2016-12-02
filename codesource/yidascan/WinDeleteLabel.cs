using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using commonhelper;
using yidascan.DataAccess;

namespace yidascan {
    public partial class WinDeleteLabel : Form {
        public WinDeleteLabel() {
            InitializeComponent();
        }

        public FrmMain mainwin = null;

        private void button1_Click(object sender, EventArgs e) {
            DeleteLabelByHand(txtLabelCode);
        }

        /// <summary>
        /// 手工删除标签号码。
        /// </summary>
        private void DeleteLabelByHand(TextBox txtDelLCode) {
            if (txtDelLCode.Text.Trim().Length < 12) {
                MessageBox.Show("删除号码长度不正确");
                return;
            }

            string code = txtDelLCode.Text.Trim().Substring(0, 12);

            //提示用户确认。
            var question = string.Format("您确定要删除标签[{0}]吗？", code);
            if (!CommonHelper.Confirm(question)) { return; }

            // 删除号码。
            if (LableCode.Delete(code)) {
                // 更新主界面胡显示。
                mainwin.RemoveRowFromView(code);
                mainwin.RefreshCounter();               
                mainwin.WriteLabelCodeToOpc(code);
                FrmMain.logOpt.ViewInfo(string.Format("删除标签{0}成功", code));
            } else {
                FrmMain.logOpt.ViewInfo(string.Format("删除标签{0}失败", code));
            }

            txtDelLCode.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
