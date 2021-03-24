using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    
    
    /// Called by the button for creating an object. Each button should have a game object preview related to it
    public void buildBuilding(GameObject _go){
        /// Setting the Game BuildManager to true instead of pressing H
        GameObject buildManager = GameObject.Find("BuildingSystem");        /// get the build manager
        BuildManager BMScript = buildManager.GetComponent<BuildManager>();  /// get the script on it
        BMScript.startBuildingMood();                                       /// start the build manager script.
    }

    public void destroyBuilding(){
        /// Enters a destroy building mode which we will implement later.
    }

    /// increasing teh count of the repairmen we have.
    public void addRepairman(){

    }

    /// spawn a cleaner near the cleaning house in the game. The player can set a destination to the cleaner through cleaner menu or through clikcing on them. 
    public void addCleaner(){

    }

    /// increasing the number of security guards and spawning them. Note: security guiurds will roam the park freely. So basically the current implementation of the GUESTS right now is more likely a guard than a guest
    public void addSecurityGuard(){

    }

}
