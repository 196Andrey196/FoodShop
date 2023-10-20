using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class ItemsBoard : MonoBehaviour
{
    [SerializeField] private List<Item> _itemsList;
    public List<Item> itemsList { get { return _itemsList; } }
    public List<Item> selectedItemForSell;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _checkMark;
    public int countSelectedItem;
    public int countItemInOreder;
    public event Action OnSellButtonClicked;
    [SerializeField] private AudioClip _clickButton;

    private void Start()
    {
        selectedItemForSell = new List<Item>();
        GenerateListItem();
    }
    private void Update()
    {
        if (countItemInOreder == 0)
        {
            DeselectItems();
        }
    }
    private void DeselectItems()
    {

        Transform[] items = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            items[i] = transform.GetChild(i);
            Image image = items[i].GetComponent<Image>();
            DeselectItem(image, items[i].gameObject, _itemsList[i]);
        }
    }
    public void ActiveSellButton(Button button)
    {
        Image itemImage = button.GetComponent<Image>();
        float alfaButton = 0;
        if (countSelectedItem == countItemInOreder)
        {
            alfaButton = 1f;
            button.interactable = true;
        }
        else
        {
            alfaButton = 0.5f;
            button.interactable = false;
        }

        ChangeButtonAlfa(itemImage, alfaButton);
    }


    void HandleButtonClick(GameObject button, Image itemImage, Item item)
    {
        SoundManager.instance.PlaySound(_clickButton);

        if (countSelectedItem < countItemInOreder && !item.isActive)
        {
            item.isActive = true;
            SelectItem(itemImage, button, item);
        }

        else
        {
            item.isActive = false;
            DeselectItem(itemImage, button, item);
        }

    }
    private void GenerateListItem()
    {
        foreach (Item item in _itemsList)
        {
            GameObject buttonObject = Instantiate(_buttonPrefab, transform);
            Image itemImage = buttonObject.GetComponent<Image>();
            itemImage.sprite = item.image;
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => HandleButtonClick(buttonObject, itemImage, item));
            item.isActive = false;
        }

    }
    private void ChangeButtonAlfa(Image itemImage, float alfa)
    {
        Color originalColor = itemImage.color;
        Color newColor = originalColor;
        newColor.a = alfa;
        itemImage.color = newColor;
    }

    private void SelectItem(Image itemImage, GameObject button, Item item)
    {
        ChangeButtonAlfa(itemImage, 0.3f);
        GameObject overlayImageObject = Instantiate(_checkMark, button.transform);
        overlayImageObject.transform.localPosition = Vector3.zero;
        selectedItemForSell.Add(item);
        countSelectedItem++;
    }
    private void DeselectItem(Image itemImage, GameObject button, Item item)
    {

        ChangeButtonAlfa(itemImage, 1f);
        if (button.transform.childCount > 0)
        {
            GameObject chekMark = button.transform.GetChild(0).gameObject;
            if (selectedItemForSell.Contains(item))
            {
                selectedItemForSell.Remove(item);
                countSelectedItem--;
            }
            Destroy(chekMark);
        }

    }

}
