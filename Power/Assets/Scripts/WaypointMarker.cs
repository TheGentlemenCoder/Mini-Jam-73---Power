using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    public GameObject[] collectables;
    public Transform closestCore;
    public GameObject getPlayer;

    private void Start()
    {
        pointerRectTransform = this.GetComponent<RectTransform>();
        closestCore = null;
    }

    private void Update()
    {
        FindTarget();
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = getPlayer.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = ((Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360) - 90;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void FindTarget()
    {
        closestCore = GetClosestCore();
        if (collectables.Length != 0)
        {
            targetPosition = new Vector3(closestCore.position.x, closestCore.position.y);
        }
        else
        {
            targetPosition = new Vector3(GameObject.Find("GoalPost").GetComponent<Transform>().position.x,
                        GameObject.Find("GoalPost").GetComponent<Transform>().position.y);
        }
    }

    public Transform GetClosestCore()
    {
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;
        foreach (GameObject go in collectables)
        {
            float currentDistance = Vector3.Distance(getPlayer.transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }
}

