using DemoPRojectConsole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using YamlDotNet.Serialization;

namespace DemoPRojectConsole
{
    public class Convertor
    {
        private IDal iDal;
        public Convertor(IDal dal)
        {
            iDal = dal;
        }

        public void ConvertFile(string input)
        {
            try
            {
                if (input.StartsWith("http:\\"))
                {
                    // Call method accordingly for converting URL to certain format
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(input);
                    string extn = fileInfo.Extension;
                    if (extn == ".yaml")
                    {
                        ConvertYamlFileToJSON(input);
                    }
                    else if (extn == ".json")
                    {
                        ConvertJSONFileToCSV(input);
                    }
                    else
                    {
                        Console.WriteLine("This file format is not supported");
                    }
                }
            }
            catch (Exception ex)
            {
                // Logging can done by writing into the files
                //_logger.WriteError("Exception", ex);
                throw;
            }
        }
        public void ConvertJSONFileToCSV(string input)
        {
            try
            {
                using (StreamReader r = new StreamReader(@input))
                {
                    string json = r.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<Root>(json);
                    jsonStringToCSV(json);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ConvertYamlFileToJSON(string input)
        {
            try
            {
                // convert string/file to YAML object
                var r = new StreamReader(@input);
                var deserializer = new Deserializer();
                var yamlObject = deserializer.Deserialize(r);

                // now convert the object to JSON. 
                Newtonsoft.Json.JsonSerializer js = new Newtonsoft.Json.JsonSerializer();

                var sw = new StringWriter();
                js.Serialize(sw, yamlObject);
                string jsonText = sw.ToString();
                jsonStringToCSV(jsonText);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Common logic : To convert JSON to CSV format
        /// </summary>
        /// <param name="jsonContent"></param>
        public static void jsonStringToCSV(string jsonContent)
        {
            try
            {
                XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml.InnerXml);
                XmlReader xmlReader = new XmlNodeReader(xml);
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlReader);
                var dataTable = dataSet.Tables[1];

                //Datatable to CSV
                var lines = new List<string>();
                string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();
                var header = string.Join(",", columnNames);
                lines.Add(header);
                var valueLines = dataTable.AsEnumerable()
                                   .Select(row => string.Join(",", row.ItemArray));
                lines.AddRange(valueLines);
                //Output location can be configurable to app settings ..similar to file input
                File.WriteAllLines(@"D:/Export.csv", lines);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
