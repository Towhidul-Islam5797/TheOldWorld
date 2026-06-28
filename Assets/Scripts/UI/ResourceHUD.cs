#region Summary
/// ResourceHUD displays all 5 resources live on screen.
/// Subscribes to ResourceManager.OnResourceChanged and updates text fields.
/// Gold shows 0 until Phase 4 VIP system is implemented.
#endregion

#region Phase 2 Sprint 2 - Resource HUD With Gold
using UnityEngine;
using TMPro;

public class ResourceHUD : MonoBehaviour
{
    [Header("Resource Text Fields")]
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI silverText;
    [SerializeField] private TextMeshProUGUI goldText;

    void Start()
    {
        if (ResourceManager.Instance == null)
        {
            Debug.LogError("ResourceHUD: ResourceManager.Instance is null");
            return;
        }

        ResourceManager.Instance.OnResourceChanged += UpdateHUD;
        UpdateHUD();
    }

    void OnDestroy()
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= UpdateHUD;
    }

    void UpdateHUD()
    {
        foodText.text = Mathf.FloorToInt(ResourceManager.Instance.food).ToString();
        woodText.text = Mathf.FloorToInt(ResourceManager.Instance.wood).ToString();
        stoneText.text = Mathf.FloorToInt(ResourceManager.Instance.stone).ToString();
        silverText.text = Mathf.FloorToInt(ResourceManager.Instance.silver).ToString();
        goldText.text = Mathf.FloorToInt(ResourceManager.Instance.gold).ToString();
    }
}
#endregion