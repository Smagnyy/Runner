using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum SceneIndexes
{
    Title_Screen = 0,
    Main_Scene = 1
}

public class LoadingManager : MonoBehaviour
{
    private static LoadingManager instance;

    public TMP_Text progressText;
    public Image progressBar;
    public Image fadeScreen;
    public Image backGround;
    public TMP_Text clickCounterText;

    int clickCount, maxClickCount = 10;

    AsyncOperation loadingSceneOperation;
    bool canChangeScene;


    private void Start()
    {
        instance = this;
        
        backGround.gameObject.SetActive(false); ///////////////////////////////////////////À»ÿÕﬂﬂ —“–Œ◊ ¿, ÕŒ «¿“Œ ÀŒ¿ƒ — –»Õ ¬—≈√ƒ¿ œŒ ¿«€¬¿≈“—ﬂ Õ¿ › –¿Õ≈ œ–≈ƒœ–Œ—ÃŒ“–¿

        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.Title_Screen)
        {
            backGround.gameObject.SetActive(true);
            LoadNewScene(SceneIndexes.Main_Scene);
            StartCoroutine(instance.ProgressTextAnimating());
            clickCounterText.gameObject.SetActive(false);
        }

        
        
    }

    private void Awake()
    {
        StartCoroutine(FadeOut(fadeScreen.GetComponent<CanvasGroup>()));
    }

    public static void LoadNewScene(SceneIndexes sceneIndex)
    {
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync((int)sceneIndex);
        instance.loadingSceneOperation.allowSceneActivation = false;
        instance.canChangeScene = false;


    }

    public void OnClickToContinue()
    {
        
        //FadeScreen.color = new Color(0, 0, 0, 1);
        //StopCoroutine(Fading(true));
        if (clickCount < maxClickCount)
        {
            clickCount++;
            clickCounterText.gameObject.SetActive(true);
            clickCounterText.text = $"{clickCount}/{maxClickCount}";
            progressText.text = "Click";
            StopAllCoroutines();
            
        }
       
        StartCoroutine(FadeIn(fadeScreen.GetComponent<CanvasGroup>()));

        

    }


    private void Update()
    {
        if(backGround.gameObject.activeSelf)
        {
            progressBar.fillAmount = Mathf.RoundToInt(loadingSceneOperation.progress * 100f);
        }

        if (canChangeScene)
        {
            loadingSceneOperation.allowSceneActivation = true;
        }
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{

        //}
    }

    IEnumerator ProgressTextAnimating()
    {
        while(loadingSceneOperation.progress <0.9f)
        {
            string dots = "";
            for (int i = 0; i <= 3; i++)
            {
                if (loadingSceneOperation.progress > 0.9f) break;
                progressText.text = "Loading" + dots;
                dots += ".";
                
                yield return new WaitForSeconds(0.5f);
            }            
        }

        progressText.text = "Click to continue";

    }

    IEnumerator FadeIn(CanvasGroup obj)
    {
        Debug.Log("new Coroutine");
        //float i = 0;
        obj.alpha = 0;



        for (float i = 0; i <= 100; i+=100*Time.deltaTime)
        {
            obj.alpha = Mathf.Lerp(0, 1, i/100);
            yield return null;
        }
        canChangeScene = true;
        Debug.Log("‰Ó¯ÎÓ");
        //loadingSceneOperation.allowSceneActivation = true; ///////////////////////// ˝ÚÓ„Ó ÚÛÚ ·˚Ú¸ ÌÂ ‰ÓÎÊÌÓ
        //while (FadeScreen.color.a < toFade/255)
        //{
        //    //Debug.Log(i);

        //    i+=100 *Time.deltaTime;


        //}
    }

    IEnumerator FadeOut(CanvasGroup obj)
    {
        //Debug.Log("new Coroutine");
        //float i = 0;
        obj.alpha = 1;



        for (float i = 100; i >= 0; i -= 100 * Time.deltaTime)
        {
           // Debug.Log(i);
            obj.alpha = Mathf.Lerp(0, 1, i / 100);
            yield return null;
        }
    }

    /* ◊“Œ-“Œ Õ≈ “Œ
    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

    float totalSceneProgress;
    

    // Start is called before the first frame update
    void Awake()
    {
        //SceneManager.LoadSceneAsync((int)SceneIndexes.Title_Screen, LoadSceneMode.Additive);
        LoadGame();
    }

    void LoadGame()
    {
        progressBar.fillAmount = 0;

        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Title_Screen));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Main, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        
        for (int i = 0; i < sceneLoading.Count; i++)
        {
            sceneLoading[i].allowSceneActivation = false;
            while(!sceneLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in sceneLoading)
                {                   
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / sceneLoading.Count) * 100f;
                progressBar.fillAmount = Mathf.RoundToInt(totalSceneProgress);
                yield return null;

                
            }
        }
    }
    */
}
