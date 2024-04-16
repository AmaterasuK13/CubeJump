using UnityEngine;

public class Cube : MonoBehaviour
{
    public Vector3 Position { get { return transform.position; }}
    public bool IsBlocked { get; set; } = false;
}
