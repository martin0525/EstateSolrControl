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
            //http://10.0.81.5:8080/solr/update/?stream.body=<delete><query>*:*</query></delete>&stream.contentType=text/xml;charset=utf-8&commit=true

            #region Try to add all data to Index
            //try
            //{
            //    string sql = @"SELECT [MainID]
            //    ,[SourceID]
            //    ,[City]
            //    ,[Region]
            //    ,[Circle]
            //    ,[Name] 
            //    ,[Address]
            //    ,[X]
            //    ,[Y]
            //    ,[PropertyType]
            //    ,[Developer]
            //    ,[PropertyCompany]
            //    ,[PlotRatio]
            //    ,[GreeningRatio]
            //    ,[BuildDate]
            //    ,[ConfidenceLevel]
            //    ,[MultiName]
            //    ,[MultiAddress]
            //    ,[FullIndex]
            //    ,[CreateTime] 
            //    ,[SourceFrom]
            //FROM [dbo].[TB_ESTATE_BASEINFO_FULLINDEX_V2] where state =1";

            //    Console.WriteLine("读取数据源。。。");

            //    System.Data.DataSet result = SimpleDataHelper.Query(SimpleDataHelper.CoreCenterConnectionString, sql);

            //    object data = result.Tables[0];

            //    IndexTools it = new IndexTools();

            //    Console.WriteLine("导入中。。。");

            //    it.InitIndex(data);

            //    Console.WriteLine("完成");

            //}
            //catch (Exception EE)
            //{
            //    Console.WriteLine(EE.Message);
            //}

            #endregion

            

            //Add new record by indexid
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
                ,[FullIndex]
                ,[CreateTime] 
                ,[SourceFrom]
            FROM [dbo].[TB_ESTATE_BASEINFO_FULLINDEX_V2] where state =1 and CreateTime >= cast((getdate()-1) as date)";

                Console.WriteLine("读取数据源。。。");

                System.Data.DataSet result = SimpleDataHelper.Query(SimpleDataHelper.CoreCenterConnectionString, sql);

                IndexTools it = new IndexTools();

                //Delete a record by indexid

                Console.WriteLine("删除有更新的数据。。。");
                string[] indexid = new string[result.Tables[0].Rows.Count];

                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    indexid[i] = result.Tables[0].Rows[i][0].ToString();
                }

                it.Delete(indexid);

                Console.WriteLine("完成删除。。。");


                object data = result.Tables[0];
                Console.WriteLine("导入中。。。");

                it.AtomAdd(data);

                Console.WriteLine("完成");

            }
            catch (Exception EE)
            {
                Console.WriteLine(EE.Message);
            }



        }
    }
}
