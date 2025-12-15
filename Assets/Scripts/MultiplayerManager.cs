using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server.");
        Debug.Log("Region: " + PhotonNetwork.CloudRegion);

        StartCoroutine(PingLoop());

        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms available, creating a new room.");
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Cilantro", options);

    }
    private IEnumerator PingLoop()
    {

        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Ping: " + PhotonNetwork.GetPing()+" ms");
            }

        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        PhotonNetwork.Instantiate("PlayerPrefab", randomPosition, Quaternion.identity);
    }   



}
