#region Summary
/// <summary>
/// TrainingJob represents an individual training task for a specific type of troop in the game.
/// It includes the configuration of the troop being trained, the quantity of troops being trained, and the expected completion time for the training job.
/// When a new TrainingJob is created, it calculates the completion time based on the current time and the total training time required for the specified quantity of troops.
/// This allows the game to track when the training will be completed and update the troop counts accordingly.
/// Example usage:
/// 1. When a player initiates a training job for a certain type of troop, a new TrainingJob instance is created with the corresponding TroopConfig and quantity.
/// 2. The game can periodically check the completionTime of each TrainingJob to determine if the training is complete and update the player's troop counts accordingly.
/// 3. The TrainingJob can be stored in a list or queue to manage multiple training tasks simultaneously, allowing for complex training strategies and scheduling within the game.
/// Note: The TrainingJob class relies on the TroopConfig class to provide the necessary information about the troop being trained, such as the training time per unit. 
///     This allows for flexibility and scalability in managing different types of troops and their training requirements.
/// </summary>
#endregion
#region Phase 1 Sprint 5 - Training Job Class
using System;

[Serializable]
public class TrainingJob
{
    public TroopConfig config;
    public int quantity;
    public DateTime completionTime;

    public TrainingJob(TroopConfig config, int quantity)
    {
        this.config = config;
        this.quantity = quantity;
        float totalSeconds = config.trainingTimeSecondsPerUnit * quantity;
        completionTime = DateTime.UtcNow.AddSeconds(totalSeconds);
    }
}
#endregion