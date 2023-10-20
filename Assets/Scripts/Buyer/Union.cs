using UnityEngine;
using UnityEngine.UI;

public class Union : MonoBehaviour
{
    [SerializeField] private AudioClip _showUnion;
    [SerializeField] private AudioClip _hideUnion;
    private void OnEnable()
    {
        if(_showUnion)
        SoundManager.instance.PlaySound(_showUnion);
    }
    private void OnDisable()
    {
          if(_hideUnion)
        SoundManager.instance.PlaySound(_hideUnion);
    }
    [SerializeField] private GameObject _unionImg;
    public void AddImageInUnion(Sprite image)
    {
        GameObject newImageObject = new GameObject(name);
        Image imageComponent = newImageObject.AddComponent<Image>();
        imageComponent.sprite = image;
        RectTransform rectTransform = imageComponent.GetComponent<RectTransform>();
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = new Vector2(0.03f, 0.03f);
        newImageObject.transform.SetParent(_unionImg.transform);


    }
}
