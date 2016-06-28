using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

using EasyNet.Solr;
using EasyNet.Solr.Impl;
using EasyNet.Solr.Commons;

namespace AQMCustomerTagSolrControl
{
    public class IndexCustomerTagModel
    {
        public IndexCustomerTagModel()
        {
        }

        

        public string mobile { get; set; }



        public string buycount { get; set; }

        public float bcconfidence { get; set; }

        public string buypurpose { get; set; }

        public float bpconfidence { get; set; }
        public string buymoney { get; set; }

        public float bmconfidence { get; set; }

        public string rttag { get; set; }
        public string buyprefer { get; set; }
        public string acregion { get; set; }
        public string childtag { get; set; }

        public float ctconfidence { get; set; }
        public string familytag { get; set; }
        public string preferproject { get; set; }

        public string wbtag { get; set; }

    }

    public class IndexTools
    {

        static OptimizeOptions optimizeOptions = new OptimizeOptions();
        static CommitOptions commitOptions = new CommitOptions() { SoftCommit = true };
        static ISolrResponseParser<NamedList, ResponseHeader> binaryResponseHeaderParser = new BinaryResponseHeaderParser();
        static IUpdateParametersConvert<NamedList> updateParametersConvert = new BinaryUpdateParametersConvert();
        static ISolrUpdateConnection<NamedList, NamedList> solrUpdateConnection = new SolrUpdateConnection<NamedList, NamedList>() { ServerUrl = "http://172.28.70.71:8080/solr/" };
        static ISolrUpdateOperations<NamedList> updateOperations = new SolrUpdateOperations<NamedList, NamedList>(solrUpdateConnection, updateParametersConvert) { ResponseWriter = "javabin" };

        static ISolrQueryConnection<NamedList> connection = new SolrQueryConnection<NamedList>() { ServerUrl = "http://172.28.70.71:8080/solr/" };
        static ISolrQueryOperations<NamedList> operations = new SolrQueryOperations<NamedList>(connection) { ResponseWriter = "javabin" };

        /// <summary>
        /// 创建索引
        /// </summary>
        public void InitIndex(object data)
        {
            var docs = new List<SolrInputDocument>();
            DataTable list = data as DataTable;
            foreach (DataRow pro in list.Rows)
            {
                var model = new SolrInputDocument();

                PropertyInfo[] properites = typeof(IndexCustomerTagModel).GetProperties();//得到实体类属性的集合
                //string[] dateFields = { "CreateDate" };
                string field = string.Empty;//存储fieldname
                foreach (PropertyInfo propertyInfo in properites)//遍历数组
                {
                    object val = pro[propertyInfo.Name];
                    if (val != DBNull.Value)
                    {
                        model.Add(propertyInfo.Name, new SolrInputField(propertyInfo.Name, val));
                    }
                }
                docs.Add(model);


            }

            var result = updateOperations.Update("core1", "/update", new UpdateOptions() { Docs = docs ,CommitOptions=commitOptions});
            var header = binaryResponseHeaderParser.Parse(result);

            System.Console.WriteLine(string.Format("Update Status:{0} QTime:{1}", header.Status, header.QTime));
            //System.Console.ReadLine();

        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        public void Delete()
        {
            var result = updateOperations.Update("core1", "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, DelById = new string[] { "89C88185-B541-42CC-9AD0-C7A978AD7680" } });
            var header = binaryResponseHeaderParser.Parse(result);

            System.Console.WriteLine(string.Format("Update Status:{0} QTime:{1}", header.Status, header.QTime));
            System.Console.ReadLine();
        }
    }
}
