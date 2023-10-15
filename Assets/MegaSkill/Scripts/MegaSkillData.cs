using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace MegaSkill.Main
{
    [CreateAssetMenu(fileName = "MegaSkillData", menuName = "MegaSkill", order = 0)]
    public class MegaSkillData : ScriptableObject
    {
        private static MegaSkillData _main = null;
        public string AdminPassword = "";

        [Space(30)]
        [Header("Scenes")]
        public string[] Levels;
        public string FinishScene = "FinishMenu";

        [Space(30)]
        [Header("Stats URL:")]
        [TextArea]
        public string StatsSheetURL;
        public string ENTRY_DeviceID = "entry.1124466930";
        public string ENTRY_Region = "entry.664169668";
        public string ENTRY_Mode = "entry.594185121";
        public string ENTRY_Name = "entry.116202082";
        public string ENTRY_Group = "entry.16747230";
        public string ENTRY_StudentID = "entry.919100097";
        public string ENTRY_Score = "entry.1422784263";
        public string ENTRY_Duration = "entry.741377427";
        public string ENTRY_Attempts = "entry.1211038660";
        public string ENTRY_TeamName = "entry.1107303370";
        public string ENTRY_Team_Score = "entry.200428432";

        
        [Space(30)]
        [Header("General")]
        // https://docs.google.com/spreadsheets/d/{key}/gviz/tq?tqx=out:csv&sheet={sheet_name}
        [TextArea]
        public string RegionsURL = "https://docs.google.com/spreadsheets/d/1pPJKKx-W_KXJQsdiemyWwz4TWNhTxJn7mRdMz1mh7Yg/gviz/tq?tqx=out:csv&sheet=Sheet1";
        [TextArea]
        public string ConnectionCheckURL = "https://docs.google.com/spreadsheets/d/1pPJKKx-W_KXJQsdiemyWwz4TWNhTxJn7mRdMz1mh7Yg/gviz/tq?tqx=out:csv&sheet=ConnectionTest";

        public static MegaSkillData main{
            get{
                if (_main == null){
                    MegaSkillData[] objs = Resources.LoadAll<MegaSkillData>("");
                    if (objs.Length == 0){
                        Debug.LogError("No MegaSkill Data Found");
                        return null;
                    }
                    _main = objs[0];
                }
                return _main;
            }
        }
    }
}
