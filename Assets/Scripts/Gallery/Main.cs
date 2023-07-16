using System;
using UnityEngine;
using UnityEngine.UI;
using Yurject;

public class Main : MonoBehaviour
{
    [SerializeField] private Button galleryButton;
    private  SceneLoader sceneLoader;


    [Inject]
    public void Container(SceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }

    private void Start()
    {
        Init(sceneLoader);
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void Init(SceneLoader sceneLoader)
    {
        try
        {
            sceneLoader.Init();
            galleryButton.Subscribe(() 
                => sceneLoader.LoadScene(SceneLoader.SceneName.Gallery));
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    private void OnDestroy()
    {
        galleryButton.Unsubscribe(()
            => sceneLoader.LoadScene(SceneLoader.SceneName.Gallery));
    }
}
