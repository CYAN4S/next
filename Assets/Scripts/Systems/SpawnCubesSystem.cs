using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class SpawnCubesSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubesConfig>();
    }

    protected override void OnUpdate()
    {
        Enabled = false; // Run only once.
        var config = SystemAPI.GetSingleton<SpawnCubesConfig>();

        for (int i = 0; i < config.amountToSpawn; i++)
        {
            var spawned = EntityManager.Instantiate(config.cubePrefabEntity);
        }
    }
}