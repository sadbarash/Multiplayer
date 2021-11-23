using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public NetworkVariable<Vector 3> Position = new NetworkVariable<Vector 3>();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Move();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Position.value;
    }
    
    public void Move()
    {
        if(NetworkManager.Singleton.IsServer)
        {
            var randomPosition = GetRandomPostion();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }
        else
        {
            SubmitPositionRequestServerRpc();
        }
    }

    static Vector3 GetRandomPostion()
    {
        return new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        Position.Value = GetRandomPostion();
    }
}
