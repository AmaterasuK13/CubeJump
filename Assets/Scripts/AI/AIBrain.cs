using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    private AIMovementController _movementController;
    private AIDecisionController _decisionController;

    private List<IAIController> _controllers = new List<IAIController>();

    private FieldGenerator _fieldGenerator;

    private void Awake()
    {
        _movementController = new AIMovementController(transform);
        _controllers.Add(_movementController);
        _decisionController = new AIDecisionController(1);
        _controllers.Add(_decisionController);

        _decisionController.DecisionMade += DecisionMade;
        _movementController.IsSuccess += DecisionRecall;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(0, 1, _movementController.CurrentCube.Position));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(-1, 0, _movementController.CurrentCube.Position));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(1, 0, _movementController.CurrentCube.Position));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(0, -1, _movementController.CurrentCube.Position));
        }

        foreach (var controller in _controllers)
        {
            if (controller != null)
            {
                controller.Tick();
            }
        }
    }

    private void DecisionRecall(bool isSuccess)
    {
        _decisionController.DecisionRecall(isSuccess);
    }

    private void DecisionMade(byte decision)
    {
        if (decision == 1)
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(0, 1, _movementController.CurrentCube.Position));
        }
        else if (decision == 2)
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(1, 0, _movementController.CurrentCube.Position));
        }
        else if (decision == 3)
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(-1, 0, _movementController.CurrentCube.Position));
        }
        else if (decision == 4)
        {
            _movementController.SetPosition(_fieldGenerator.GetNextCube(0, -1, _movementController.CurrentCube.Position));
        }
        else if (decision == 5)
        {
            return;
        }
    }

    public void Init(FieldGenerator field)
    {
        _fieldGenerator = field;
        _movementController.SetPosition(_fieldGenerator.GetStartPosCube());
    }
}
