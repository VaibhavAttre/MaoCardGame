using Riptide;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{

    public static Dictionary <ushort, Player> list = new Dictionary <ushort, Player> ();
    private static int playerCount = list.Count;
    public ushort Id {  get; private set; }
    public string Username { get; private set; }
    public bool IsLocal { get; private set; }
    public bool IsHost {  get; set; }

    [SerializeField] private Transform camera;
    public void Init(ushort id, string username, bool isLocal)
    {
        Id = id;
        Username = username;
        IsLocal = isLocal;
    }

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    private void Move(Vector3 newPos, Vector3 forward)
    {
        transform.position = newPos;
        
        if(!IsLocal)
        {
            camera.forward = forward;
        }
    }

    private void MoveAroundDeck(Vector3 loc, Vector3 rotation)
    {
        transform.position = loc;
        transform.rotation = Quaternion.LookRotation(rotation);
    }

    public static void Spawn(ushort id, string username, Vector3 position)
    {
        Player player;
        if(id == NetworkManager.Singleton.client.Id)
        {
            player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = true;
        } else
        {
            player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            player.IsLocal = false;
        }

        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)}";
        player.Id = id;
        player.Username = username;
        if(username == "Host")
        {
            player.IsHost = true;
        }
        list.Add(id, player);
        playerCount++;
    }

    [MessageHandler((ushort)ServerToClientId.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
    }

    [MessageHandler((ushort)ServerToClientId.playerMovement)]
    private static void PlayerMovement(Message message)
    {
        if (list.TryGetValue(message.GetUShort(), out Player player))
        {
            player.Move(message.GetVector3(), message.GetVector3());
        }
    }


    [MessageHandler((ushort)ServerToClientId.spawnDeck)]
    private static void OnSpawnDeck(Message message)
    {
        ushort playerId = message.GetUShort();  // Get player ID
        Vector3 pos = message.GetVector3();  // Get position

        Vector3 rotation = message.GetVector3();  // Get rotation

        // Check if the player exists in the dictionary
        if (Player.list.TryGetValue(playerId, out Player player))
        {
            //player.Move(position, rotation);
            player.transform.position = pos;

            Debug.Log($"Player {player.name}'s position is {pos}");
        }
        else
        {
            Debug.LogError($"Player with ID {playerId} not found in the list.");
        }
    }

    [MessageHandler((ushort)ServerToClientId.waitingRoom)]
    private static void HandleWaitingRoomInfo(Message message)
    {
        int currentPlayerCount = message.GetInt();
        int minPlayersToStart = message.GetInt();

        // Display waiting room status to the player
        Debug.Log($"Waiting for players... ({currentPlayerCount}/{minPlayersToStart})");

        // Update UI to show waiting room information
        // Example: Show how many players are in the room, and if they can start the game
    }
}
