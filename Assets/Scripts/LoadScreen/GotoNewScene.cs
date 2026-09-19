using DG.Tweening;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNewScene : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public static string sceneName;
    public void SetNewScene(string sceneName)
    {
        GotoNewScene.sceneName = sceneName;
        StartCoroutine(LoadSceneAsync("LoadScreen"));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        canvasGroup.DOFade(1, 1.5f);

        yield return new WaitForSeconds(3f);


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
