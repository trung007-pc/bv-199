using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Core.Const;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace WebClient.LanguageResources
{
    public class JsonStringLocalizer
    {
        public readonly JsonSerializer _jsonSerializer = new JsonSerializer();
        public string Code { get; set; } = CultureType.VI;
        private List<LocalizedString> LocalizedString = new();

        public JsonStringLocalizer()
        {
            LoadResources(Code);
        }
    
        public void LoadResources(string code)
        {
            LocalizedString = new();
            string filePath = $@"LanguageResources/App.{code}.json";
            using (var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var sReader = new StreamReader(str))
            using (var reader = new JsonTextReader(sReader))
            {
                while (reader.Read())
                {
                    if (reader.TokenType != JsonToken.PropertyName)
                        continue;
                    string key = (string) reader.Value;
                    reader.Read();
                    string value = _jsonSerializer.Deserialize<string>(reader);
                    LocalizedString.Add(new LocalizedString(key, value, false));
                }
            }   
        }
    
    
        public LocalizedString this[string name]
        {
            get
            {
                var value =  LocalizedString.FirstOrDefault(x => x.Name == name);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                    : actualValue;
            }
        }
    }
}