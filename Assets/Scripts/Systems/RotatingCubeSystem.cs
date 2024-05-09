using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public partial struct RotatingCubeSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        /* When not using Job
        foreach (var (localTransform, rotateSpeed)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
        {
            localTransform.ValueRW =
                localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
        */

        var rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotatingCubeJob.ScheduleParallel();
    }
    
    [BurstCompile]
    [WithNone(typeof(Player))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        public float deltaTime;
        
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime);
        }
    }
}