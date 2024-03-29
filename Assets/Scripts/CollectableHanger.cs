using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHanger : MonoBehaviour
{
    bool isCollect;
    int index;
    public Hanger collector;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollect == true){
            if(transform.parent != null){
                transform.localPosition = new Vector3(0,-index,0);
            }
        }
        
       
    }

    public bool GetIsCollect(){
        return isCollect;
    }

    public void SetCollect(){
        isCollect = true;
    }
      public void SetIndex(int index){
        this.index = index;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Obstacle"){
            collector.reduceHeight(1);
            transform.parent = null;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetComponent<Rigidbody>().useGravity = true;
            int a = transform.GetChild(2).childCount;
            for(int i = 0; i<a; i++){
                Debug.Log(i);
                transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                transform.GetChild(2).gameObject.transform.GetChild(0).parent = null;
            }
                       
        }
    } 
}
