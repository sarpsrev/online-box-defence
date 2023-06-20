using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CannonBall : MonoBehaviour
{
    float hit;
    public float cannonBallDestroyDelay = 2f;


    GameObject gameControl;
    GameObject playerLeft;
    AudioSource cannaonBallDestroySound;
    PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        hit = 20;
        gameControl = GameObject.FindWithTag("GameManager");
        pw = GetComponent<PhotonView>();
        cannaonBallDestroySound=GetComponent<AudioSource>();
        
    } 

    [PunRPC]
    public void tagAdapter(string chosenTag)
    {
        
        playerLeft = GameObject.FindWithTag(chosenTag);
        

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("middleDefendBoxes")){

    
            collision.gameObject.GetComponent<PhotonView>().RPC("defendBoxHit",RpcTarget.All,hit);
            playerLeft.GetComponent<cannaonBallUser>().isPowerPlay();

            GetComponent<CircleCollider2D>().isTrigger = false;

            deleteCannonBall();            
        }
        
        if(collision.gameObject.CompareTag("Player2Tower") || collision.gameObject.CompareTag("player2")){


            gameControl.GetComponent<PhotonView>().RPC("playerHitControl",RpcTarget.All,2,hit);
            playerLeft.GetComponent<cannaonBallUser>().isPowerPlay();


            GetComponent<CircleCollider2D>().isTrigger = false;

            deleteCannonBall();        }

         if(collision.gameObject.CompareTag("Player1Tower") || collision.gameObject.CompareTag("player1")){


            gameControl.GetComponent<PhotonView>().RPC("playerHitControl",RpcTarget.All,1,hit);

            playerLeft.GetComponent<cannaonBallUser>().isPowerPlay();


            GetComponent<CircleCollider2D>().isTrigger = false;

            deleteCannonBall();        }

        if(collision.gameObject.CompareTag("deepPlane")){


            playerLeft.GetComponent<cannaonBallUser>().isPowerPlay();


            deleteCannonBall();
        }


        
    }


    public void deleteCannonBall(){
        if(pw.IsMine)
        {
        PhotonNetwork.Instantiate("boxHit",transform.position,transform.rotation,0,null);    
        PhotonNetwork.Destroy(gameObject);
        cannaonBallDestroySound.Play();
        }
    }


}
