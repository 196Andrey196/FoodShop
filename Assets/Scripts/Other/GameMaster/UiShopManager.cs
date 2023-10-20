using System.Collections;
using UnityEngine;

public class UiShopManager : MonoBehaviour
{
    [SerializeField] private float _timeForActiveUnion = 1f;
    [SerializeField] private float _timeForActiveShop = 5f;
    [SerializeField] private GameObject _playerUnion;
    [SerializeField] private Shop _shopPanel;
    Animator _shopPanelAnimastor;


    private void Start()
    {
        _shopPanel.endPoint.OnBuyerReachedEndPoint += HandleBuyerReachedEndPoint;
        _shopPanel.endPoint.OnCurentBuyer += _shopPanel.SetCurentBuyer;
        _shopPanel.OnButtonSell += ActionForBuyer;
        _shopPanelAnimastor = _shopPanel.gameObject.GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        _shopPanel.endPoint.OnBuyerReachedEndPoint -= HandleBuyerReachedEndPoint;
        _shopPanel.endPoint.OnCurentBuyer -= _shopPanel.SetCurentBuyer;
        _shopPanel.OnButtonSell -= ActionForBuyer;
    }
    ///////////////////////Buyer
    public IEnumerator ActiveUiForBuyer()
    {
        yield return new WaitForSeconds(_timeForActiveUnion);
        Transform buyerUnion = _shopPanel.curentBuyer.transform.GetChild(0);
        buyerUnion.gameObject.SetActive(true);
        yield return new WaitForSeconds(_timeForActiveShop);
        buyerUnion.gameObject.SetActive(false);
        Transform[] items = new Transform[buyerUnion.transform.GetChild(0).GetChild(0).childCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = buyerUnion.transform.GetChild(0).GetChild(0).GetChild(i);
            Destroy(items[i].gameObject);
        }
        _shopPanel.gameObject.SetActive(true);
    }
    private void HandleBuyerReachedEndPoint()
    {
        StartCoroutine(ActiveUiForBuyer());
    }
    ///////////////////////Player
    public IEnumerator ActiveUiForPlayer()
    {
        yield return new WaitForSeconds(_timeForActiveUnion);
        _playerUnion.SetActive(true);
        _shopPanelAnimastor.SetTrigger("HidePanel");
        yield return new WaitForSeconds(5);
        _shopPanel.gameObject.SetActive(false);
    }
    private void ActionForBuyer()
    {
        StartCoroutine(ActiveUiForPlayer());
    }


}
