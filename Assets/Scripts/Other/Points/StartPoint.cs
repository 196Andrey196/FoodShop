using System.Collections;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private GameObject _buyerPrefab;
    [SerializeField] private bool _buyerPaid;
    private void Start()
    {
        StartCoroutine(SpawnNewBuyer(_buyerPrefab));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Buyer"))
        {
            _buyerPaid = other.GetComponent<PaymentHandler>().payItems;
            if (_buyerPaid)
                StartCoroutine(SpawnNewBuyer(other.gameObject));

        }
    }

    private IEnumerator SpawnNewBuyer(GameObject curentBuyer)
    {
        if (_buyerPaid)
            Destroy(curentBuyer);
        yield return new WaitForSeconds(1);
        Instantiate(_buyerPrefab, transform.position, Quaternion.identity);
    }
}
