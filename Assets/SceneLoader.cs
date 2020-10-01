using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class SceneLoader : MonoBehaviour
{
    public AssetReference scene;
    private AsyncOperationHandle<SceneInstance> handle;
    private bool unloaded;
    public GameObject Img;
    private void Awake()
    {
     //   DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

        Addressables.LoadSceneAsync(scene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed
         += SceneLoadCompleted;
    }
    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded scene.");
        //    UnityEngine.SceneManagement.SceneManager.UnloadScene("SampleScene");
            handle = obj;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !unloaded)
        {
            unloaded = true;
            UnloadScene();
        }
    }
    void UnloadScene()
    {
        Addressables.UnloadSceneAsync(handle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
                Debug.Log("Successfully unloaded scene.");
        };
    }

    public void OnImage()
    {
        Img.SetActive(true);
    }

}