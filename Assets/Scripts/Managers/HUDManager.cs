#region Summary
/// HUDManager is the single controller for all UI panels in the game.
/// It shows and hides panels and ensures only one panel is open at a time.
/// All buttons call HUDManager methods via the Inspector OnClick events.
#endregion

#region Phase 2 Sprint 2 - HUD Manager
//using UnityEngine;

//public class HUDManager : MonoBehaviour
//{
//    public static HUDManager Instance;

//    [Header("Always Visible")]
//    [SerializeField] private GameObject resourcePanel;
//    [SerializeField] private GameObject utilityButtonsPanel;

//    [Header("Context Panels")]
//    [SerializeField] private GameObject panelSettings;
//    [SerializeField] private GameObject panelProfile;
//    [SerializeField] private GameObject panelMap;
//    [SerializeField] private GameObject panelMail;
//    [SerializeField] private GameObject panelRankings;
//    [SerializeField] private GameObject panelEvents;
//    [SerializeField] private GameObject panelQuests;

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        Show(resourcePanel);
//        Show(utilityButtonsPanel);
//        CloseAll();
//    }

//    public void OpenSettings() { OpenPanel(panelSettings); }
//    public void OpenProfile() { OpenPanel(panelProfile); }
//    public void OpenMap() { OpenPanel(panelMap); }
//    public void OpenMail() { OpenPanel(panelMail); }
//    public void OpenRankings() { OpenPanel(panelRankings); }
//    public void OpenEvents() { OpenPanel(panelEvents); }
//    public void OpenQuests() { OpenPanel(panelQuests); }

//    public void CloseAll()
//    {
//        Hide(panelSettings);
//        Hide(panelProfile);
//        Hide(panelMap);
//        Hide(panelMail);
//        Hide(panelRankings);
//        Hide(panelEvents);
//        Hide(panelQuests);
//    }

//    private void OpenPanel(GameObject panel)
//    {
//        CloseAll();
//        Show(panel);
//    }

//    private void Show(GameObject panel)
//    {
//        if (panel != null) panel.SetActive(true);
//    }

//    private void Hide(GameObject panel)
//    {
//        if (panel != null) panel.SetActive(false);
//    }
//}
#endregion

#region Phase 2 Sprint 5 - HUD Manager With Training Panel
using UnityEngine;
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    [Header("Always Visible")]
    [SerializeField] private GameObject resourcePanel;
    [SerializeField] private GameObject utilityButtonsPanel;
    [Header("Context Panels")]
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelProfile;
    [SerializeField] private GameObject panelMap;
    [SerializeField] private GameObject panelMail;
    [SerializeField] private GameObject panelRankings;
    [SerializeField] private GameObject panelEvents;
    [SerializeField] private GameObject panelQuests;
    [SerializeField] private GameObject panelTraining;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Show(resourcePanel);
        Show(utilityButtonsPanel);
        CloseAll();
    }

    public void OpenSettings() { OpenPanel(panelSettings); }
    public void OpenProfile() { OpenPanel(panelProfile); }
    public void OpenMap() { OpenPanel(panelMap); }
    public void OpenMail() { OpenPanel(panelMail); }
    public void OpenRankings() { OpenPanel(panelRankings); }
    public void OpenEvents() { OpenPanel(panelEvents); }
    public void OpenQuests() { OpenPanel(panelQuests); }
    public void OpenTraining() { OpenPanel(panelTraining); }

    public void CloseAll()
    {
        Hide(panelSettings);
        Hide(panelProfile);
        Hide(panelMap);
        Hide(panelMail);
        Hide(panelRankings);
        Hide(panelEvents);
        Hide(panelQuests);
        Hide(panelTraining);
    }

    private void OpenPanel(GameObject panel)
    {
        CloseAll();
        Show(panel);
    }

    private void Show(GameObject panel)
    {
        if (panel != null) panel.SetActive(true);
    }

    private void Hide(GameObject panel)
    {
        if (panel != null) panel.SetActive(false);
    }
}
#endregion