using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DefendBox : MonoBehaviour
{
    float boxHealth = 100;
    public Image healthBar;
    public GameObject healthCanvas;
    PhotonView pw;


    [PunRPC]
    public void defendBoxHit(float hitPower){

       

         boxHealth -= hitPower;

        healthBar.fillAmount = boxHealth/100;
        if(boxHealth<=0){
            Destroy(gameObject);
        }else
        {
           StartCoroutine(canvasChecker());

        }
       

    }

    IEnumerator canvasChecker(){
        if(!healthCanvas.activeInHierarchy){
            healthCanvas.SetActive(true);
            yield return new WaitForSeconds(2);
            healthCanvas.SetActive(false);
        }
    }
}
