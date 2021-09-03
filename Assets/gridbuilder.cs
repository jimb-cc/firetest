using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridbuilder : MonoBehaviour
{
    [SerializeField] public int GridX = 10;
    [SerializeField] public int GridY = 10;

    public GameObject Cell;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid(GridX,GridY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGrid(int GridX,int GridY)
    {
 //       GameObject CellObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
 //       CellObject.transform.localScale = new Vector3(1,1,1);

        GameObject[,] CellGrid = new GameObject[GridX, GridY];
   
        for (int x = 0; x < GridX; x++)
        {
            for (int y = 0; y < GridY; y++)
            {
                

               GameObject CellObject = Instantiate(Cell, new Vector3(x *1.10F, 0, y  * 1.10F), Quaternion.identity);
               
               if (Random.Range(0, 2) == 0) CellObject.GetComponent<MeshRenderer>().material.color = Color.black;
               CellObject.name = "cell-"+x+"-"+y;
             }
        }

    }

}
