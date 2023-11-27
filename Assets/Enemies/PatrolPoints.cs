using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    private Vector3 startingPosition;
    private void Start()
    {
        startingPosition = gameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = startingPosition;

    }
}
