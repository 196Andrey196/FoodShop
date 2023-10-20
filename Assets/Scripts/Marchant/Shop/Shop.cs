using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] private EndPoint _endPoint;
    [SerializeField] private GameObject _curentBuyer;
    public GameObject curentBuyer { get { return _curentBuyer; } }
    [SerializeField] private OrderHandler _orderHandler;
    public EndPoint endPoint { get { return _endPoint; } }
    [SerializeField] private ItemsBoard _itemBoard;
    [SerializeField] Button _sellButton;
    public event Action OnButtonSell;
    [SerializeField] private AudioClip _clickButton;

    private void Start()
    {
        _itemBoard = transform.GetChild(1).GetComponent<ItemsBoard>();
        _sellButton.onClick.AddListener(() => PressButtonSell(_itemBoard));
    }
    private void Update()
    {
        _itemBoard.ActiveSellButton(_sellButton);
    }

    private void PressButtonSell(ItemsBoard itemBoard)
    {
        SoundManager.instance.PlaySound(_clickButton);
        _orderHandler.VerifyOrder(itemBoard, _curentBuyer);
        _itemBoard.countItemInOreder = 0;
        OnButtonSell?.Invoke();
    }

    public void SetCurentBuyer(GameObject buyer)
    {
        _curentBuyer = buyer;
        _itemBoard.countItemInOreder = buyer.GetComponent<OrderGenerator>().countItemOfOrder;
    }

}
