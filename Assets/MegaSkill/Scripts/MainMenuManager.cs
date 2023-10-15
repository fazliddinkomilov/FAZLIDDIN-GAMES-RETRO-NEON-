using System.Collections;
using MegaSkill.Keyboard;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MegaSkill.Main
{
    public class MainMenuManager : MonoBehaviour {
        public static MainMenuManager main;
        public string NextScene;
        public PasswordManager passwordManager;
        [Space(30)]
        [Header("Connection")]
        public GameObject wifiLoadingIcon;
        public GameObject wifiICon;
        public GameObject noWifiICon;
        public float connectionCheckInterval = 10;
        [Space(30)]
        [Header("Player data")]
        [Header("Single Player")]
        public GameObject singlePlayerDataMenu;
        public PlayerDataUI singlePlayerUI;
        public EditTextField[] requiredFieldsSingle;
        [Header("Team Player")]
        public GameObject teamPlayerDataMenu;
        public PlayerDataUI[] teamPlayer2UIs;
        public EditTextField teamNameField;
        public EditTextField[] requiredFieldsTeam;

        [Space(30)]
        [Header("Timer")]
        public TMP_Text timerLimitText;
        [Header("Game Mode")]
        public Toggle mode_toggle_demo;
        public Toggle mode_toggle_single;
        public Toggle mode_toggle_team;
        [Header("Stats")]
        public TMP_Text demoSessionStat;
        public TMP_Text normalSessionStat;

        [Header("Menus")]
        public int startMenuID = 0;
        public GameObject[] menus;

        private const int TEAM_SIZE = 2;
    
        private void Awake() {
            main = this;
            passwordManager.password = MegaSkillData.main.AdminPassword;
        }

        private void Start() {
            foreach (var item in requiredFieldsSingle)
                item.SetIsRequired(true);
            foreach (var item in requiredFieldsTeam)
                item.SetIsRequired(true);

            if (SaveManager.main.data.timerLimitInMin < 1)
                SaveManager.main.data.timerLimitInMin = 8;

            int playerCount = SaveManager.main.isTeamMode ? 2: 1;
            int currPlayerID = DataManager.main.currentPlayerID;
            int newPlayerID = currPlayerID + 1;

            if (currPlayerID >= 0 && newPlayerID < playerCount)
            {
                DataManager.main.currentPlayerID = newPlayerID;
                SceneManager.LoadScene(NextScene);
            }else{
                UpdateTimerLimit();
                GoToMainMenu();
            }

            DataManager.main.SyncData();
            StartCoroutine(_CheckConnection());
        }
    
        IEnumerator _CheckConnection() {
            while (true){
                wifiLoadingIcon.SetActive(true);
                wifiICon.SetActive(false);
                noWifiICon.SetActive(false);
        
                UnityWebRequest request = UnityWebRequest.Get(MegaSkillData.main.ConnectionCheckURL);
                yield return request.SendWebRequest();
        
                wifiLoadingIcon.SetActive(false);
                if (request.result != UnityWebRequest.Result.Success){
                    noWifiICon.SetActive(true);
                }else{
                    wifiICon.SetActive(true);
                }

                yield return new WaitForSeconds(connectionCheckInterval);
            }
        }

        void UpdateTimerLimit() {
            timerLimitText.text = SaveManager.main.data.timerLimitInMin.ToString();
            SaveManager.main.Save();
        }

        public void AddTimerLimit(int amount) {
            SaveManager.main.data.timerLimitInMin = Mathf.Clamp(SaveManager.main.data.timerLimitInMin + amount, 5, 15);
            UpdateTimerLimit();
        }

        public void ChangeMenu(GameObject obj) {
            foreach (var menu in menus) {
                menu.SetActive(menu == obj);
            }
        }

        public bool CheckDataFilled(){
            EditTextField[] fields = SaveManager.main.isTeamMode ? requiredFieldsTeam : requiredFieldsSingle;
            foreach (var item in fields)
                if(item.isEmpty())return false;

            return true;
        }

        public void StartGame(){
            if(!CheckDataFilled())
                return;

            DataManager.main.currentPlayerID = 0;
        
            // SaveManager.main.data.teamCnt++;
            // SaveManager.main.Save();

            if(SaveManager.main.isTeamMode){
                DataManager.main.SetPlayerCnt(TEAM_SIZE);
                for (int i = 0; i < TEAM_SIZE; i++)
                    DataManager.main.SetPlayerData(teamPlayer2UIs[i], i);
                DataManager.main.SetTeamName(teamNameField.GetText());
            }else{
                DataManager.main.SetPlayerCnt(1);
                DataManager.main.SetPlayerData(singlePlayerUI, 0);
            }
            DataManager.main.ResetStats();
            SceneManager.LoadScene(NextScene);
        }

        // void PlayerData2UI(PlayerDataUI UI, PlayerData data){
        //     UI.Name.SetText(data.name);
        //     UI.GroupName.SetText(data.groupName);
        //     UI.ID.SetText(data.id);
        // }
        public void UpdateGameMode(){
            if (mode_toggle_demo.isOn)
                SaveManager.main.data.gameMode = GameMode.Demo;
            if (mode_toggle_single.isOn)
                SaveManager.main.data.gameMode = GameMode.Single;
            if (mode_toggle_team.isOn)
                SaveManager.main.data.gameMode = GameMode.Team;
            SaveManager.main.Save();
        }
        public void UpdateGameModeUI(){
            switch (SaveManager.main.data.gameMode){
                case GameMode.Single:
                    mode_toggle_single.isOn = true;
                    break;
                case GameMode.Team:
                    mode_toggle_team.isOn = true;
                    break;
                case GameMode.Demo:
                    mode_toggle_demo.isOn = true;
                    break;
            }
        }

        void UpdatePlayerDataUI(){
            bool isTeam = SaveManager.main.isTeamMode;
            singlePlayerDataMenu.SetActive(!isTeam);
            teamPlayerDataMenu.SetActive(isTeam);
        }

        public void ResetDemoSessionCount(){
            SaveManager.main.data.demoSessionCnt = 0;
            SaveManager.main.Save();
            UpdateSessionStats();
        }
        public void ResetNormalSessionCount(){
            SaveManager.main.data.normalSessionCnt = 0;
            SaveManager.main.Save();
            UpdateSessionStats();
        }

        void UpdateSessionStats(){
            demoSessionStat.text = SaveManager.main.data.demoSessionCnt.ToString();
            normalSessionStat.text = SaveManager.main.data.normalSessionCnt.ToString();
        }

        public void GoToMainMenu(){
            ChangeMenu(menus[0]);
            UpdatePlayerDataUI();
        }
        public void GoToPasswordMenu(){
            ChangeMenu(menus[1]);
        }

        public void GoToAdminMenu(){
            ChangeMenu(menus[2]);
            RegionManager.main.UpdateActiveRegion();
            UpdateGameModeUI();
            UpdateSessionStats();
        }
        public void GoToRegionMenu(){
            ChangeMenu(menus[3]);
            RegionManager.main.ReloadData();
        }
    }
}
