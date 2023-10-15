using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MegaSkill.Main
{
    [System.Serializable]
    public class SaveData{
        public GameMode gameMode;
        public int demoSessionCnt;
        public int normalSessionCnt;
        public string region;
        public int fromLan = 1;
        public int toLan = 0;
        public List<string> languages;
        public List<string> dictionary = new();
        public int timerLimitInMin;
        public List<PlayerData> playerDataExports = new();
    }

    public class SaveManager : MonoBehaviour {
        public SaveData data;

        private static SaveManager _main;
        public static SaveManager main{
            get{
                if(_main == null){
                    _main = new GameObject("SaveManager").AddComponent<SaveManager>();
                }
                return _main;
            }
        }

        public bool isDemoMode => data.gameMode == GameMode.Demo;
        public bool isTeamMode => data.gameMode == GameMode.Team;

        private void Awake() {
            if(_main != null && _main != this){
                Destroy(this);
                return;
            }

            _main = this;
            DontDestroyOnLoad(gameObject);
            data = new SaveData();
            Load();
        }

        const string fileName = "saveData";
        public void Load(){
            data = new SaveData();
            string json = ReadFromFile(fileName);
            if(json != ""){
                JsonUtility.FromJsonOverwrite(json, data);
            }
        }

        public void Save(){
            string json = JsonUtility.ToJson(data);
            WriteToFile(fileName, json);
        }

        private void WriteToFile(string fileName, string json){
            string path = GetFilePath(fileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream)){
                writer.Write(json);
            }
            fileStream.Close();
        }

        private string ReadFromFile(string fileName){
            string path = GetFilePath(fileName);
            if (File.Exists(path)){
                using (StreamReader reader = new StreamReader(path)){
                    string json = reader.ReadToEnd();
                    return json;
                }
            } else{
                Debug.LogWarning("File not found");
                return "";
            }
        }

        private string GetFilePath(string fileName){
            return Application.persistentDataPath + "/" + fileName;
        }

        // private void OnApplicationPause(bool pause){
        //     if (pause) Save();
        // }
    }
}