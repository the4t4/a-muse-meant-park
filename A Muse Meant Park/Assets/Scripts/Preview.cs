using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{

    public GameObject prefab;//the prefab that represents this preview ie, a foundation, wall....

    private MeshRenderer myRend;
    public Material goodMat;//green material
    public Material badMat;//red material

    private BuildSystem buildSystem;

    public List<string> tagsISnapTo = new List<string>();//list of all of the SnapPoint tags this particular preview can snap to
                                                         //this allows this previewObject to be able to snap to multiple snap points


    private void Start()
    {
        buildSystem = GameObject.FindObjectOfType<BuildSystem>();
        myRend = GetComponent<MeshRenderer>();
        myRend.material = goodMat;
    }

    public void Place()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)//this is what dertermins if you are snapped to a snap point
    {
        for (int i = 0; i < tagsISnapTo.Count; i++)//loop through all the tags this preview can snap too
        {
            string currentTag = tagsISnapTo[i]; //setting the current tag were looking at to a string...its easier to write currentTag then tagsISnapTo[i]

            if(other.tag == currentTag)
            {
                buildSystem.PauseBuild(true); //since we are using a raycast to position the preview
                                              //when we snap to something we need to "pause" the raycast

                transform.position = other.transform.position;//set position of preview so that it "snaps" into position
            }

        }
    }

    private void OnTriggerExit(Collider other)//this is what determines if the preview is no longer snapped to a snap point
    {
        for (int i = 0; i < tagsISnapTo.Count; i++)//loop through all tags
        {
            string currentTag = tagsISnapTo[i];

        }
    }
}
