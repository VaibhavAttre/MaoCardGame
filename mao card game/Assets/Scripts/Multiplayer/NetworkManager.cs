using Riptide;
using Riptide.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    protected override void Awake()
    {
        base.Awake();
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);

    }

    public Client client;
    [SerializeField] private ushort m_Port = 7777;
    [SerializeField] private string m_Ip = "127.0.0.1";

    private void Start()
    {
        client  = new Client();
        Connect();
    }

    public void Connect()
    {
        client.Connect($"{m_Ip}:{m_Port}");
    }

    private void FixedUpdate()
    {
        client.Update();
    }
}
