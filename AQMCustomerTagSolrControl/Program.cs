using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AQMCustomerTagSolrControl
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Try to add all data to Index
            try
            {

                string sqlcount = "select count(1) as cc from customer_tag_buycapacity";

                System.Data.DataSet count = SimpleDataHelper.Query(SimpleDataHelper.CoreCenterConnectionString, sqlcount);

                int rowcount = Convert.ToInt32(count.Tables[0].Rows[0][0]);

                for (int i = 5; i < rowcount/100000+1; i++)
                {
                    string sql = string.Format( @"SELECT  [customer_id]      
      ,[mobile]
      ,[buycount]
      ,[bcconfidence]
      ,[buypurpose]
      ,[bpconfidence]
      ,[buymoney]
      ,[bmconfidence]
            FROM [dbo].[customer_tag_buycapacity] where id >= 100000*{0} and id < 100000*({0}+1)",i);

                    Console.WriteLine("读取数据源。。。");

                    System.Data.DataSet result = SimpleDataHelper.Query(SimpleDataHelper.CoreCenterConnectionString, sql);

                    object data = result.Tables[0];

                    IndexTools it = new IndexTools();

                    Console.WriteLine("导入中。。。");

                    it.InitIndex(data);

                    //http://172.168.63.233:8983/solr/admin/cores?action=RELOAD&core=collection2 

                    WebClient client = new WebClient();
                    var urladdress = "http://172.28.70.71:8080/solr/admin/cores?action=RELOAD&core=core1";
                    client.Encoding = Encoding.UTF8;
                    string content = client.DownloadString(urladdress);
                    

                    Console.WriteLine("完成");
                }

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
