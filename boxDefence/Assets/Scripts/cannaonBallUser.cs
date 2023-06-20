using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;




public class cannaonBallUser : MonoBehaviour
{
    public GameObject cannaonBall;
    public GameObject firePoint;
    public GameObject fireEffect;
    public AudioSource cannaonBallFireSound;
    float hitDrection;


    [Header("POWERBAR SETTINGS")]
    Image powerBarImage;
    float powerRatio;
    bool powerBarControl=false;

    Coroutine powerLoop;

    PhotonView pw;
    


    // Start is called before the first frame update
    void Start()
    {
      pw = GetComponent<PhotonView>();

       if(pw.IsMine){
        //  GetComponent<cannaonBallUser>().enabled=true;

          powerBarImage = GameObject.FindWithTag("powerbar").GetComponent<Image>();


          if(PhotonNetwork.IsMasterClient){

            //gameObject.tag="player1";
            transform.position = GameObject.FindWithTag("Spawnpoint_1").transform.position;
            transform.rotation = GameObject.FindWithTag("Spawnpoint_1").transform.rotation;

            hitDrection = 2f;
          } 
          else 
          {
           // gameObject.tag="player2";            
            transform.position = GameObject.FindWithTag("Spawnpoint_2").transform.position;
            transform.rotation = GameObject.FindWithTag("Spawnpoint_2").transform.rotation;
            hitDrection = -2f;

          }
       } 

       InvokeRepeating("isGameStart",0,0.5f);

        
    }

    public void isGameStart()
    {

       if(PhotonNetwork.PlayerList.Length == 2)
       {
         if(pw.IsMine)
          {
            powerLoop = StartCoroutine(powerBar());
            CancelInvoke("isGameStart");        
          } 
       } else
       {
        StopAllCoroutines();
       }

    }

    IEnumerator powerBar(){
        powerBarImage.fillAmount = 0;
        powerBarControl = false;

        while (true)
        {
            if(powerBarImage.fillAmount < 1 && !powerBarControl){
              powerRatio = 0.001f;

              powerBarImage.fillAmount += powerRatio;

              yield return new WaitForSeconds(0.01f * Time.deltaTime);

            }else
            {
              powerBarControl = true;

              powerRatio = 0.001f;

              powerBarImage.fillAmount -= powerRatio;

              yield return new WaitForSeconds(0.01f * Time.deltaTime);

            }

            if(powerBarImage.fillAmount==0){
                powerBarControl=false;
            }

            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(pw.IsMine)
        {
             if(Input.GetKeyDown(KeyCode.Space))
             {

            GameObject cannaonBallCreate =  PhotonNetwork.Instantiate("CannonBall_P1",firePoint.transform.position,firePoint.transform.rotation,0,null);

            //GameObject cannonFireCreate =   PhotonNetwork.Instantiate("CannonBallFire",firePoint.transform.position,firePoint.transform.rotation,0,null);
            
            cannaonBallCreate.GetComponent<PhotonView>().RPC("tagAdapter",RpcTarget.All,gameObject.tag);
            Rigidbody2D rg = cannaonBallCreate.GetComponent<Rigidbody2D>();
            rg.AddForce(new Vector2(hitDrection,0f)*powerBarImage.fillAmount* 12f, ForceMode2D.Impulse);
            

            cannaonBallFireSound.Play();

             StopCoroutine(powerLoop); 
                   
            }

        }


    }

    public void isPowerPlay(){
        
        
       powerLoop = StartCoroutine(powerBar());
    }



}
