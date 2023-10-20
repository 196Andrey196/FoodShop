using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetPause : MonoBehaviour
{
    [SerializeField] private GameObject _menuSettings;
    [SerializeField] private Button _button;
    [SerializeField] private bool isMenuActive = false;
     [SerializeField] private AudioClip _clickButton;
    void Start()
    {
        _button.onClick.AddListener(() => ShowMenuSettings());
        var eventTrigger = _menuSettings.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        entry.callback.AddListener((data) => CloseMenuIfActive());
        eventTrigger.triggers.Add(entry);
    }

    private void ShowMenuSettings()
    {
        SoundManager.instance.PlaySound(_clickButton);
        _menuSettings.SetActive(true);
        isMenuActive = true;
        Time.timeScale = 0f;
    }
    private void CloseMenuIfActive()
    {
        if (isMenuActive)
        {
            _menuSettings.SetActive(false);
            isMenuActive = false;
            Time.timeScale = 1f;
        }
    }
}
