
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
   [SerializeField] protected Image image;
    protected bool takenSprite;
    protected string imageUrl;

    public virtual void Init(string baseUrl, int artName)
    {
        imageUrl = baseUrl + artName + ".jpg";
        StartCoroutine(CorSendRequest(imageUrl));
    }
    protected IEnumerator CorSendRequest(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
            image.sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), Vector2.one * 0.5f);
            takenSprite = true;
        }
    }
}
