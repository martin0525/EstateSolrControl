using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateSolrControl
{
    class Program
    {
        static void Main(string[] args)
        {

            //删除全部索引
            //http://172.28.70.71:8080/solr/update/?stream.body=<delete><query>*:*</query></delete>&stream.contentType=text/xml;charset=utf-8&commit=true

            #region Try to add all data to Index
            try
            {
                string sql = @"SELECT [MainID]
                ,[SourceID]
                ,[City]
                ,[Region]
                ,[Circle]
                ,[Name]
                ,[Address]
                ,[X]
                ,[Y]
                ,[PropertyType]
                ,[Developer]
                ,[PropertyCompany]
                ,[PlotRatio]
                ,[GreeningRatio]
                ,[BuildDate]
                ,[ConfidenceLevel]
                ,[MultiName]
                ,[MultiAddress]
                ,[MultiNamePY]
                ,[MultiAddressPY]
                ,[CreateDate]
            FROM [dbo].[TB_ESTATE_BASEINFO_FULLINDEX] where state =1";

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
