using UnityEngine;

[System.Serializable]
public class SerializeableVector3
{
    float x, y, z;

    public SerializeableVector3(Vector3 original)
    {
        x = original.x;
        y = original.y;
        z = original.z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}