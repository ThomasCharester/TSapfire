using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Entity trackingEntity;

    [SerializeField] Transform heartStartPosition;
    [SerializeField] GameObject heart;
    Stack<GameObject> hearts = new();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth(trackingEntity.GetHealth());
        
    }

    void DisplayHealth(float health)
    {
        while(hearts.Count != health)
        {
            if (hearts.Count > health) 
                Destroy(hearts.Pop());
            else hearts.Push(Instantiate(heart, heartStartPosition.position + new Vector3(1f,0,0),transform.rotation));
        }
    }
}
