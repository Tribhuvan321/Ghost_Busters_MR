using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    // Start is called before the first frame update
    public OVRInput.RawButton shootButton;
    public GameObject linePrefab;
    public Transform startingPoint;
    public float maxDistance;
    public float timeToDestroy = 0.3f;
    public AudioSource audio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(shootButton))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Debug.Log("olammo Champesadey");

        LineRenderer line = Instantiate(linePrefab).GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.SetPosition(0, startingPoint.position);
        Vector3 endpoint = startingPoint.position + startingPoint.forward * maxDistance;
        line.SetPosition(1, endpoint);

        Destroy(line.gameObject,timeToDestroy);

        audio.Play();
    }
}
