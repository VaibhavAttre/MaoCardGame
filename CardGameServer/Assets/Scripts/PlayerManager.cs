using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject playerPrefab;
    private static GameObject playerStaticPrefab;
    private static Dictionary<ushort,  Player> players = new Dictionary<ushort, Player>();
    public static Player GetPlayer(ushort id)
    {
        players.TryGetValue(id, out Player player);
        return player;
    }

    public static bool RemovePlayer(ushort id)
    {
        if(players.TryGetValue(id, out Player player))
        {
            players.Remove(id);
            return true;
        }
        return false;
    }

    protected override void Awake()
    {
        base.Awake();
        playerStaticPrefab = playerPrefab;

    }

    private static void SpawnPlayer(ushort id, string username) 
    {
        Player player = Instantiate(playerStaticPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.name = $"{username} -- {id}";
        player.Init(id, username);
        players.Add(id, player);
        bool approve = true;
        player.LoginApprove(approve);
    }


    #region Messages

    [MessageHandler((ushort)ClientToServerMessage.RequestLogin)]
    private static void ReceiveLoginRequest(ushort id, Message message)
    {
        string username = message.GetString();
        SpawnPlayer(id, username);
    }
    #endregion
}
