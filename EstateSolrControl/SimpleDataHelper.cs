using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EstateSolrControl
{
    public class SimpleDataHelper
    {
        /// <summary>
        /// 连接字符串模板
        /// </summary>
        internal static string connectionStringCC = ConfigurationManager.ConnectionStrings["DB_DC_ESTATE_FULLINDEX"].ConnectionString;

        /// <summary>
        /// 查询楼盘信息接口用
        /// </summary>
        /// 
        public static readonly string CoreCenterConnectionString = string.Format(connectionStringCC, "DB_DC_ESTATE_FULLINDEX");


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

        public static bool UpdateRecordTime(string ConnString, DateTime LastTime)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                conn.ConnectionString = ConnString;
                conn.Open();
                int ResultCount = 0;
                using (System.Data.SqlClient.SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "Update [dbo].[TB_ESTATE_BASE_FULLIND_UPDATE_INFO] set [LastUpdateTime] = '"
                        + LastTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";

                    ResultCount = command.ExecuteNonQuery();

                }
                conn.Close();

                if (ResultCount < 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
