using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Codebase
{
    public class Storage 
    {
        private const string FILE_NAME = "Data";
        private string _path;

        public Storage()
        {
#if UNITY_EDITOR
            _path = Application.dataPath + $"/{FILE_NAME}.json";
#else
            _path = Application.persistentDataPath + $"/{FILE_NAME}.json";
#endif
        }

        public void Save(GameData data)
        {
            string jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(_path, jsonString);
            Debug.Log("Saved!");
        }

        public GameData Load()
        {
            if (!File.Exists(_path))
                return new GameData();

            string jsonString = File.ReadAllText(_path);
            GameData myData = JsonUtility.FromJson<GameData>(jsonString);
            return myData;
        }
    }
}
