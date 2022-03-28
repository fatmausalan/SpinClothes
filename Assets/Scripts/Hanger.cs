using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger : MonoBehaviour
{
    GameObject mainHanger;
    GameObject tempHanger = null;
    int height;
    int dressCount = 0;
    int temp = 4;
    bool isHangersFull = false;
    int nextHanger = 16;

    void Start(){
        mainHanger = GameObject.Find("MainHanger");
        mainHanger.transform.position = new Vector3(transform.position.x, height, transform.position.z);
        this.transform.localPosition = new Vector3(0,-height,0);
        StartCoroutine(hangerMoveRotate());
        tempHanger = this.gameObject;

    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal")*5*Time.deltaTime;
        transform.position += new Vector3(-1f * Time.unscaledDeltaTime, 0,0);
        this.transform.Translate(horizontalAxis, 0, 0);
    }
   
    IEnumerator hangerMoveRotate(){
        float rotation = transform.rotation.y;
        while (true) {
            rotation += 50;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0)); // rotate on y axis
            yield  return new WaitForSecondsRealtime(0.1f);
        }
    }


   
    private void OnTriggerEnter(Collider other) {
      
        if(other.gameObject.tag == "Dress"){           
            if(temp == 16 && isHangersFull == true){
                if(this.gameObject.transform.childCount > nextHanger){
                    tempHanger= this.gameObject.transform.GetChild(nextHanger).gameObject;
                    temp = 4;
                    other.gameObject.SetActive(false);
                    dressCount++;
                    tempHanger.transform.GetChild(temp).gameObject.SetActive(true);
                    temp++;
                    nextHanger++;
                }
            }
            else{
                other.gameObject.SetActive(false);
                tempHanger.transform.GetChild(temp).gameObject.SetActive(true);
                temp++;
                if(temp == 16){
                    isHangersFull = true;
                }
            }
           
          
        }
        if(other.gameObject.GetComponent<CollectableHanger>() == null){
            return;
        }
        if(other.gameObject.tag == "Hanger" && other.gameObject.GetComponent<CollectableHanger>().GetIsCollect() == false){
            height -= 1;
            other.gameObject.GetComponent<CollectableHanger>().SetCollect();
            other.gameObject.GetComponent<CollectableHanger>().SetIndex(height);
            other.gameObject.transform.parent = mainHanger.transform;
        }  
      
    }

    public void reduceHeight(int h){
      height -= h;
    }
}