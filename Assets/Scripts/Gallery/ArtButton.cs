
using UnityEngine;
using UnityEngine.UI;

public class ArtButton : Cell
{
    [SerializeField] private Button artButton;
  
    public override void Init(string baseUrl, int artName)
    {
        base.Init(baseUrl, artName);
        artButton.Subscribe(() => OnClickArt());
    }
 
    private void OnClickArt()
    {
        Art.url = imageUrl;
        SceneLoader.Load( SceneLoader.SceneName.ViewPic);
    }
}
