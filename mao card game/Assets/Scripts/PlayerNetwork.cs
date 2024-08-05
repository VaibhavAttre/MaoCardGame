using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    private NetworkVariable<int> randomNum = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct DataType : INetworkSerializable
    {
        public int num;
        public bool val;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref num);
            serializer.SerializeValue(ref val);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNum.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + "; randNum: " + randomNum.Value);
        };
    }

    private void Update()
    {
        Debug.Log(OwnerClientId + ", " + randomNum.Value);
        if (!IsOwner) return;
        Vector3 moveDir = new Vector3(0, 0, 0);

        if(Input.GetKeyDown(KeyCode.T))
        {
            randomNum.Value = Random.Range(0, 100);
        }
        if (Input.GetKey(KeyCode.W)) moveDir.z += 1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z -= 1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x -= 1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x += 1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    private void testServerRpc()
    {
        Debug.Log("test");
    }
}
