using Riptide;
using Riptide.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientMessage : ushort
{

    ApproveLogin,
}
public enum ClientToServerMessage : ushort
{

    RequestLogin,
}

public class NetworkManager : Singleton<NetworkManager>
{
    protected override void Awake()
    {
        base.Awake();
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);

    }

    public Server server;
    [SerializeField] private ushort m_Port = 7777;
    [SerializeField] private ushort m_MaxPlayers;

    private void Start()
    {
        server = new Server();
        server.Start(m_Port, m_MaxPlayers);
    }

    private void FixedUpdate()
    {
        server.Update();
    }
}
