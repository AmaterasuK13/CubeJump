using System;
using UnityEngine;

public class AIMovementController : IAIController
{
    private Transform _aiTransform;
    public Cube CurrentCube {  get; private set; }

    public event Action<bool> IsSuccess;

    public AIMovementController(Transform aiTransform)
    {
        _aiTransform = aiTransform;
    }

    public void SetPosition(Cube cube)
    {
        if (cube == null)
        {
            IsSuccess?.Invoke(false);
            return;
        }

        if (cube.IsBlocked)
        {
            IsSuccess?.Invoke(false);
            return;
        }

        _aiTransform.position = cube.Position + Vector3.up;
        CurrentCube = cube;
        IsSuccess?.Invoke(true);
    }

    public void Tick()
    {
        
    }
}
