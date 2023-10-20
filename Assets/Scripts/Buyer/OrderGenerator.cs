using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField] private ItemsBoard _itemsBoard;
    [SerializeField] private int _maxCountItem = 4;
    [SerializeField] private int _countItemForOreder;
    public int countItemOfOrder { get { return _countItemForOreder; } }
    [SerializeField] private Union _union;
    [SerializeField] private List<Item> _orderItemList;
    public List<Item> orderItemList { get { return _orderItemList; } }
    public bool payedOrder = false;
    private void Start()
    {
        _orderItemList = new List<Item>();
        CreatOrder();
    }
    private void CreatOrder()
    {
        _countItemForOreder = ItemRandomiser(1, _maxCountItem);
        if (_itemsBoard)
        {
            List<Item> itemList = _itemsBoard.itemsList;
            if (itemList != null)
            {
                for (int i = 0; i < _countItemForOreder; i++)
                {
                    int randomItemIndex;
                    do
                    {
                        randomItemIndex = ItemRandomiser(0, itemList.Count);
                    }
                    while (ChekingADuplicateItem(itemList[randomItemIndex]));

                    _orderItemList.Add(itemList[randomItemIndex]);
                    _union.AddImageInUnion(itemList[randomItemIndex].image);
                }
            }
        }

    }
    private int ItemRandomiser(int minValue, int maxValue)
    {
        return Random.Range(minValue, maxValue);
    }
    private bool ChekingADuplicateItem(Item item)
    {
        bool result = false;
        foreach (Item orderItem in _orderItemList)
        {
            if (item.id == orderItem.id)
            {
                result = true;
            }
        }
        return result;

    }
}

