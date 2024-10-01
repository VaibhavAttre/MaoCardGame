using Riptide;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();
    private static int playerCount = list.Count;
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    public PlayerMovement Movement => movement;

    [SerializeField] private PlayerMovement movement;
    //[SerializeField] private GameObject deckManager;

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    private void Start()
    {

    }

    public static void Spawn(ushort id, string username)
    {
        foreach (Player otherPlayer in list.Values)
            otherPlayer.SendSpawned(id);

        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        Player player = Instantiate(GameLogic.Singleton.PlayerPrefab, new Vector3(x, 1f, z), Quaternion.identity).GetComponent<Player>();
        GameObject spawnParent = GameObject.Find("SpawnPoint");
        player.transform.SetParent(spawnParent.transform, false);
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        player.SendSpawned();
        list.Add(id, player);
        playerCount++;
        NetworkManager.Singleton.SendWaitingRoom();
    }


    #region Messages
    private void SendSpawned()
    {

        NetworkManager.Singleton.Server.SendToAll(AddSpawnData(Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerSpawned)));
    }

    private void SendSpawned(ushort toClientId)
    {
        NetworkManager.Singleton.Server.Send(AddSpawnData(Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerSpawned)), toClientId);
    }

    private Message AddSpawnData(Message message)
    {
        message.AddUShort(Id);
        message.AddString(Username);
        message.AddVector3(transform.position);
        return message;
    }

    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId, Message message)
    {
        Spawn(fromClientId, message.GetString());
    }

    [MessageHandler((ushort)ClientToServerId.input)] 
    private static void Input(ushort fromClientId, Message message)
    {
        if(list.TryGetValue(fromClientId, out Player player))
        {
            player.Movement.SetInput(message.GetBools(6), message.GetVector3());
        }
    }
    #endregion
}