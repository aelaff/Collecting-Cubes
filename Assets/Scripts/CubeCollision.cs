using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    GameObject target;
    LevelEditor levelEditor;
    public static int collectedCubes = 1;
    private void Start()
    {
        target = GameObject.Find("Target");
        levelEditor = GameObject.Find("LevelEditor").GetComponent<LevelEditor>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        //check if the cubes collided by the target
        if (collision.gameObject.name== "Target") {
            //change the layer of collision to layer called cubes which doesn't affected by the player
            gameObject.layer = 11;
            //change the color of cubes to the target sphere which is gold color
            GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
            levelEditor.ChangeLevelBar(collectedCubes++);

        }
    }
    private void Update()
    {
        if (gameObject.layer == 11)
        {
            gameObject.GetComponent<Rigidbody>().AddForce((target.transform.position - gameObject.transform.position) * 10);

        }
    }
}
