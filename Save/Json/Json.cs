using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Platformer2D
{ 
    public class Json
    {
        protected string _filePath;

        public Json()
        {
            _filePath = Application.persistentDataPath;
        }

        public void Save(ISaveData data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void Load(ISaveData data)
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                //data = JsonConvert.DeserializeObject<SettingsData>(json);
                JsonUtility.FromJsonOverwrite(json, data);
            }
            catch (FileNotFoundException)
            {
            }
        }

        public bool IsExistsFilePath()
        {
            return File.Exists(_filePath);
        }
    }
}
