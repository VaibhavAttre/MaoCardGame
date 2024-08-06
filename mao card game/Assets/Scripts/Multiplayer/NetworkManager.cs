using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientMessage : ushort { 

    ApproveLogin,
}
public enum ClientToServerMessage : ushort { 

    RequestLogin, 
}

public class NetworkManager : Singleton<NetworkManager>
{
    protected override void Awake()
    {
        base.Awake();
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);

    }

    private static string localUsername;

    public Client client;
    [SerializeField] private ushort m_Port = 7777;
    [SerializeField] private string m_Ip = "127.0.0.1";

    private void Start()
    {
        client  = new Client();
        client.Connected += OnClientConnected;
    }

    private void OnClientConnected(object sender, EventArgs e)
    {
        PlayerManager.Instance.SpawnInitialPlayer(localUsername);
    }

    public void Connect(string username)
    {
        localUsername = string.IsNullOrEmpty(username) ? "Guest": username;
        client.Connect($"{m_Ip}:{m_Port}");
    }

    private void FixedUpdate()
    {
        client.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        client.Connected -= OnClientConnected;
    }
}
