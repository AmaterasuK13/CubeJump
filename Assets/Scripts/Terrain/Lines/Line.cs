using System.Collections.Generic;
using UnityEngine;

public abstract class Line : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;

    public virtual void Tick()
    {

    }

    public Cube GetCubeByPos(int posX)
    {
        return _cubes.Find(c => c.Position.x == posX);
    }
}
