using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI; ///for rebaking the scene everytime a new object is placed.


public class BuildSystem : MonoBehaviour {


    public Camera cam;//camera used for raycast
    public LayerMask layer;//the layer that the raycast will hit on

    private GameObject previewGameObject = null;//referance to the preview gameobject
    private Preview previewScript = null;//the Preview.cs script sitting on the previewGameObject

    public float stickTolerance = 40f;//used for measuring deviation in the mouse when the buildSystem is paused

    [HideInInspector] //hiding this in inspector, so it doesnt accidently get clicked
    public bool isBuilding = false;//are we or are we not currently trying to build something? 
    private bool pauseBuilding = false;//used to pause the raycast
    private Vector3 savedMousePos = Vector3.zero;//used to calculate mouse deviation


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//rotate
        {
            previewGameObject.transform.Rotate(0, 90f, 0);//rotate the preview 90 degrees.
        }

        if (Input.GetKeyDown(KeyCode.G))//cancel build
        {
            CancelBuild();
        }

        if (Input.GetMouseButtonDown(0) && isBuilding)//actually build the thing in the world
        {
            ConfirmBuild();//if so then stop the build and actually build it in the world
        }

        if (isBuilding)
        {
            if (pauseBuilding)//is the build system currently paused? if so then we need to check deviation in the mouse 
            {
                Vector3 delta = Input.mousePosition - savedMousePos;//get the mouse deviation

                if (Mathf.Abs(delta.x) >= stickTolerance || Mathf.Abs(delta.y) >= stickTolerance)//check if horizontal or vertical value is greater than stickTolerance
                {
                    pauseBuilding = false;//if it is, then unpause building, and call the raycast again
                }

            }
            else//if building system isn't paused then call the raycast
            {
                DoBuildRay();
            }
        }
    }

    public void NewBuild(GameObject _go) //starts the building process
    {
        previewGameObject = Instantiate(_go, Vector3.zero, Quaternion.identity);
        previewScript = previewGameObject.GetComponent<Preview>();
        isBuilding = true;
    }

    private void CancelBuild()//this will get rid of the previewGameObject in the scene
    {
        Destroy(previewGameObject);
        previewGameObject = null;
        previewScript = null;
        isBuilding = false;
    }

    private void ConfirmBuild()//build the preview in the world
    {
        previewScript.Place();

        UnityEditor.AI.NavMeshBuilder.BuildNavMesh(); //By Mohido: Rebaking the scene after a building is confirmed an added. Hope I placed in the right place.

        previewGameObject = null;
        previewScript = null;
        isBuilding = false; 
    }

    public void PauseBuild(bool _value)//public method to change the pauseBuilding bool from another script
    {
        savedMousePos = Input.mousePosition;
        pauseBuilding = _value;
    }

    private void DoBuildRay()//positions your previewGameobject in the world
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);//raycast stuff
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100f, layer))
        {   
            //raycast for using unity primitives
            float y = hit.point.y + (previewGameObject.transform.localScale.y / 2f);
            Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
            previewGameObject.transform.position = pos;
            
            //raycast for other pre-built 3d models with correct anchor points 
            //previewGameObject.transform.position = hit.point;
        }
    }

}
