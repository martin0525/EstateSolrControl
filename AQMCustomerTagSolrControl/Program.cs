using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQMCustomerTagSolrControl
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Try to add all data to Index
            try
            {
                string sql = @"SELECT top 10000 [customer_id]      
      ,[mobile]
      ,[buycount]
      ,[bcconfidence]
      ,[buypurpose]
      ,[bpconfidence]
      ,[buymoney]
      ,[bmconfidence]
            FROM [dbo].[customer_tag_buycapacity] ";

                Console.WriteLine("读取数据源。。。");

                System.Data.DataSet result = SimpleDataHelper.Query(SimpleDataHelper.CoreCenterConnectionString, sql);

                object data = result.Tables[0];

                IndexTools it = new IndexTools();

                Console.WriteLine("导入中。。。");

                it.InitIndex(data);

                Console.WriteLine("完成");

            }
            catch (Exception EE)
            {
                Console.WriteLine(EE.Message);
            }

            #endregion

            //Delete a record by indexid

            //IndexTools its = new IndexTools();
            //its.Delete();

        }
    }
}
