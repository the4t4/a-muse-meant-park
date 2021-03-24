using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour {

    public GameObject foundationPreview;//the preview of the gameobject to be built
    
    public BuildSystem buildSystem;

    private bool inBuildingMood;


    private void start(){
        inBuildingMood = false;
    }


    private void Update()
    {
        bool working = Input.GetKeyDown(KeyCode.H) || inBuildingMood;
        if (working && !buildSystem.isBuilding)//when the H key is pressed and the buildSystem is not active
        {
            buildSystem.NewBuild(foundationPreview);//then start the building process
            inBuildingMood = false;
        }
    }

    public void startBuildingMood(){ /// Can be called via the game play manager.
        inBuildingMood = true;
    }

   
}


