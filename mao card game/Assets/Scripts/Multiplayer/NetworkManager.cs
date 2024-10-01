using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;
using UnityEngine.Windows;

public enum ServerToClientId : ushort { 

    playerSpawned = 1,
    playerMovement,
    spawnDeck,
    waitingRoom
}
public enum ClientToServerId : ushort { 

    name = 1,
    input,
    startGame,
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

    public Client client { get; private set; }

    [SerializeField] private string ip;
    [SerializeField] private string port;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        client = new Client();
        client.Connected += DidConenct;
        client.ConnectionFailed += FailedToConnect;
        client.ClientDisconnected += PlayerLeft;
        client.Disconnected += DidDisconnect;
    }

    private void FixedUpdate()
    {
        client.Update();
    }

    private void OnApplicationQuit()
    {
        client.Disconnect();
    }

    public void Connect()
    {
        Debug.Log("husfwse");
        client.Connect($"{ip}:{port}");
    }

    private void DidConenct(object sender, EventArgs e)
    {
        UIManager.Singleton.SendName();
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.Disconnect();
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        UIManager.Singleton.Disconnect();
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        Destroy(Player.list[e.Id].gameObject);
    }
}
