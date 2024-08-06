using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject playerPrefab;
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

    public static Player LocalPlayer { get { return GetPlayer(NetworkManager.Instance.client.Id); } }
    public bool IsLocalPlayer(ushort id)
    {
        return id == LocalPlayer.Id;
    }

    public void SpawnInitialPlayer(string username)
    {
        Player player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.name = $"{username} -- LOCAL PLAYER WAITING FOR SERVER";
        ushort id = NetworkManager.Instance.client.Id;
        player.Init(id, username, true);
        players.Add(id, player);
        player.RequestInit();
    }

    private static void InitializeLocalPlayer()
    {
        LocalPlayer.name = $"{LocalPlayer.Username} -- {LocalPlayer.Id} -- LOCAL";
    }

    #region Messages

    [MessageHandler((ushort)(ServerToClientMessage.ApproveLogin))]
    private static void RecievedApproveLogin(Message msg)
    {
        bool approve = msg.GetBool();
        if(approve)
        {
            InitializeLocalPlayer();
        }
    }
    
    #endregion
}
