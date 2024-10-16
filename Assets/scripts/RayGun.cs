using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    // Start is called before the first frame update
    public OVRInput.RawButton shootButton;
    public GameObject linePrefab;
    public GameObject impactPrefab;
    private Vector3 endPoint;
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
        Ray ray = new Ray(startingPoint.position, startingPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxDistance);
        
        Debug.Log("olammo Champesadey");
        LineRenderer line = Instantiate(linePrefab).GetComponent<LineRenderer>();


        line.positionCount = 2;
        line.SetPosition(0, startingPoint.position);
        endPoint = Vector3.zero;
        

        Destroy(line.gameObject, timeToDestroy);
        if (hasHit)
        {
            endPoint = hit.point;
            GameObject rayImpact =Instantiate(impactPrefab, endPoint, Quaternion.LookRotation(-hit.normal));
            Destroy(rayImpact, 0.2f);
        }
        else
        {
            endPoint = startingPoint.position + startingPoint.forward * maxDistance;
        }

        line.SetPosition(1, endPoint);
        audio.Play();
    }
}
