using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace MegaSkill.Main
{
    public class RegionManager : MonoBehaviour
    {
        public static RegionManager main;
        public TMP_Text activeRegionText;
        public TMP_Text noRegionText;
        public Transform regionBtnsParent;
        public MenuButton regionBtnPref;
        public Transform filterBtnsParent;
        public MenuButton filterBtnPref;
        public TMP_Text errorMsg;
        public GameObject loadIcon;

        List<MenuButton> regionButtons;
        List<MenuButton> filterButtons;

        private void Awake() {
            main = this;
        }

        public void UpdateActiveRegion(){
            activeRegionText.text = SaveManager.main.data.region;
            bool selected = SaveManager.main.data.region != "";
            activeRegionText.gameObject.SetActive(selected);
            noRegionText.gameObject.SetActive(!selected);
        }
        public void ReloadData(){
            StartCoroutine(_loadData());
        }

        public void SetRegion(string regionName){
            SaveManager.main.data.region = regionName;
            SaveManager.main.Save();
            MainMenuManager.main.GoToAdminMenu();
        }

        public void UpdateRegionFilter(){
            foreach (var item in regionButtons){
                item.gameObject.SetActive(filterButtons[item.categoryID].toggle.isOn);
            }
        }
        
        void ClearChild(Transform tr) {
            foreach (Transform child in tr){
                Destroy(child.gameObject);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator _loadData(){
            UnityWebRequest request = UnityWebRequest.Get(MegaSkillData.main.RegionsURL);
            errorMsg.gameObject.SetActive(false);
            loadIcon.SetActive(true);
            regionButtons = new List<MenuButton>();
            filterButtons = new List<MenuButton>();
            ClearChild(regionBtnsParent);
            ClearChild(filterBtnsParent);
            
            // int childs = regionBtnsParent.childCount;
            // for (int i = childs - 1; i >= 0; i--)
            //     Destroy(regionBtnsParent.GetChild(i).gameObject);

            yield return request.SendWebRequest();


            loadIcon.SetActive(false);
            if (request.result != UnityWebRequest.Result.Success){
                Debug.Log(request.error);
                errorMsg.gameObject.SetActive(true);
                errorMsg.text = request.error;
                yield break;
            }

            string[] lines = request.downloadHandler.text.Split('\n');

            List<string> categoryNames = new List<string>();

            int GetCategoryIndex(string cName){
                int id = 0;
                foreach (var item in categoryNames){
                    if(item == cName)
                        return id;
                    id++;
                }

                categoryNames.Add(cName);
                return categoryNames.Count - 1;
            }
            
            // Region Buttons
            foreach (string line in lines){
                string[] elements = line.Split(',');
                MenuButton btn = Instantiate(regionBtnPref, regionBtnsParent);
                string name = elements[0].Trim('"').Trim();
                string categoryName = elements[1].Trim('"').Trim();
                btn.categoryID = GetCategoryIndex(categoryName);
                btn.data = btn.UIText.text = name;
                btn.button.onClick.AddListener(()=>SetRegion(name));
                regionButtons.Add(btn);
            }

            // Filter Buttons
            int id = 0;
            foreach (string item in categoryNames){
                MenuButton btn = Instantiate(filterBtnPref, filterBtnsParent);
                btn.UIText.text = item;
                btn.categoryID = id;
                btn.toggle.onValueChanged.AddListener((bool b)=> UpdateRegionFilter());
                filterButtons.Add(btn);
                id++;
            }
            UpdateRegionFilter();
        }
    }
}
