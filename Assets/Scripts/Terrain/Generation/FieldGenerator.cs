using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    private List<Line> cachedLines = new List<Line>();
    [SerializeField] private WaterLine _waterLine;
    [SerializeField] private GrassLine _grassLine;
    [SerializeField] private RoadLine _roadLine;
    private const int _minHeigth = 10;

    private void Awake()
    {
        GenerateField();
    }

    private void Update()
    {
        foreach (var line in cachedLines)
        {
            if(line)
                line.Proceed();
        }
    }

    private void GenerateField()
    {
        CreateLine(Instantiate(_grassLine, new Vector3(0, 0, 0), Quaternion.identity));
        for (int i = 1; i < 10; i++)
        {
            var random = Random.Range(0, 3);
            CreateLine(Instantiate(GetLineById(random), new Vector3(0, 0, i), Quaternion.identity));
        }
    }

    private void CreateLine(Line line)
    {
        cachedLines.Add(line);

    }

    private Line GetLineById(int id)
    {
        var line = (LineTypes)id;
        switch (line)
        {
            case LineTypes.Grass:
                return _grassLine;
            case LineTypes.Road:
                return _roadLine;
            case LineTypes.Water:
                return _waterLine;
            default:
                return null;
        }
    }

}
