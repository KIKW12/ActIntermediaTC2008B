using UnityEngine;

public static class Vector2External
{
    public static Vector2 Rotate(this Vector2 originalVector, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return rotation * originalVector;
    }
}
