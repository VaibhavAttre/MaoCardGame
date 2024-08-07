using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = " Multiplayer/Network/Settings")]
public class NetworkData : ScriptableObject
{
    [SerializeField] private ushort port = 7777;
    [SerializeField] private string ip = "127.0.0.1";

    public string localUsername;
    public ushort localId;
    public ushort Port {  get { return port; } }
    public string Ip { get { return ip; } }
}
