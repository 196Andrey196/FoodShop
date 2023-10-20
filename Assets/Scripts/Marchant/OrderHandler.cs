using System.Collections.Generic;
using UnityEngine;


public class OrderHandler : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private AudioClip _moneyIWallet;
    public void VerifyOrder(ItemsBoard itemBoard, GameObject curentBuyer)
    {
        Union _union = transform.GetChild(0).GetComponent<Union>();
        UnionItemMarks unionItemMarks = transform.GetChild(0).GetComponent<UnionItemMarks>();
        List<Item> selectedItems = itemBoard.selectedItemForSell;
        OrderGenerator orderGenerator = curentBuyer.GetComponent<OrderGenerator>();
        List<Item> buyerOrderList = orderGenerator.orderItemList;

        if (selectedItems.Count == buyerOrderList.Count && selectedItems.Count != 0 && buyerOrderList.Count != 0)
        {
            bool[] itemStatusArray = new bool[selectedItems.Count];
            for (int i = 0; i < selectedItems.Count; i++)
            {
                {
                    _union.AddImageInUnion(selectedItems[i].image);
                    _union.gameObject.SetActive(true);
                    if (buyerOrderList.Contains(selectedItems[i]))
                    {
                        unionItemMarks.SetMatrkStatus(true);
                        itemStatusArray[i] = true;
                    }
                    else
                    {
                        unionItemMarks.SetMatrkStatus(false);
                        itemStatusArray[i] = false;
                    }
                }

            }

            PaymentHandler buyerPay = curentBuyer.GetComponent<PaymentHandler>();
            buyerPay.payItems = true;
            unionItemMarks.buyerPaymentHandler = buyerPay;
            if (buyerPay.GetMoneyForItem(itemStatusArray) != 0)
            {
                SoundManager.instance.PlaySound(_moneyIWallet);
                _wallet.money += buyerPay.GetMoneyForItem(itemStatusArray);
            }

        }
    }
}
