public interface INetObject
{
    bool IsMine
    {
        get;
    }

    string ObjectID
    {
        get;
        set;
    }

    void WriteData(string obj);
    void DeleteObject();
}