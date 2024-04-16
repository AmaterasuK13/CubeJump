using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AIBrain _aiPlayerPrefab;
    [SerializeField] private FieldGenerator _fieldGeneratorPrefab;
    [SerializeField] private Transform _startPosition;

    private AIBrain _aiPlayer;
    private FieldGenerator _fieldGenerator;

    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        _fieldGenerator = Instantiate(_fieldGeneratorPrefab);
        _fieldGenerator.GenerateField();
        _aiPlayer = Instantiate(_aiPlayerPrefab);
        _aiPlayer.Init(_fieldGenerator);
    }
}
