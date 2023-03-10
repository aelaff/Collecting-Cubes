using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    //List contains all the pixel pictures for the levels
    public List<Texture2D> levelPictures;
    //the instantiated cube with all properties
    public GameObject cube;

    //it is alpha thershold because when we compare by zero all near transparent will not deleted
    int alpha=40;
    //The needed number of cubes to win
    static int targetCubes = 0;
    //call the canvasUI
    public  Transform canvasUI;


    private void Start()
    {

        if (PlayerPrefs.GetInt("LevelIndex")  >= levelPictures.Count)
        {
            canvasUI.GetChild(2).gameObject.SetActive(true);
        }
        else {
            GenerateLevel(PlayerPrefs.GetInt("LevelIndex", 0));

        }
        
        //to generate the last saved level
    }

    public void GenerateLevel(int levelIndex)
    {
        int cubeIndex = 0;

        //Change UI in the beginning of level generation
        canvasUI.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = (levelIndex+1) +"";
        canvasUI.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = (levelIndex + 2)+"";
        canvasUI.GetChild(0).GetChild(2).GetChild(0).localScale = new Vector3(0,1,1);

        //reset the cubes renderer , collider and gravity
        

        //give cubes the colors from the given 16*16 image
        Texture2D currentImage= levelPictures[levelIndex];

       
        
        for (int x = 0; x < currentImage.width; x++) {
            for (int y= 0; y < currentImage.height; y++)
            {
                transform.GetChild(cubeIndex).GetComponent<MeshRenderer>().enabled = true;
                transform.GetChild(cubeIndex).GetComponent<BoxCollider>().enabled = true;
                transform.GetChild(cubeIndex).GetComponent<Rigidbody>().useGravity = true;
                Color32 currentColor = currentImage.GetPixel(x,y);

                //to ignore the alpha pixels
                if (currentColor.a > alpha)
                {
                    transform.GetChild(cubeIndex).GetComponent<Renderer>().material.color = currentColor;
                    targetCubes++;
                }
                else {
                    transform.GetChild(cubeIndex).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(cubeIndex).GetComponent<Collider>().enabled = false;
                    transform.GetChild(cubeIndex).GetComponent<Rigidbody>().useGravity = false;
                }
                cubeIndex++;
            }
        }
      
    }
    public void ChangeLevelBar(int collectedCubes) {
        canvasUI.GetChild(0).GetChild(2).GetChild(0).localScale = new Vector3((float)collectedCubes / targetCubes, 1, 1);
       if (collectedCubes == targetCubes) {
            
            StartCoroutine(WinningUIShow());
        }
    }
    IEnumerator WinningUIShow() {
        canvasUI.GetChild(1).GetChild(0).GetComponent<Text>().text = "Level "+ (PlayerPrefs.GetInt("LevelIndex") + 1) + " Completed";
        canvasUI.GetChild(1).gameObject.SetActive(true);
       yield return new WaitForSeconds(1.1f);
       canvasUI.GetChild(1).gameObject.SetActive(false);
      
        
        GameReset();
    }
    void GameReset() {
        
        
            int nextLevel = PlayerPrefs.GetInt("LevelIndex") + 1;
            PlayerPrefs.SetInt("LevelIndex", nextLevel);
            CubeCollision.collectedCubes = 0;
            SceneManager.LoadScene(0);
    
        

    }
    public void PlayFromScratch()
    {
        PlayerPrefs.SetInt("LevelIndex", 0);
        SceneManager.LoadScene(0);

    }
}
