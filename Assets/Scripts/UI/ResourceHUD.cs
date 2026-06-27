#region Summary
/// ResourceHUD listens to ResourceManager.OnResourceChanged and updates
/// the four resource text fields on screen whenever resources change.
#endregion

#region Phase 2 Sprint 2 - Resource HUD
using UnityEngine;
using TMPro;

public class ResourceHUD : MonoBehaviour
{
    [Header("Resource Text Fields")]
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI silverText;

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
    }
}
#endregion