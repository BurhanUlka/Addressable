using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private AsyncOperationHandle<Sprite> sprite1;
    private AsyncOperationHandle<Sprite> sprite2;
    private AsyncOperationHandle<AudioClip> audioClip0;

    public ObjectPool objectPool;

    public SpriteRenderer[] sr;
    public Text[] buttonText;
    public Text aduioDownloadText;

    public AudioSource audioSource;

    private AsyncOperationHandle<Sprite> sprite0Ad;
    private AsyncOperationHandle<Sprite> sprite1Ad;
    private AsyncOperationHandle<AudioClip> audio0Ad;

    // Start is called before the first frame update
    void Start()
    {
        buttonText[0].text = objectPool.Sprites[0] != null ? "Set" : "Download";
        buttonText[1].text = objectPool.Sprites[1] != null ? "Set" : "Download";
        buttonText[2].text = objectPool.Audio[0] != null ? "Play" : "Download";
    }

    public void Button0()
    {
        if (objectPool.Sprites[0] == null)
        {
            sprite0Ad = Addressables.LoadAssetAsync<Sprite>("Sprite0");
            sprite0Ad.Completed += OnLoadDone0;
        }
        else
        {
            sr[0].sprite = objectPool.Sprites[0];
        }
    }

    private void OnLoadDone0(AsyncOperationHandle<Sprite> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            objectPool.Sprites[0] = obj.Result;
            buttonText[0].text = "Set";
        }

    }

    public void Button1()
    {
        if (objectPool.Sprites[1] == null)
        {
            sprite1Ad = Addressables.LoadAssetAsync<Sprite>("Sprite1");
            sprite1Ad.Completed += OnLoadDone1;
        }
        else
        {
            sr[1].sprite = objectPool.Sprites[1];
        }
    }

    private void OnLoadDone1(AsyncOperationHandle<Sprite> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            objectPool.Sprites[1] = obj.Result;
            buttonText[1].text = "Set";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (audio0Ad.IsValid())
        {
            aduioDownloadText.text = string.Format("Loading: {0}%", (int)(audio0Ad.PercentComplete * 100));
        }
    }

    public void Button2()
    {
        if (objectPool.Audio[0] == null)
        {
            audio0Ad = Addressables.LoadAssetAsync<AudioClip>("audio0");
            audio0Ad.Completed += OnLoadDone2;
        }
        else
        {
            audioSource.clip = objectPool.Audio[0];
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    private void OnLoadDone2(AsyncOperationHandle<AudioClip> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            objectPool.Audio[0] = obj.Result;
            buttonText[2].text = "Play";
        }

    }
}
