using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnionItemMarks : MonoBehaviour
{
    [SerializeField] private GameObject _checkMark;
    [SerializeField] private GameObject _xMark;
    [SerializeField] private GameObject _uninIcon;
    public PaymentHandler buyerPaymentHandler;
    private List<Transform> markedItems = new List<Transform>();

    public void SetMatrkStatus(bool markStatus)
    {
        if (_uninIcon.activeSelf)
        {
            if (markStatus)
            {
                StartCoroutine(AddMarkers(_checkMark));
            }
            else
            {
                StartCoroutine(AddMarkers(_xMark));
            }
        }
    }
    private IEnumerator AddMarkers(GameObject mark)
    {
        Transform[] items = new Transform[_uninIcon.transform.childCount];
        for (int i = 0; i < _uninIcon.transform.childCount; i++)
        {
            items[i] = _uninIcon.transform.GetChild(i);
        }
        bool addFirstItem = false;
        foreach (Transform item in items)
        {
            if (addFirstItem)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                addFirstItem = true;
                yield return new WaitForSeconds(1.5f);
            }

            if (!markedItems.Contains(item))
            {
                markedItems.Add(item);
                Image itemInUnion = item.GetComponent<Image>();
                CreateMark(itemInUnion, mark);
            }
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(buyerPaymentHandler.SetReactionAndGo());
        yield return new WaitForSeconds(1.25f);
        transform.gameObject.SetActive(false);
        Array.Clear(items, 0, items.Length);
        ClearUnion();

    }
    private void ClearUnion()
    {
        Transform childUnion = _uninIcon.transform;
        int childCount = childUnion.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = childUnion.GetChild(i);
            Debug.Log(child);
            Destroy(child.gameObject);
        }
    }
    private void CreateMark(Image itemImage, GameObject mark)
    {
        Color originalColor = itemImage.color;
        Color newColor = originalColor;
        newColor.a = 0.3f;
        itemImage.color = newColor;
        GameObject overlayImageObject = Instantiate(mark, itemImage.transform);
        overlayImageObject.transform.localPosition = Vector3.zero;
        overlayImageObject.transform.localScale = new Vector3(0.165319f, 0.165319f, 0f);
    }

}
