#region Summary
/// <summary>
/// TrainingManager is a singleton class responsible for managing the training of troops in the game.
/// It maintains a queue of training jobs, each representing a batch of troops being trained.
/// The manager checks for training completion in the Update loop and handles resource costs and building requirements for starting new training jobs.
/// Key functionalities include:
/// 1. Validating if a new training job can be started based on queue capacity, building requirements, and resource availability.
/// 2. Starting a new training job by deducting the necessary resources and adding the job to the queue.
/// 3. Completing training jobs when their completion time is reached, which involves adding the trained troops to the player's inventory and removing the job from the queue.
/// The class interacts with other game systems such as ResourceManager for handling resources, BuildingManager for checking building requirements, and TroopInventory for updating the player's troop counts.
/// Example usage:
/// - A player initiates training of 10 Archers. The TrainingManager checks if the player has the required Barracks building and enough resources.
///     If valid, it starts the training job and deducts resources. Once the training time elapses, the Archers are added to the player's inventory.
/// Note: The TrainingManager relies on the TroopConfig class to define the properties of each troop type, including training costs and required buildings.
///     This allows for flexible configuration of different troop types and their training requirements.
/// </summary>
#endregion
#region Phase 1 Sprint 5 - Training Manager Class
using UnityEngine;
using System;
using System.Collections.Generic;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager Instance;

    private const int maxQueueSize = 2;

    private Queue<TrainingJob> trainingQueue = new Queue<TrainingJob>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (trainingQueue.Count == 0) return;

        TrainingJob current = trainingQueue.Peek();

        if (DateTime.UtcNow >= current.completionTime)
            CompleteCurrentJob();
    }

    public bool CanTrain(TroopConfig config, int quantity)
    {
        if (quantity <= 0)
        {
            Debug.Log("Quantity must be greater than zero.");
            return false;
        }

        if (trainingQueue.Count >= maxQueueSize)
        {
            Debug.Log("Training queue is full.");
            return false;
        }

        if (!HasRequiredBuilding(config.requiredBuilding))
        {
            Debug.Log("Required building not placed: " + config.requiredBuilding);
            return false;
        }

        ResourceCost totalCost = GetTotalCost(config, quantity);

        if (!ResourceManager.Instance.CanAfford(totalCost))
        {
            Debug.Log("Not enough resources to train " + quantity + " " + config.troopName);
            return false;
        }

        return true;
    }

    public bool StartTraining(TroopConfig config, int quantity)
    {
        if (!CanTrain(config, quantity)) return false;

        ResourceCost totalCost = GetTotalCost(config, quantity);
        ResourceManager.Instance.Deduct(totalCost);

        TrainingJob job = new TrainingJob(config, quantity);
        trainingQueue.Enqueue(job);

        Debug.Log("Training started: " + quantity + " " + config.troopName
            + ". Completes at: " + job.completionTime.ToLocalTime());

        return true;
    }

    private void CompleteCurrentJob()
    {
        TrainingJob job = trainingQueue.Dequeue();
        TroopInventory.Instance.Add(job.config.troopType, job.quantity);
        Debug.Log("Training complete: " + job.quantity + " " + job.config.troopName);
    }

    private bool HasRequiredBuilding(BuildingType buildingType)
    {
        foreach (BuildingState b in BuildingManager.Instance.GetAllBuildings())
        {
            if (b.config.buildingType == buildingType)
                return true;
        }
        return false;
    }

    private ResourceCost GetTotalCost(TroopConfig config, int quantity)
    {
        return new ResourceCost
        {
            food = config.trainingCostPerUnit.food * quantity,
            wood = config.trainingCostPerUnit.wood * quantity,
            stone = config.trainingCostPerUnit.stone * quantity,
            gold = config.trainingCostPerUnit.gold * quantity
        };
    }
}
#endregion