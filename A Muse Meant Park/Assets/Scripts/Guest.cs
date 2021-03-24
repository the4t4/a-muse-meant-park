using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;


enum States {
    Playing, Walking, Idle
}


public class Guest : MonoBehaviour
{   
    States status;                      /// Sets the states of the guest.

    NavMeshAgent navMeshAgent;  /// the navMeshAgent Component regarding the guest.

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        status = States.Idle;
    }

    void Update()
    {
       //  buildings;
        if(status == States.Idle){// if he is doing nothing, make him do somehting.
            GameObject[] buildings = GameObject.FindGameObjectsWithTag("Path_Block"); ///choosing a random destination on the road. Note: Path_Block should be attached to buildings not roads.
            int index = Random.Range(0, buildings.Length);  ///random index.
            Transform pos = buildings[index].transform;               // getting position of that building.
            navMeshAgent.SetDestination(pos.position);      // setting the agent (Guest) to head toward that destination.
            status = States.Walking;                        // changing the status of the Guest to walking

        }

        if(status == States.Walking){ ///if he is walking then check if he reached his destination.
            if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                status = States.Idle;
                Debug.Log("got to the distance.");
                ///TDOD: Interacting with buildings funcitonality. the object disappear till the game finishes.
            }
        }

        if(status == States.Playing){
            Debug.Log("playing");
            ///TODO: Check if the current game he is playing with is still playing.
            ///        or maybe i will make the building changes the state of the players to idle after it is done playing.
            
            ///TODO: Make the player disappear till it is done playing.
        }   
    }


    /// this should be called from the building after it is done playing. The building should call all the guests that are in it and call this function.
    public void DonePlaying(){ 
        status = States.Idle;
    }
}
