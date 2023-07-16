using UnityEngine;

namespace Yurject
{
    /// <summary>
    /// add component on gameObject and realize attribute [inject]
    /// </summary>

    public class Test : MonoBehaviour
    {
        public CreatorArtCells ArtLoader;
        [Inject]
        public void Container(CreatorArtCells artLoader)
        {
            ArtLoader = artLoader;
        }
    }
}