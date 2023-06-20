using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;




public class myServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
        
    }
        public override void OnConnectedToMaster()
    {
        Debug.Log("server");

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("lobby");

        
    }

    public void randomEntrance()
    {
      PhotonNetwork.LoadLevel(1);
      PhotonNetwork.JoinRandomRoom();
    }

    public void createOwnRoom()
    {
      PhotonNetwork.LoadLevel(1);
      string roomId = Random.Range(0,2761763).ToString();  


      PhotonNetwork.JoinOrCreateRoom(roomId , new RoomOptions{MaxPlayers=2,IsOpen=true,IsVisible=true},TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
      InvokeRepeating("checkData",0,1f);
      GameObject firstPlayer = PhotonNetwork.Instantiate("Cannon_P1",Vector3.zero,Quaternion.identity,0,null);
      firstPlayer.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("usernamess");

      if(PhotonNetwork.PlayerList.Length==2){
        firstPlayer.gameObject.tag="player2";     
       }

      //GameObject secondPlayer = PhotonNetwork.Instantiate("Cannon_P2");


    }

    public override void OnLeftRoom()
    {
        
    }

    public override void OnLeftLobby()
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }


    public override void OnPlayerLeftRoom(Player newPlayer)
    {
       InvokeRepeating("checkData",0,1f);

        
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }

     public override void OnJoinRandomFailed(short returnCode, string message)
    {
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

   void checkData()
   {

    if(PhotonNetwork.PlayerList.Length==2){

     GameObject.FindWithTag("playerWaiting").SetActive(false);
     GameObject.FindWithTag("P1name").GetComponent<TMPro.TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
     GameObject.FindWithTag("P2name").GetComponent<TMPro.TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
     CancelInvoke("checkData");



    }
    else
    {

     GameObject.FindWithTag("playerWaiting").SetActive(true);
     GameObject.FindWithTag("P1name").GetComponent<TMPro.TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
     GameObject.FindWithTag("P2name").GetComponent<TMPro.TextMeshProUGUI>().text = ".....";      

    }
   }



 


}
