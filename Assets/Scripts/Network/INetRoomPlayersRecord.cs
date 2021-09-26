public interface INetRoomPlayersRecord
{

    void RegisterPlayer(string playerID, INetObject playerObject);
    void RemovePlayer(string playerID);

}