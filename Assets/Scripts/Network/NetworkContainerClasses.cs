using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PacketWS<T>
{
    public string servOp;
    public string eventName;
    public T data;
}

[System.Serializable]
public struct PositionWS
{
    public float X, Y, Z;
    public static Vector3 vector3 = Vector3.zero;

    public PositionWS(Vector3 position)
    {
        X = position.x;
        Y = position.y;
        Z = position.z;
    }

    public PositionWS ToPositionWS(Vector3 position)
    {
        X = position.x;
        Y = position.y;
        Z = position.z;
        return this;
    }

    public Vector3 ToVector()
    {
        vector3.x = X;
        vector3.y = Y;
        vector3.z = Z;
        return vector3;
    }

}

[System.Serializable]
public class RotationWS
{
    public float X, Y, Z, W;

    public RotationWS(Quaternion rotation)
    {
        X = rotation.x;
        Y = rotation.y;
        Z = rotation.z;
        W = rotation.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(X, Y, Z, W);
    }
}

[System.Serializable]
public abstract class NetDataBase
{
    public string eventName;
    public string id;
    public SpawnObjectWS data;
}

[System.Serializable]
public class TransformWS
{
    public PositionWS position;
    public RotationWS rotation;

    public TransformWS() {}
    
    public TransformWS(Vector3 position, Quaternion rotation) {
        this.position = new PositionWS(position);
        this.rotation = new RotationWS(rotation);
    }
}

[System.Serializable]
public class SpawnObjectWS
{
    public string ownerID;
    public string objectName;
    public TransformWS transformWS;
}

[System.Serializable]
public class DestroyObjectWS
{
    public string id;
}

[System.Serializable]
public class RecieveSpawnObjectWS : NetDataBase
{
}


[System.Serializable]
public class RecieveDestroyObjectWS : NetDataBase
{
    
}
