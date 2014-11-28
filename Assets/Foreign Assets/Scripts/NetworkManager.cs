using UnityEngine;
using System.Collections;
public class NetworkManager : MonoBehaviour
{
    private const string typeName = "popadmin286";
    private const string gameName = "mainserver";

    private void StartServer()
    {
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }
    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
    }
    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            GUI.Box(new Rect(10, 10, 185, 140), "Loader Menu");

            if (GUI.Button(new Rect(15, 35, 175, 50), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(15, 90, 175, 50), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(30, 250 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }
    private HostData[] hostList;

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }
    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
    }
            }
public class NetworkManagerConnect : MonoBehaviour
{
    public float speed = 10f;

    public GameObject player;

    void OnServerInitialized()
    {
        SpawnPlayer();
    }

    void OnConnectedToServer()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Network.Instantiate(player, new Vector3(683.4358f, 21.75099f, 659.9949f), Quaternion.identity, 0);
    }
}


