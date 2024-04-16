using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField] private WaterLine _waterLine;
    [SerializeField] private GrassLine _grassLine;
    [SerializeField] private RoadLine _roadLine;
    [SerializeField] private GameObject _blockedCubePrefab;
    private List<Line> _cachedLines = new List<Line>();

    private void Update()
    {
        foreach (var line in _cachedLines)
        {
            if(line)
                line.Tick();
        }
    }

    public void GenerateField()
    {
        CreateLine(Instantiate(_grassLine, new Vector3(0, 0, 0), Quaternion.identity));
        for (int i = 1; i < 9; i++)
        {
            var random = Random.Range(0, 3);
            CreateLine(Instantiate(GetLineById(random), new Vector3(0, 0, i), Quaternion.identity));
        }
        CreateLine(Instantiate(_grassLine, new Vector3(0, 0, 9), Quaternion.identity));
        GenerateBlockedCubes();
    }

    public Cube GetStartPosCube()
    {
        return _cachedLines[0].GetCubeByPos(5);
    }

    public Cube GetNextCube(int xAxis, int zAxis, Vector3 currentPos) 
    {
        if (!CheckIfOutOfRange((int)currentPos.x + xAxis, (int)currentPos.z + zAxis)) 
            return null;

        return _cachedLines[(int)currentPos.z + zAxis].GetCubeByPos((int)currentPos.x + xAxis);
    }

    private void CreateLine(Line line)
    {
        _cachedLines.Add(line);
    }

    private void GenerateBlockedCubes()
    {
        for (int i = 1; i < 9; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (ConditionToBlock(j, i))
                {
                    var percentage = Random.Range(0, 100);
                    if (percentage < 50)
                    {
                        BlockCube(_cachedLines[i].GetCubeByPos(j));
                    }
                }
            }
        }
    }

    private void BlockCube(Cube cube)
    {
        cube.IsBlocked = true;
        Instantiate(_blockedCubePrefab, cube.Position + Vector3.up, Quaternion.identity);
    }

    private bool CheckIfOutOfRange(int posX, int posZ)
    {
        var isTrue = (posX >= 0 && posX <= 9 && posZ >= 0 && posZ <= 9);

        if (!isTrue) 
            Debug.LogError("Out of field");
        
        return isTrue;
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

    private bool ConditionToBlock(int posX, int posZ)
    {
        if (posX > 0)
        {
            var left = _cachedLines[posZ].GetCubeByPos(posX - 1);

            if (left && left.IsBlocked)
            {
                return false;
            }
        }

        if (posZ > 0)
        {
            var down = _cachedLines[posZ - 1].GetCubeByPos(posX);

            if (down && down.IsBlocked)
            {
                return false;
            }

            if (posX > 0 && posX < 9)
            {
                var downLeft = _cachedLines[posZ - 1].GetCubeByPos(posX - 1);
                var downRight = _cachedLines[posZ - 1].GetCubeByPos(posX + 1);

                if (downLeft && downLeft.IsBlocked && downRight && downRight.IsBlocked)
                {
                    return false;
                }
            }
        }
        return true;
    }

}
