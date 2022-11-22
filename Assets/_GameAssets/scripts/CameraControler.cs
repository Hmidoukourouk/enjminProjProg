using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public static CameraControler instance;
    [SerializeField] float speed=10f;
    Vector3 offset;
    public List<Transform> players = new List<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count<=0)
        {
            return;
        }
        Vector3 totalPos = Vector3.zero;
        foreach (Transform item in players)
        {
            totalPos += item.position;
        }
        Vector3 newPosition = totalPos / players.Count;
        //Debug.Log(newPosition);
        transform.position = Vector3.Lerp(transform.position, new Vector3(newPosition.x, 0, newPosition.z)+offset, Time.deltaTime*speed);
    }
}
