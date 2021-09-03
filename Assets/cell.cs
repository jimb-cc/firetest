using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell : MonoBehaviour
{
    
    [SerializeField] bool onFire = false;
    [SerializeField] float burnRate = 0.05f;
    [SerializeField] float flashpoint = 0f;
    [SerializeField] float fuel = 0f;
    [SerializeField] float heat = 0f;
    [SerializeField] float heatTransferAmount = 0.05f;
    [SerializeField] float coolDownRate = 0.005f;
    
    int xPos, yPos, xMax, yMax;
    string nNeighbour,eNeighbour,sNeighbour,wNeighbour;
    // Start is called before the first frame update
    void Start()
    {
        // where am I in the grid?
        string[] positions = name.Split('-');
        xPos =  int.Parse(positions[1]);
        yPos =  int.Parse(positions[2]);

        xMax = GameObject.Find("GridBuilder").GetComponent<gridbuilder>().GridX;
        yMax = GameObject.Find("GridBuilder").GetComponent<gridbuilder>().GridY;

        Debug.Log("xmax=:"+xMax);
        Debug.Log("ymax=:"+yMax);
    
        fuel = Random.Range(2.0f, 10.0f);
        flashpoint = Random.Range(0.7f, 1.0f);
        burnRate = Random.Range(0.0f, 0.5f);

        transform.localScale = new Vector3(1, fuel, 1);      
        
        nNeighbour = "cell-"+(xPos)+"-"+(yPos+1);
        eNeighbour = "cell-"+(xPos+1)+"-"+(yPos);
        sNeighbour = "cell-"+(xPos)+"-"+(yPos-1);
        wNeighbour = "cell-"+(xPos-1)+"-"+(yPos);

    }

    // Update is called once per frame
    void Update()
    {

        testForFire();
        
        //lerpedColor = Color.Lerp(Color.red, Color.black, Mathf.PingPong(Time.time, 1));

    }
    
    void testForFire()
    {
        // am I on fire?
        if (onFire && fuel > 0 ) {
            GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.red, heat);
            fuel = fuel - burnRate;
            heat = heat - heatTransferAmount;

            
            transform.localScale = new Vector3(1, fuel, 1);
        }
        else  GetComponent<MeshRenderer>().material.color = Color.black;
   
        
        if (yPos < yMax-1) if (GameObject.Find(nNeighbour).GetComponent<cell>().onFire) heat = heat + GameObject.Find(nNeighbour).GetComponent<cell>().heatTransferAmount; else heat=heat-coolDownRate;
        if (xPos < xMax-1) if (GameObject.Find(eNeighbour).GetComponent<cell>().onFire) heat = heat + GameObject.Find(eNeighbour).GetComponent<cell>().heatTransferAmount; else heat=heat-coolDownRate;

        if (yPos > 0) if (GameObject.Find(sNeighbour).GetComponent<cell>().onFire) heat = heat + GameObject.Find(sNeighbour).GetComponent<cell>().heatTransferAmount; else heat=heat-coolDownRate;
        if (xPos > 0) if (GameObject.Find(wNeighbour).GetComponent<cell>().onFire) heat = heat + GameObject.Find(wNeighbour).GetComponent<cell>().heatTransferAmount; else heat=heat-coolDownRate;

        if (heat>flashpoint) onFire = true;
        if (fuel == 0) {heatTransferAmount = 0; onFire = false;}
        if (onFire == false) GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.yellow, heat);

            if (fuel<0) fuel = 0;
            if (heat<0) heat = 0;


  //      {
    //        GetComponent<MeshRenderer>().material.color = lerpedColor;
      //  }
        //else GetComponent<MeshRenderer>().material.color = Color.black;


    }
}
