#region Summary
/// This script manages the interaction between the BuildingPlacer and BuildingPopup components.
/// It handles the selection and deselection of buildings, as well as the display of the building popup.
/// It ensures that only one building popup is visible at a time and updates the popup based on the currently selected building.
/// Example usage:
/// - When a building is selected, the BuildingInteraction updates the BuildingPopup to show the building's details.
/// - When a building is deselected, the BuildingInteraction hides the BuildingPopup.
#endregion
#region Phase 2 Sprint 3 - Building Interaction Manager
//using UnityEngine;

//// Sits between BuildingPlacer and BuildingPopup.
//// BuildingPlacer tells this what was tapped.
//// This decides whether to open, close, or swap the popup.
//public class BuildingInteraction : MonoBehaviour
//{
//    public static BuildingInteraction Instance;

//    private BuildingState selectedBuilding;

//    void Awake()
//    {
//        Instance = this;
//    }

//    public void SelectBuilding(BuildingState building)
//    {
//        if (building == null)
//        {
//            Deselect();
//            return;
//        }

//        selectedBuilding = building;
//        Vector3 worldPos = BuildingManager.Instance.GetBuildingWorldPosition(building);
//        BuildingPopup.Instance.Show(building, worldPos);
//    }

//    public void Deselect()
//    {
//        selectedBuilding = null;
//        BuildingPopup.Instance.Hide();
//    }

//    public BuildingState GetSelected()
//    {
//        return selectedBuilding;
//    }
//}
#endregion
#region Phase 2 Sprint 5 - Building Interaction Manager
using UnityEngine;

public class BuildingInteraction : MonoBehaviour
{
    public static BuildingInteraction Instance;

    private BuildingState selectedBuilding;

    void Awake()
    {
        Instance = this;
    }

    public void SelectBuilding(BuildingState building)
    {
        if (building == null)
        {
            Deselect();
            return;
        }

        selectedBuilding = building;
        Vector3 worldPos = BuildingManager.Instance.GetBuildingWorldPosition(building);
        BuildingPopup.Instance.Show(building, worldPos);
    }

    public void Deselect()
    {
        selectedBuilding = null;
        BuildingPopup.Instance.Hide();
        TrainingPopup.Instance.Hide();
    }

    public BuildingState GetSelected()
    {
        return selectedBuilding;
    }
}
#endregion