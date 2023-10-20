using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PaymentHandler : MonoBehaviour
{
    [SerializeField] private Sprite _isSatisfied;
    [SerializeField] private Sprite _isDissatisfied;
    [SerializeField] Union _union;
    [SerializeField] private bool _allItemList = false;
    private bool reactionShown = false;

    public bool payItems = false;


    public int GetMoneyForItem(bool[] selectProducts)
    {
        return PaymentAndReaction(selectProducts);
    }
    private int PaymentAndReaction(bool[] selectProducts)
    {
        int moneyToPay = 0;
        foreach (var item in selectProducts)
        {

            if (item == true)
            {
                moneyToPay += 10;
            }

        }
        if (selectProducts.All(item => item == true))
        {
            moneyToPay *= 2;
            _allItemList = true;
        }
        else
        {
            _allItemList = false;
        }
        return moneyToPay;
    }

    public IEnumerator SetReactionAndGo()
    {
        WayPointMove _wayPointMove = GetComponent<WayPointMove>();
        _union.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (!reactionShown)
        {
            if (_allItemList)
                _union.AddImageInUnion(_isSatisfied);
            else
                _union.AddImageInUnion(_isDissatisfied);


            reactionShown = true;
        }
        if (payItems)
            _wayPointMove.GoBack();
        yield return new WaitForSeconds(3);
        _union.gameObject.SetActive(false);
    }

}
