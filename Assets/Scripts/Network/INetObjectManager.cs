public interface INetObjectManager
{
    int GetNetObjectCount();
    void DestroyObjectAtIndex(int index);
    void AddSpawnObject(string data);
    void AddDespawnObject(NetDataBase data);
    void PassDataToObject(string objectID, string dataString);
}
