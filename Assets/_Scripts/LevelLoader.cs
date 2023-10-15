using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private int Level;
    private void Start()
    {
        Level = PlayerPrefs.GetInt("gamelevel");
    }
    public void LoadScene(int loadSceneIndex)
    {
        PlayerPrefs.SetInt("gamelevel", loadSceneIndex);

        SceneManager.LoadSceneAsync(1);



    }

    public void LoadNextScene()
    {
   
        PlayerPrefs.SetInt("currentLevel", Level);
      
            SceneManager.LoadSceneAsync(1);
      


    }

    public void NewGame()
    {
        Level = 0;
        PlayerPrefs.SetInt("gamelevel", Level);
        SceneManager.LoadSceneAsync(1);
    }

    private void Update()
    {
      if(Input.GetKey(KeyCode.K))
        {
            NewGame();
        }
        if (Input.GetKey(KeyCode.L))
        {
            LoadScene(1);
        }
    }

    public void Reload()
    {
  
        PlayerPrefs.SetInt("currentLevel", Level);

        SceneManager.LoadSceneAsync(1);



    }


}
