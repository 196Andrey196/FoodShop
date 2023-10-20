using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money;
    public int money { set { if (value != 0) _money = value; } get { return _money; } }
    [SerializeField] private TextMeshProUGUI _wallet;

    private void Start()
    {
        _money = PlayerPrefs.GetInt("money",0);
    }
    void Update()
    {
        PlayerPrefs.SetInt("money",_money);
        _wallet.text = "$ " + _money.ToString();
    }

}
