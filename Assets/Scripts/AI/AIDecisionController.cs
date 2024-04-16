using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AIDecisionController : IAIController
{
    private List<byte> _moves = new List<byte>();
    private float _decisionCooldown; 
    private float _currentDecisionTime;

    private byte _lastDecision;
    private byte _currentDirPriority = 2;
    private bool _isPrioritySwitched = false;

    public event Action<byte> DecisionMade;

    public AIDecisionController(float decisionCooldown)
    {
        _decisionCooldown = decisionCooldown;
        _currentDecisionTime = decisionCooldown;
    }

    public void Tick()
    {
        _currentDecisionTime -= Time.deltaTime;
        if (_currentDecisionTime < 0) 
        {
            _currentDecisionTime = _decisionCooldown;
            MakeDecision(1);
        }
    }

    public void DecisionRecall(bool isSucceed)
    {
        if (isSucceed)
        {
            _moves.Add(_lastDecision);
            _isPrioritySwitched = false;
        }
        else 
        {
            Debug.Log("New Decision");

            if ( _lastDecision == 1)
            {
                byte decision = _currentDirPriority;
                _lastDecision = decision;

                DecisionMade?.Invoke(decision);
                return;
            }
            
            if ((_lastDecision == 2 || _lastDecision == 3) && !_isPrioritySwitched)
            {
                _currentDirPriority = _currentDirPriority == 3 ? (byte)2 : (byte)3;
                byte decision = _currentDirPriority;
                _isPrioritySwitched = true;
                _lastDecision = decision;

                DecisionMade?.Invoke(decision);
                return;
            }

            if (_lastDecision == 4)
            {
                byte decision = 5;
                _lastDecision = decision;

                DecisionMade?.Invoke(decision);
                return;
            }
            
            if (_isPrioritySwitched)
            {
                byte decision = 4;
                _lastDecision = decision;

                DecisionMade?.Invoke(decision);
                return;
            }
        }
    }

    private void MakeDecision(byte decision)
    {
        _lastDecision = decision;
        DecisionMade?.Invoke(decision);
    }
}
