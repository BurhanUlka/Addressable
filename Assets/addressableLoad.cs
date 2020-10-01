using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

public class addressableLoad : MonoBehaviour
{
    public AssetReference scene;

    private AsyncOperationHandle<SceneInstance> handle;
    private bool unloaded;

    private void Awake()
    {
     //   DontDestroyOnLoad(gameObject);
    }

    public void DownloadAsset()
    {
        StartCoroutine(DoDownload());
    }

    IEnumerator DoDownload()
    {
        var dl = Addressables.DownloadDependenciesAsync(scene);
        dl.Completed += (AsyncOperationHandle) =>
        {
            DownloadComplete();

        };

        while (dl.PercentComplete < 1 && !dl.IsDone)
        {
            Debug.Log("Downloading Asset: " + dl.PercentComplete.ToString());
            yield return null;
        }

    }

    private void DownloadComplete()
    {
        Debug.Log("OOOOOK");
        Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).Completed += SceneLoadComplete;
    }

    private void Start()
    {
        DownloadAsset();  
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && !unloaded)
        {
            unloaded = true;
            UnloadScene();
        }
    }

    private void SceneLoadComplete(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(obj.Result.Scene.name + " successfully loaded.");
            handle = obj;
        }
        else if(obj.Status == AsyncOperationStatus.Failed){
            Debug.Log(obj.Result.Scene.name + " failed to load.");
        }

    }

    private void UnloadScene()
    {
        Addressables.UnloadSceneAsync(handle, true).Completed += op =>
         {
             if (op.Status == AsyncOperationStatus.Succeeded)
                 Debug.Log("Successfully unloaded scene");
 
         };
    }

}
