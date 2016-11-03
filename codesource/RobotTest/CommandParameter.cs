using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace yidascan.DataAccess
{
    /// <summary>
    /// 命令参数类
    /// </summary>
    public class CommandParameter
    {
        string sql;
        /// <summary>
        /// sql语句
        /// </summary>
        public string Sql
        {
            get { return sql; }
            set { sql = value; }
        }

        SqlParameter[] p;
        /// <summary>
        /// 参数集合
        /// </summary>
        public SqlParameter[] P
        {
            get { return p; }
            set { p = value; }
        }
    }
}
