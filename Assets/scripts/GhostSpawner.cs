using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnTimer = 1;
    public int spawnTry = 1000;
    private float timer;

    public GameObject ghostPrefab;
    public float minEdgeDistance = 0.3f;
    public MRUKAnchor.SceneLabels sceneLabels;
    public float offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!MRUK.Instance && !MRUK.Instance.IsInitialized)
        {
            return;
        }

        timer += Time.deltaTime;
        if(timer > spawnTimer)
        {
            SpawnGhost();
            timer -= spawnTimer;

        }
    }

    void SpawnGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        int currentTry = 0;
        while(currentTry < spawnTry)
        {
            bool foundAPos = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(sceneLabels), out Vector3 pos, out Vector3 norm);

            if (foundAPos)
            {
                Vector3 randomPositionWithOffset = pos + norm * offset;
                randomPositionWithOffset.y = 0;

                Instantiate(ghostPrefab, randomPositionWithOffset, Quaternion.identity);

                return;
            }
            else
            {
                currentTry++;
            }
        }
        
    }
}
