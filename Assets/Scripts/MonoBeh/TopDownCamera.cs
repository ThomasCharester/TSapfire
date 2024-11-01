using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] Transform target;

    [SerializeField] float height = 0;
    [SerializeField] float cameraSpeed = 1f;
    void Start()
    {
        transform.position = point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = target.position;

        newPosition.y += height;

        //print("TP " + target.position.ToString());
        //print("P1 " + point1.position.ToString());
        //print("P2 " + point2.position.ToString());

        if (((target.position.x > point1.position.x && target.position.x > point2.position.x) || (target.position.x < point1.position.x && target.position.x < point2.position.x))
            && ((transform.position.x > point1.position.x && transform.position.x > point2.position.x) || (transform.position.x < point1.position.x && transform.position.x < point2.position.x)))
            newPosition.x = transform.position.x;
        
        if (((target.position.z > point1.position.z && target.position.z > point2.position.z) || (target.position.z < point1.position.z && target.position.z < point2.position.z))
            && ((transform.position.z > point1.position.z && transform.position.z > point2.position.z) || (transform.position.z < point1.position.z && transform.position.z < point2.position.z)))
            newPosition.z = transform.position.z;

        //print("NP " + newPosition.ToString());

        transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed*Time.deltaTime);
    }

    void ChangePoints(Transform newPoint1, Transform newPoint2)
    {
        point1 = newPoint1;
        point2 = newPoint2;
    }
}
