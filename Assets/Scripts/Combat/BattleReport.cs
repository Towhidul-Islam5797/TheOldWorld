#region Summary
/// <summary>
/// BattleReport is a class that encapsulates the results of a combat encounter between the player and a bandit camp in the game.
///   It contains information about the outcome of the battle (win or lose), the level of the camp, the number of each troop type sent and lost by the player, and the resources and crafting materials looted from the camp if the player wins.
/// Example usage:
/// 1. After a combat encounter is resolved by the CombatResolver, a BattleReport instance is created to store the details of the battle outcome.
/// 2. The BattleReport can be used to update the player's troop inventory (deducting lost troops) and to award resources and materials if the player won the battle.
/// 3. The BattleReport can also be logged or displayed to the player to provide feedback on the results of their combat encounter, including what they lost and what they gained from the battle.
/// Note: The BattleReport serves as a comprehensive record of a combat encounter, allowing for easy access to all relevant information about the battle's outcome and its impact on the player's resources and troops.
/// </summary>
#endregion

#region Milestone 1 Sprint 10 - Battle Report
using System.Collections.Generic;
[System.Serializable]
public class BattleReport
{
    public bool playerWon;
    public int campLevel;

    public int infantrySent;
    public int archersSent;
    public int cavalrySent;
    public int siegeSent;

    public int infantryLost;
    public int archersLost;
    public int cavalryLost;
    public int siegeLost;

    // Resource loot dropped by the camp
    public float foodLooted;
    public float woodLooted;
    public float stoneLooted;
    public float silverLooted;

    // Crafting material loot — material name and quantity
    public Dictionary<string, int> materialsLooted = new Dictionary<string, int>();

    public override string ToString()
    {
        string result = playerWon ? "VICTORY" : "DEFEAT";
        return string.Format(
            "[BattleReport] {0} vs Lvl{1} camp | Lost: Inf{2} Arch{3} Cav{4} Siege{5} | " +
            "Loot: Food{6} Wood{7} Stone{8} Silver{9}",
            result, campLevel,
            infantryLost, archersLost, cavalryLost, siegeLost,
            foodLooted, woodLooted, stoneLooted, silverLooted
        );
    }
}
#endregion