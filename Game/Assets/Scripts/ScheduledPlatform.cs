using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScheduledPlatform : MonoBehaviour {

    private int MAX_STEP = 10;


    public List<Vector4> myMoveSchedule;
    public bool myShouldGoCycle;

    int myPosDelta = 1;
    int myPos = 0;
    Vector3 myMoveDelta;
    int myStepsNumber = 0;

    void Start() {
        Vector3 moveVector = new Vector3(myMoveSchedule[myPos].x, myMoveSchedule[myPos].y, myMoveSchedule[myPos].z);
        myStepsNumber = (int)System.Math.Round(myMoveSchedule[myPos].w);
        myMoveDelta = (moveVector) / myStepsNumber;

        myPos += myPosDelta;
    }

    // Update is called once per frame
    void Update () {
        if (myStepsNumber >= 0)
        {
            transform.localPosition += myMoveDelta;
            myStepsNumber--;
            return;
        }

        if (myPos >= myMoveSchedule.Count || myPos < 0)
        {
            if (myShouldGoCycle)
            {
                myPos = (myPos + myMoveSchedule.Count) % myMoveSchedule.Count;
            }
            else
            {
                myPosDelta *= -1; 
            }

            myPos += myPosDelta;
        }

        Vector3 moveVector = new Vector3(myMoveSchedule[myPos].x, myMoveSchedule[myPos].y, myMoveSchedule[myPos].z);
        myStepsNumber = (int)System.Math.Round(myMoveSchedule[myPos].w);
        myMoveDelta = (moveVector * (float)myPosDelta) / myStepsNumber;

        myPos += myPosDelta;
    }
}
