using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // camera to follow the player around the level
    //need to keep z at -10
    //update both x and y positions

    //set up a connection to player
    public GameObject thePlayer;

    //setting offset
    public static float offSetX;
    public float offSetY;

    //Start is called before the first frame update
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(thePlayer.transform.position.x + offSetX, thePlayer.transform.position.y + offSetY, -10);

        Vector3 startPos = transform.position;
        Vector3 endPos = thePlayer.transform.position;


        endPos.x += offSetX;
        endPos.y += offSetY;
        endPos.z += -10;

        transform.position = Vector3.Lerp(startPos, endPos, lerpSpeed * Time.deltaTime);






    }
}
