using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("LoadScene")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TextMeshProUGUI tmpLoadText;
    [SerializeField] private Image imageLoadBar;
    [SerializeField] private string[] loadingTexts;
    [SerializeField] private string textAfterScreenLoaded = "Touch Screen to continue";
    public void StartGame() {
        LoadScene(1);
    }

     private void LoadScene(int sceneBuildIndex)
    {
        imageLoadBar.fillAmount = 0;
        loadingPanel.SetActive(true);
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        StartCoroutine(LoadSceneOperation(asyncOperation));
    }
    IEnumerator LoadSceneOperation(AsyncOperation asyncOperation)
    {
        float lastTextTime = Time.time;
        var ratio = 1/(loadingTexts.Length * 2);
        var txtIndex = 0;

        tmpLoadText.text = loadingTexts[txtIndex];
        
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone)
        {
            
            if(Time.time >= lastTextTime + ratio && txtIndex < loadingTexts.Length-1)
            {
                lastTextTime = Time.time;
                txtIndex++;
                tmpLoadText.text = loadingTexts[txtIndex];
            }
            
            imageLoadBar.fillAmount = asyncOperation.progress;
            
            if (asyncOperation.progress >= 0.9f)
            {
                imageLoadBar.fillAmount = 1;
                
                tmpLoadText.text = textAfterScreenLoaded;
                
                if (Input.touchCount > 0)
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
