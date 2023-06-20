using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;




public class GameControl : MonoBehaviour
{




    [Header("PlayerBoxHealth")]
    public Image player1HealthImage;
    public Image player2HealthImage;
    float player1Health=100;
    float player2Health=100;
    PhotonView pw;

 



    // Start is called before the first frame update
    private void Start()
    {
    pw = GetComponent<PhotonView>();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void playerHitControl(int hitCase,float hitPower){

        switch (hitCase)
        {
            case 1:
            
             player1Health -= hitPower;

             player1HealthImage.fillAmount = player1Health/100;

             if(player1Health<=0)
             {
             }

             break;
             case 2:
             player2Health -= hitPower;
             if(player2Health<=0)
             {
             }             

             player2HealthImage.fillAmount = player2Health/100;             
             break;
            
            
        }


    }

}
