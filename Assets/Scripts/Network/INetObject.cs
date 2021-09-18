public interface INetObject
{
    bool IsMine
    {
        get;
    }
    void WriteData(string obj);
    void DeleteObject();
}