
using UnityEngine;
using UnityEngine.UI;

public class Art : Cell
{
    [SerializeField] private Image artImage;
    [SerializeField] private Button closeButton;

    public static string url;

   
    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(CorSendRequest(url));
        closeButton.Subscribe(() => SceneLoader.Load( SceneLoader.SceneName.Gallery));
    }
    
}
