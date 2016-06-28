using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AQMCustomerTagSolrControl
{
    public class SimpleDataHelper
    {
        /// <summary>
        /// 连接字符串模板
        /// </summary>
        internal static string connectionStringCC = ConfigurationManager.ConnectionStrings["BQM_TAG_OUTPUT"].ConnectionString;

        /// <summary>
        /// 查询楼盘信息接口用
        /// </summary>
        /// 
        public static readonly string CoreCenterConnectionString = string.Format(connectionStringCC, "BQM_TAG_OUTPUT");


        public static System.Data.DataSet Query(string ConnString, string sql)
        {
            System.Data.DataSet result = new System.Data.DataSet();
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                conn.ConnectionString = ConnString;
                conn.Open();
                using (System.Data.SqlClient.SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    using (System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(command))
                    {
                        adp.Fill(result);
                    }
                }
                conn.Close();
            }
            return result;
        }
    }
}
