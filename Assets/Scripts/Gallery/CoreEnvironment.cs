using UnityEngine;
using Yurject;

public class CoreEnvironment : MonoBehaviour
{
    [SerializeField] private CreatorArtCells creatorArtCells;

    [Inject]
    public void Container(CreatorArtCells creatorArtCells)
    {
        this.creatorArtCells = creatorArtCells;
    }
   
    private void Start()
    {
        creatorArtCells.Init();
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
