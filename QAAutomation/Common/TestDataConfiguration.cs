using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using QA.Automation.Common.Models;

namespace QA.Automation.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TestDataConfiguration
    {
        #region --- Fields ---

        private readonly string _dataFolderLocation;
        private readonly EnvironmentType _environment;

        #endregion

        #region --- Constructors ---
        public TestDataConfiguration(string dataFolderLocation, EnvironmentType environment)
        {
            _dataFolderLocation = dataFolderLocation;
            _environment = environment;
        }

        #endregion

        #region --- Public Methods ---
        public T GetTestDataConfiguration<T>(string filename)
        {
            T data;

            try
            {
                var partialDataFolderPath = Path.Combine(_dataFolderLocation, filename);
                data = ConfigurationSettings.GetSettingsConfiguration<T>(partialDataFolderPath);
            }
            catch
            {
                data = default(T);
            }

            return data;
        }

        public IEnumerable<string> ConvertStringToList(string stringData, char delimiter = ',')
        {
            return !string.IsNullOrWhiteSpace(stringData) ? stringData.Split(delimiter).ToList<string>() : new List<string>();
        }

        public EnvironmentTestData GetDataFromFile(string testFileName)
        {
            var ed = GetEnvDataFromFile(testFileName);
            return ed ?? new EnvironmentTestData();
        }

        public EnvironmentTestData GetDataFromFiles(string testTemplateFile, string testTestFile)
        {
            var templateEd = new EnvironmentTestData();
            if (!string.IsNullOrWhiteSpace(testTemplateFile))
            {
                templateEd = GetEnvDataFromFile(testTemplateFile);
            }

            var tEd = GetEnvDataFromFile(testTestFile);

            if (templateEd.TestAnswers.Count > 0)
            {
                foreach (var item in tEd.TestAnswers)
                {
                    if (templateEd.TestAnswers.ContainsKey(item.Key))
                    {
                        templateEd.TestAnswers[item.Key] = item.Value;
                    }
                    else
                    {
                        templateEd.TestAnswers.Add(item.Key, item.Value);
                    }
                }
            }
            else
            {
                templateEd = tEd;
            }

            return templateEd ?? new EnvironmentTestData();
        }

        #endregion


        #region --- Private Methods ---
        private EnvironmentTestData GetEnvDataFromFile(string file)
        {
            var templateEd = new EnvironmentTestData();
            try
            {
                var templateData = GetTestDataConfiguration<TestData>(file);
                templateEd = templateData != null ? templateData.Data.FirstOrDefault(a => a.EnvironmentName.ToString().Equals(_environment.ToString(), StringComparison.OrdinalIgnoreCase)) : new EnvironmentTestData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return templateEd ?? new EnvironmentTestData();
        }
        #endregion
    }
}