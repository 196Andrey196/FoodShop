using UnityEngine;
using System;
public class EndPoint : MonoBehaviour
{
    [SerializeField] private GameObject _buyer;
    public event Action OnBuyerReachedEndPoint;
    public event Action<GameObject> OnCurentBuyer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Buyer"))
        {
            _buyer = other.gameObject;
            OnBuyerReachedEndPoint?.Invoke();
            OnCurentBuyer?.Invoke(_buyer);
        }

    }
}
