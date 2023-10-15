using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MegaSkill.Utils;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

namespace MegaSkill.Main
{
    public enum GameMode{
        Single, Team, Demo
    }

    [System.Serializable]
    public struct PlayerData{
        public GameMode mode;
        public string name;
        public string group;
        public string studentID;
        public string teamName;
        public int score;
        public int teamScore;
        public float duration;
        public int attempts;
    }

    public class DataManager : MonoBehaviour
    {
        private static DataManager _main;
        Stopwatch watch;
        public int attempts;
        public int successCount;
        public int failCount;
        private int currentLevelID;

        public int currentPlayerID = -1;

        public PlayerData[] playerDatas;
        MonoRoutine SyncMR;

        public static DataManager main{
            get{
                if(_main == null){
                    _main = new GameObject("DataManager").AddComponent<DataManager>();
                }
                return _main;
            }
        }

        private void Awake() {
            if(_main != null && _main != this){
                Destroy(this);
                return;
            }

            _main = this;
            DontDestroyOnLoad(gameObject);
            watch = new Stopwatch();
            SyncMR = new MonoRoutine(this);
        }

        public bool isTimerRunning() {
            return watch.IsRunning;
        }
        public void StartTimer(){
            watch.Reset();
            watch.Restart();
        }
        public void StopTimer(){
            watch.Stop();
        }

        public bool IsLastLevel() {
            return currentLevelID + 1 >= MegaSkillData.main.Levels.Length;
        }
        public string GetCurrentLevel() {
            if (MegaSkillData.main.Levels.Length == 0){
                Debug.LogError("No Levels Selected in MegaSkillData", MegaSkillData.main);
                return null;
            }
            return MegaSkillData.main.Levels[currentLevelID];
        }
        public string GetNextLevel() {
            currentLevelID++;
            return MegaSkillData.main.Levels[currentLevelID];
        }
        public void ResetStats(){
            watch.Stop();
            watch.Reset();
            attempts = 0;
            successCount = 0;
            failCount = 0;
            currentLevelID = 0;
        }

        public float GetTime(){
            return (float)watch.Elapsed.TotalSeconds;
        }

        public void SetPlayerCnt(int cnt){
            playerDatas = new PlayerData[cnt];
        }
        public void SetPlayerData(PlayerDataUI uiData, int playerID){
            playerDatas[playerID].name = uiData.Name.GetText();
            playerDatas[playerID].group = uiData.GroupName.GetText();
            playerDatas[playerID].studentID = uiData.ID.GetText();
        }
        public void SetTeamName(string name){
            for (int i = 0; i < playerDatas.Length; i++)
            {
                PlayerData item = playerDatas[i];
                item.teamName = name;
            }
        }

        public void SetCurrentPlayerData(float duration, int attempts, int score){
            if(!playerDatas.IsRange(currentPlayerID))return;
            playerDatas[currentPlayerID].mode = SaveManager.main.data.gameMode;
            playerDatas[currentPlayerID].duration = duration;
            playerDatas[currentPlayerID].attempts = attempts;
            playerDatas[currentPlayerID].score = score;
        }

        public void UpdateTeamData(){
            if (SaveManager.main.data.gameMode != GameMode.Team) return;
            int teamScore = playerDatas[0].score + playerDatas[1].score;
            playerDatas[0].teamScore = playerDatas[1].teamScore = teamScore;
        }

        public bool IsLastPlayerInTeam(){
            return currentPlayerID + 1 >= playerDatas.Length;
        }

        public void SaveUserStats(){
            foreach (var item in playerDatas){
                SaveManager.main.data.playerDataExports.Add(item);
            }
            SaveManager.main.Save();
            SyncData();
        }

        private static string SecToTime(float t){
            var minutes = (int)(t / 60);
            var seconds = t % 60;
            return minutes.ToString() + ":" + seconds.ToString("n2");
        }

        private static List<GoogleSheetData> PlayerDataToSheet(PlayerData pdata){
            List<GoogleSheetData> data = new()
            {
                new GoogleSheetData(MegaSkillData.main.ENTRY_DeviceID, SystemInfo.deviceUniqueIdentifier),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Region, SaveManager.main.data.region),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Mode, pdata.mode.ToString()),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Name, pdata.name),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Group, pdata.group),
                new GoogleSheetData(MegaSkillData.main.ENTRY_StudentID, pdata.studentID),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Score, pdata.score.ToString()),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Duration, SecToTime(pdata.duration)),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Attempts, pdata.attempts.ToString()),
                new GoogleSheetData(MegaSkillData.main.ENTRY_TeamName, pdata.teamName ?? ""),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Team_Score, pdata.teamScore.ToString())
            };
            return data;
        }

        public void SyncData(){
            SyncMR.Start(_SyncData());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator _SyncData(){
            var dataList = SaveManager.main.data.playerDataExports;
            while (dataList.Count > 0)
            {
                var data = PlayerDataToSheet(dataList[0]);
                var www = GoogleSheetsDataSender.GetRequest(MegaSkillData.main.StatsSheetURL, data);
                yield return www.SendWebRequest();
                if(www.result == UnityWebRequest.Result.Success){
                    dataList.RemoveAt(0);
                    SaveManager.main.Save();
                    yield return null;
                }else{
                    yield break;
                }
            }
        }

        public void SendTestData(){
            List<GoogleSheetData> data = new()
            {
                new GoogleSheetData(MegaSkillData.main.ENTRY_DeviceID, "Test: DeviceID"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Region, "Test: Region"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Mode, "Test: Mode"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Name, "Test: Name"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Group, "Test: Group"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_StudentID, "Test: StudentID"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Score, "Test: Score"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Duration, "Test: Duration"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Attempts, "Test: Attempts"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_TeamName, "Test: TeamName"),
                new GoogleSheetData(MegaSkillData.main.ENTRY_Team_Score, "Test: Team_Score")
            };
            var www = GoogleSheetsDataSender.GetRequest(MegaSkillData.main.StatsSheetURL, data);

            IEnumerator Send(){
                yield return www.SendWebRequest();
            }

            StartCoroutine(Send());
        }
    }
}