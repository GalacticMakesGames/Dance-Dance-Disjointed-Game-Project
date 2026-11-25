using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetPose
{
    public GameObject targetPose; // The visual target or reference object
    //public float targetRotation; // The required Z angle for alignment
}

[System.Serializable]
public class TargetGroup
{
    public string poseGroupName;
    public TargetPose[] dancePoseArray; // Array of targets in this specific group
}

public class DancePoseSequence : MonoBehaviour
{
    public Transform[] playerObjects; // The interactive objects the player rotates
    public TargetGroup[] poseGroup; // All possible target configurations
    public float angleTolerance = 1f;

    private int currentGroupIndex = 0;

    public Timer timer;

    public bool isPoseCompleted = false;

    public ChoreoProgressBar choreoProgressBar;

    void Start()
    {
        DeactivateAllGroups();
        ShufflePoseGroups();
        ActivateGroup(0);
    }

    // Turns off all target pose visuals at startup so only the active group shows
    void DeactivateAllGroups()
    {
        foreach (TargetGroup group in poseGroup)
        {
            if (group.dancePoseArray == null) continue;

            foreach (TargetPose item in group.dancePoseArray)
            {
                if (item.targetPose != null)
                {
                    item.targetPose.SetActive(false);
                }
            }
        }
    }

    // Fisher-Yates shuffle to randomize the order of TargetGroup elements in poseGroup
    void ShufflePoseGroups()
    {
        for (int positionOfArray = 0; positionOfArray < poseGroup.Length; positionOfArray++)
        {
            TargetGroup tempGroup = poseGroup[positionOfArray];

            int randomizeArray = Random.Range(0, positionOfArray + 1);

            poseGroup[positionOfArray] = poseGroup[randomizeArray];
            poseGroup[randomizeArray] = tempGroup;
        }
    }

    // Call this from your player rotation script whenever an object finishes rotating
    public void CheckGroupAlignment()
    {
        TargetPose[] currentTargets = poseGroup[currentGroupIndex].dancePoseArray;

        // Ensure the arrays match in size to avoid index errors
        if (playerObjects.Length != currentTargets.Length)
        {
            Debug.LogWarning("Player objects count does not match target count in this group!");
            return;
        }

        // Loop through every pair and verify alignment
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (currentTargets[i].targetPose == null) continue;

            float currentZ = playerObjects[i].localEulerAngles.z;
            float targetZ = currentTargets[i].targetPose.transform.localEulerAngles.z;

            // Mathf.DeltaAngle safely handles 0/360 degree wrap-around
            if (Mathf.Abs(Mathf.DeltaAngle(currentZ, targetZ)) > angleTolerance)
            {
                Debug.Log($"Failed on Group {currentGroupIndex}, Part [{i}] ({playerObjects[i].name}): Player={currentZ:F1}°, Target={targetZ:F1}°");
                return; // Exit early if even one object is out of place
            }
        }

        // If the loop finishes, all objects in the group are aligned!
        isPoseCompleted = true;
        OnGroupsAligned();

    }

    public void OnGroupsAligned()
    {
        Debug.Log($"Group {currentGroupIndex} Solved!");

        if (isPoseCompleted == true)
        {
            //ScoreCounter.instance.AddPoint();
            choreoProgressBar.ChoreoBarFill(1);
        }

        foreach (var item in poseGroup[currentGroupIndex].dancePoseArray)
        {
            if (item.targetPose != null) item.targetPose.SetActive(false);
        }

        currentGroupIndex++;

        if (currentGroupIndex < poseGroup.Length)
        {
            ActivateGroup(currentGroupIndex);
        }
        else
        {
            //ActivateGroup(0);
            Debug.Log("Choreography Complete! No more poses left in array. Activate Game Over Screen.");
        }
    }

    private void ActivateGroup(int index)
    {
        Timer.currentTime = Timer.startingTime;
        timer.timerText.text = Timer.currentTime.ToString("0");

        currentGroupIndex = index;

        // Show new group's target visuals
        foreach (var item in poseGroup[currentGroupIndex].dancePoseArray)
        {
            if (item.targetPose != null) item.targetPose.SetActive(true);
        }

        isPoseCompleted = false;
    }
}
