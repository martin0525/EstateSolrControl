using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;

using EasyNet.Solr;
using EasyNet.Solr.Impl;
using EasyNet.Solr.Commons;
using System.Collections;

namespace EstateSolrControl
{



    public class IndexProductEstateModel
    {
        public IndexProductEstateModel()
        {
        }

        #region  Properties

        public string MainID { get; set; }

        public string SourceID { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
        public string Circle { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public float X { get; set; }

        public float Y { get; set; }
        public string PropertyType { get; set; }
        public string Developer { get; set; }


        public string PropertyCompany { get; set; }
        public float PlotRatio { get; set; }

        public float GreeningRatio { get; set; }
        public System.DateTime BuildDate { get; set; }

        public int ConfidenceLevel { get; set; }
        public string MultiName { get; set; }
        public string MultiAddress { get; set; }

       

        public System.DateTime CreateTime { get; set; }

        public string FullIndex { get; set; }

        public string SourceFrom { get; set; }

        #endregion
    }

    public class IndexTools
    {

        static OptimizeOptions optimizeOptions = new OptimizeOptions();
        static ISolrResponseParser<NamedList, ResponseHeader> binaryResponseHeaderParser = new BinaryResponseHeaderParser();
        static IUpdateParametersConvert<NamedList> updateParametersConvert = new BinaryUpdateParametersConvert();
        static ISolrUpdateConnection<NamedList, NamedList> solrUpdateConnection = new SolrUpdateConnection<NamedList, NamedList>() { ServerUrl = "http://10.0.81.5:8080/solr/" };
        static ISolrUpdateOperations<NamedList> updateOperations = new SolrUpdateOperations<NamedList, NamedList>(solrUpdateConnection, updateParametersConvert) { ResponseWriter = "javabin" };

        static ISolrQueryConnection<NamedList> connection = new SolrQueryConnection<NamedList>() { ServerUrl = "http://10.0.81.5:8080/solr/" };
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

                PropertyInfo[] properites = typeof(IndexProductEstateModel).GetProperties();//得到实体类属性的集合
                string[] dateFields = { "CreateDate" };
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

            var result = updateOperations.Update("collection1", "/update", new UpdateOptions() { Docs = docs });
            var header = binaryResponseHeaderParser.Parse(result);

            System.Console.WriteLine(string.Format("Update Status:{0} QTime:{1}", header.Status, header.QTime));
            System.Console.ReadLine();

        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        public void Delete(string[] indexid)
        {
            var result = updateOperations.Update("collection1", "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, DelById = indexid });
            var header = binaryResponseHeaderParser.Parse(result);

            System.Console.WriteLine(string.Format("Update Status:{0} QTime:{1}", header.Status, header.QTime));
          //  System.Console.ReadLine();
        }

        /// <summary>
        /// Add原则操作
        /// </summary>
        public void AtomAdd(object data)
        {
            var docs = new List<SolrInputDocument>();
            DataTable list = data as DataTable;
            foreach (DataRow pro in list.Rows)
            {
                var model = new SolrInputDocument();

                PropertyInfo[] properites = typeof(IndexProductEstateModel).GetProperties();//得到实体类属性的集合
                string[] dateFields = { "CreateTime" };
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

            var result = updateOperations.Update("collection1", "/update", new UpdateOptions() {OptimizeOptions=optimizeOptions,Docs = docs });
            var header = binaryResponseHeaderParser.Parse(result);

            System.Console.WriteLine(string.Format("Update Status:{0} QTime:{1}", header.Status, header.QTime));
       
        }


    }
}
