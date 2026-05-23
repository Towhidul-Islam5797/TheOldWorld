#region Summary
/// <summary>
/// CombatResolver is a static class that provides functionality to resolve combat encounters between player troops and NPC bandit camps in the game.
///   It calculates the outcome of a battle based on the number and types of troops sent by the player, the combat power of the bandit camp, and predefined counter multipliers that affect the effectiveness of different troop types against each other.
///   The Resolve method returns a BattleReport that details the results of the combat, including whether the player won and how many troops were lost in the encounter.
/// Example usage:
/// 1. When a player initiates an attack on a bandit camp, the AttackCamp method in BanditCampManager calls CombatResolver.Resolve, passing in the number of each troop type sent and the BanditCamp being attacked.
/// 2. CombatResolver calculates the player's total attack power, applies counter multipliers based on troop types, and compares it to the camp's combat power to determine the outcome of the battle.
/// 3. The resulting BattleReport is used to update the player's troop inventory (deducting lost troops) and to determine the loot awarded if the player wins.
/// Note: The combat resolution logic is designed to be simple and scalable, allowing for easy adjustments to troop stats, counter multipliers, and loss calculations as needed for game balance.
/// </summary>
#endregion

#region Milestone 1 Sprint 10 - Combat Resolver
// Resolves a battle between player troops and an NPC camp.
// Formula: Damage = Attack - Defense, modified by counter multipliers.
// Counter multipliers confirmed by client (from tracker sheet):
//   Infantry vs Cavalry: 1.35   Cavalry vs Archers: 1.35
//   Archers  vs Infantry: 1.35  Siege vs all: 0.80 (glass cannon)
//   Traps    vs all: 1.50       Traps vs Siege: 0.50

using UnityEngine;
public static class CombatResolver
{
    // Returns a multiplier for attacker type vs defender type.
    // Defender type is passed as a string since NPC camps don't use TroopType.
    public static float GetCounterMultiplier(TroopType attacker, string defenderType)
    {
        switch (attacker)
        {
            case TroopType.Infantry:
                return defenderType == "Cavalry" ? 1.35f : 1.00f;
            case TroopType.Archers:
                return defenderType == "Infantry" ? 1.35f : 1.00f;
            case TroopType.Cavalry:
                return defenderType == "Archers" ? 1.35f : 0.90f;
            case TroopType.Siege:
                return 0.80f; // Siege is broad damage, lower survivability
            default:
                return 1.00f;
        }
    }

    public static BattleReport Resolve(
        int infantry, int archers, int cavalry, int siege,
        BanditCamp camp)
    {
        BattleReport report = new BattleReport();
        report.campLevel = camp.level;
        report.infantrySent = infantry;
        report.archersSent = archers;
        report.cavalrySent = cavalry;
        report.siegeSent = siege;

        // Calculate total player attack power with counter multipliers
        // NPC camps are treated as a mixed "Ranged" defender type for simplicity
        float playerPower =
            (infantry * (8f + 0f) * GetCounterMultiplier(TroopType.Infantry, "Ranged")) +
            (archers * (12f + 0f) * GetCounterMultiplier(TroopType.Archers, "Ranged")) +
            (cavalry * (14f + 0f) * GetCounterMultiplier(TroopType.Cavalry, "Ranged")) +
            (siege * (18f + 0f) * GetCounterMultiplier(TroopType.Siege, "Ranged"));

        float campPower = camp.power;

        float ratio = campPower > 0 ? playerPower / campPower : 1f;
        report.playerWon = ratio >= 1f;

        // Loss calculation — higher ratio = fewer losses
        // At ratio 1.0 player loses ~30%, at ratio 2.0 loses ~5%
        float lossRate = Mathf.Clamp(1f - (ratio * 0.5f), 0.05f, 0.95f);

        report.infantryLost = Mathf.RoundToInt(infantry * lossRate);
        report.archersLost = Mathf.RoundToInt(archers * lossRate);
        report.cavalryLost = Mathf.RoundToInt(cavalry * lossRate);
        report.siegeLost = Mathf.RoundToInt(siege * lossRate);

        return report;
    }
}
#endregion