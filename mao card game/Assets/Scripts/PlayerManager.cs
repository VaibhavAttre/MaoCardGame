using Riptide;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*
    [SerializeField] private NetworkData networkSettings;
    [SerializeField] private GameObject playerPrefab;
    private static Dictionary<ushort,  Player> players = new Dictionary<ushort, Player>();
    public static ushort LocalId = ushort.MaxValue;
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

    private void Awake()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public Player LocalPlayer { get { return GetPlayer(networkSettings.localId); } }
    public bool IsLocalPlayer(ushort id)
    {
        return id == LocalPlayer.Id;
    }

    private void Subscribe()
    {
        NetworkEvents.ConnectSuccess += SpawnInitialPlayer;
    }

    private void Unsubscribe()
    {
        NetworkEvents.ConnectSuccess -= SpawnInitialPlayer;
    }

    public void SpawnInitialPlayer(ushort id, string username)
    {
        Player player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.name = $"{username} -- LOCAL PLAYER WAITING FOR SERVER";
        LocalId = id;
        player.Init(id, username, true);
        players.Add(id, player);
        player.RequestInit();
    }

    private static void InitializeLocalPlayer()
    {
        Player local = players[LocalId];
        local.name = $"{local.Username} -- {local.Id} -- LOCAL";
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
    
    #endregion*/
}
