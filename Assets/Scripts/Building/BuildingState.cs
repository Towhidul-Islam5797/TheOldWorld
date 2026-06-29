#region Summary
/// <summary>
/// BuildingState represents the current state of a building in the game, including its configuration, level, upgrade status, and position on the map.
/// It provides methods to check if the building can be upgraded, to start an upgrade, and to check if an upgrade has been completed.
/// This class is essential for managing the lifecycle of buildings in the game, allowing for dynamic interactions such as upgrading and tracking the building's progress.
/// Example usage:
/// 1. Create a new BuildingState for a farm building at tile coordinates (5, 10) using a BuildingConfig for a farm.
/// 2. Check if the building can be upgraded based on the current HQ level and upgrade status.
/// 3. If it can be upgraded, call StartUpgrade() to begin the upgrade process.
/// 4. Periodically call CheckUpgradeComplete() to see if the upgrade has finished and to update the building's level accordingly.
/// This design encapsulates the building's state and behavior, making it easier to manage and extend in the future as new building types and upgrade mechanics are added to the game.
/// Note: This class relies on the BuildingConfig class for configuration data and assumes that BuildingType is an enum defined elsewhere in the codebase.
/// </summary>
#endregion

#region Phase 1 Sprint 3 - Building State
//using UnityEngine;
//public class BuildingState
//{
//    public BuildingConfig config;
//    public int level;
//    public bool isUpgrading;
//    public float upgradeEndTime;
//    public int tileX;
//    public int tileY;

//    public BuildingState(BuildingConfig config, int x, int y)
//    {
//        this.config = config;
//        level = 1;
//        tileX = x;
//        tileY = y;
//    }

//    public bool CanUpgrade(int hqLevel)
//    {
//        if (isUpgrading) return false;
//        if (level >= config.maxLevel) return false;
//        if (config.buildingType != BuildingType.HQ && level >= hqLevel) return false;
//        return true;
//    }

//    public void StartUpgrade()
//    {
//        isUpgrading = true;
//        upgradeEndTime = UnityEngine.Time.time + config.upgradeTimeSeconds * level;
//    }

//    public void CheckUpgradeComplete()
//    {
//        if (!isUpgrading) return;
//        if (UnityEngine.Time.time >= upgradeEndTime)
//        {
//            level++;
//            isUpgrading = false;
//            UnityEngine.Debug.Log(config.buildingName + " upgrade complete. Now level " + level);
//        }
//    }
//}
#endregion

#region Phase 2 Sprint 1 - Building State With Per-Level Data
//using UnityEngine;
 
//public class BuildingState
//{
//    public BuildingConfig config;
//    public int level;
//    public bool isUpgrading;
//    public float upgradeEndTime;
//    public int tileX;
//    public int tileY;

//    public BuildingState(BuildingConfig config, int x, int y)
//    {
//        this.config = config;
//        level = 1;
//        tileX = x;
//        tileY = y;
//    }

//    public bool CanUpgrade(int hqLevel)
//    {
//        if (isUpgrading) return false;
//        if (level >= config.maxLevel) return false;
//        if (config.buildingType != BuildingType.HQ && level >= hqLevel) return false;
//        return true;
//    }

//    public void StartUpgrade()
//    {
//        isUpgrading = true;
//        float upgradeTime = config.GetLevel(level).upgradeTimeSeconds;
//        upgradeEndTime = Time.time + upgradeTime;
//    }

//    public void CheckUpgradeComplete()
//    {
//        if (!isUpgrading) return;
//        if (Time.time >= upgradeEndTime)
//        {
//            level++;
//            isUpgrading = false;
//            Debug.Log(config.buildingName + " upgrade complete. Now level " + level);
//        }
//    }
//}
#endregion

#region Phase 2 Sprint 3 - Building State With Cancel Upgrade
using UnityEngine;
public class BuildingState
{
    public BuildingConfig config;
    public int level;
    public bool isUpgrading;
    public float upgradeEndTime;
    public int tileX;
    public int tileY;

    public BuildingState(BuildingConfig config, int x, int y)
    {
        this.config = config;
        level = 1;
        tileX = x;
        tileY = y;
    }

    public bool CanUpgrade(int hqLevel)
    {
        if (isUpgrading) return false;
        if (level >= config.maxLevel) return false;
        if (config.buildingType != BuildingType.HQ && level >= hqLevel) return false;
        return true;
    }

    public void StartUpgrade()
    {
        isUpgrading = true;
        float upgradeTime = config.GetLevel(level).upgradeTimeSeconds;
        upgradeEndTime = Time.time + upgradeTime;
    }

    public void CancelUpgrade()
    {
        isUpgrading = false;
        upgradeEndTime = 0f;
    }

    public void CheckUpgradeComplete()
    {
        if (!isUpgrading) return;
        if (Time.time >= upgradeEndTime)
        {
            level++;
            isUpgrading = false;
            Debug.Log(config.buildingName + " upgrade complete. Now level " + level);
        }
    }
}
#endregion