using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientId : ushort
{

    playerSpawned = 1,
    playerMovement,
    spawnDeck,
    waitingRoom,
}
public enum ClientToServerId : ushort
{

    name = 1,
    input,
    startGame,
}

public enum GameState
{
    WaitingRoom,
    GameInProgress
}

public class NetworkManager : MonoBehaviour
{

    private static NetworkManager singleton;

    public static NetworkManager Singleton
    {

        get => singleton;
        private set
        {
            if (singleton == null)
                singleton = value;
            else if (singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instnace already exists");
                Destroy(value);
            }

        }

    }

    public Server Server { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;
    [SerializeField] private ArrangeAroundBoard deckManager;
    [SerializeField] private int minPlayers = 3;
    private bool startServer = false;
    public static GameState CurrentGameState = GameState.WaitingRoom;
    public ArrangeAroundBoard DeckManager => deckManager;


    private void Awake()
    {
        Singleton = this;
    }

    public void changeServerState(bool serverState)
    {
        startServer = serverState;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += PlayerLeft;
    }

    private void PlayerLeft(object sender, ServerDisconnectedEventArgs e)
    {
        Destroy(Player.list[e.Client.Id].gameObject);
        if(Player.list.Count < minPlayers)
        {
            CurrentGameState = GameState.WaitingRoom;
        }
        SendWaitingRoom();
    }

    private void FixedUpdate()
    {
        Server.Update();
        if(CurrentGameState == GameState.WaitingRoom && Player.list.Count >= minPlayers)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        CurrentGameState = GameState.GameInProgress;
        int playerCount = Player.list.Count;
        int i = 0;
        foreach (Player player in Player.list.Values)
        {
            NetworkManager.Singleton.DeckManager.Arrange(playerCount, player, i);
            i++;
        }
     
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public void SendWaitingRoom()
    {
        Debug.Log("Sending to waiting room");
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.waitingRoom);
        message.AddInt(Player.list.Count);  
        message.AddInt(minPlayers);  
        NetworkManager.Singleton.Server.SendToAll(message);
    }

    #region Messages

    #endregion
}
