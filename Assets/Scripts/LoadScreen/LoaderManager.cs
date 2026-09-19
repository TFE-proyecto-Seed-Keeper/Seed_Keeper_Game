using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{

    CanvasGroup canvasGroup;
    public void SetNewScene()
   {
       StartCoroutine(LoadSceneAsync(GotoNewScene.sceneName));
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        SetNewScene();
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
