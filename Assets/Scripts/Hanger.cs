using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger : MonoBehaviour
{
    GameObject mainHanger;
    GameObject tabla;
    GameObject tempHanger = null;
    float height;
    int dressCount = 0;
    int temp = 0;
    bool isHangersFull = false;
    int nextHanger = 12;

    void Start(){
        mainHanger = GameObject.Find("MainHanger");
        tabla = mainHanger.transform.GetChild(2).gameObject;
        mainHanger.transform.position = new Vector3(transform.position.x, -height, transform.position.z);
        this.transform.localPosition = new Vector3(0,height,0);
        StartCoroutine(hangerMoveRotate());
        tempHanger = this.gameObject;

    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal")*5*Time.deltaTime;
        transform.position += new Vector3(-1f * Time.deltaTime, 0,0);
        this.transform.Translate(0, 0, horizontalAxis);
    }
   
    IEnumerator hangerMoveRotate(){
        float rotation = transform.rotation.y;
        while (true) {
            rotation += 50;
            tabla.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0)); // rotate on y axis
            yield  return new WaitForSecondsRealtime(0.1f);
        }
    }


   
    private void OnTriggerEnter(Collider other) {
      
        if(other.gameObject.tag == "Dress"){           
            if(temp == 12 && isHangersFull == true){
                if(tabla.transform.childCount > nextHanger){
                    tempHanger= tabla.transform.GetChild(nextHanger).gameObject;
                    temp = 0;
                    other.gameObject.SetActive(false);
                    dressCount++;
                    tempHanger.transform.GetChild(2).gameObject.transform.GetChild(temp).gameObject.SetActive(true);
                    temp++;
                    nextHanger++;
                }
            }
            else{
                other.gameObject.SetActive(false);
                tempHanger.transform.GetChild(2).gameObject.transform.GetChild(temp).gameObject.SetActive(true);
                temp++;
                if(temp == 12){
                    isHangersFull = true;
                }
            }
           
          
        }
        if(other.gameObject.GetComponent<CollectableHanger>() == null){
            return;
        }
        if(other.gameObject.tag == "Hanger" && other.gameObject.GetComponent<CollectableHanger>().GetIsCollect() == false){
            height -= 0.5f;
            Debug.Log("heyy");
            other.gameObject.GetComponent<CollectableHanger>().SetCollect();
          //  other.gameObject.GetComponent<CollectableHanger>().SetIndex(height);
            other.gameObject.transform.parent = tabla.transform;
        }  
      
    }

    public void reduceHeight(int h){
      height -= h;
    }
}
