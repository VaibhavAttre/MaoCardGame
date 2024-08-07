using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public enum ServerToClientMessage : ushort { 

    ApproveLogin,
}
public enum ClientToServerMessage : ushort { 

    RequestLogin, 
}

public class NetworkManager : MonoBehaviour
{
    protected void Awake()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);

    }

    private static string localUsername;
    [SerializeField] private NetworkData networkSettings;
    public Client client;

    private void Start()
    {
        client  = new Client();
        client.Connected += OnClientConnected;
        Subscription();
    }

    private void Subscription()
    {
        NetworkEvents.ConnectRequest += Connect;
        NetworkEvents.SendMessage += OnSendMessage;
    }

    private void UnSubscription()
    {
        NetworkEvents.ConnectRequest -= Connect;
    }

    private void OnClientConnected(object sender, EventArgs e)
    {
        NetworkEvents.OnConnectSuccess(client.Id, networkSettings.localUsername);
        networkSettings.localId = client.Id;
        PlayerManager.Instance.SpawnInitialPlayer(localUsername);
    }

    private void OnSendMessage(Message message)
    {
        client.Send(message);
    }

    public void Connect(string username)
    {
        networkSettings.localUsername = string.IsNullOrEmpty(username) ? "Guest": username;
        client.Connect($"{networkSettings.Ip}:{networkSettings.Port}");
    }

    private void FixedUpdate()
    {
        client.Update();
    }

    protected void OnDestroy()
    {
        client.Connected -= OnClientConnected;
        UnSubscription();
    }
}
