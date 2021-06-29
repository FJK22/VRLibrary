using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSize : MonoBehaviour
{
   
    public Vector3 sizeofRow;
    public Vector3 sizeofBook;
    public MeshRenderer rendererRow;
    public  MeshRenderer rendererBook;


    float noOfBooks;
    float distanceBetweenBooks = Mathf.Sqrt((5.59f-5.48f) * (5.59f-5.48f) + (0.97f-0.8f) * (0.97f-0.8f));

    

    void Start()
    {
        //we get the size of row
         sizeofRow = rendererRow.bounds.size;
         Debug.Log("The size of the row is:" + sizeofRow);
         
         //we get the size of book
         sizeofBook = rendererBook.bounds.size;
         Debug.Log("The size of the book is:" + sizeofBook);

         noOfBooks = Mathf.Sqrt(sizeofRow.x * sizeofRow.x + sizeofRow.z * sizeofRow.z) / distanceBetweenBooks;
         int noOfBooks2 = (int) noOfBooks;

         Debug.Log("Number of books that this row can take is:" + noOfBooks2);
        
    }

}
