using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class UnloadObject : MonoBehaviour
{
    GameObject player;
    List<Object> unloadedObjects;
    List<Object> objectsToUnload;
    [SerializeField] float maxIntersections;
    List<Object> objectsToReload;
    LayerMask layersMask;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        unloadedObjects = new();
        objectsToUnload = new();
        objectsToReload = new();
        layersMask = LayerMask.GetMask("Unload", "Player");
    }

    private void FixedUpdate()
    {
        TestCollision();
        Unload();
    }

    private void TestCollision()
    {
        float playerDist = Vector3.Distance(transform.position, player.transform.position);
        RaycastHit hit;
        Vector3 pos = transform.position;
        bool reachedPlayer = false;
        float numIntersections = 0;
        Vector3 offset = new Vector3(0, 0.1f, 0);
        while (!reachedPlayer && numIntersections < maxIntersections)
        {
            if (Physics.Raycast(pos, transform.forward + offset, out hit, playerDist, layersMask))
            {
                if (playerDist <= 0)
                {
                    reachedPlayer = true;
                    break;
                }

                if (hit.transform.tag == "Player")
                {
                    reachedPlayer = true;
                    Debug.DrawRay(pos, (transform.forward + offset) * hit.distance, new Color(numIntersections * 0.1f, numIntersections * 0.1f, numIntersections * 0.1f));
                    break;
                }

                Debug.DrawRay(pos, (transform.forward + offset) * hit.distance, new Color(numIntersections * 0.1f, numIntersections * 0.1f, numIntersections * 0.1f));
                Object intersectedObject = hit.transform.GetComponent<Object>();
                objectsToUnload.Add(intersectedObject);

                // Update values for next iteration step
                pos = hit.point;
                playerDist -= hit.distance;

                ++numIntersections;
            }

            else reachedPlayer = true;
        }

        foreach (Object obj in objectsToUnload)
        {
            if (unloadedObjects.Contains(obj)) continue;
            unloadedObjects.Add(obj);
        }
    }

    //private void Unload()
    //{
    //    foreach (Object obj in unloadedObjects)
    //    {
    //        SpriteRenderer _renderer = obj.GetComponent<SpriteRenderer>();
    //        if (!objectsToUnload.Contains(obj))
    //        {
    //            int materialId = obj.GetObjectID();
    //            _renderer.material = SetMaterial(materialId);
    //            _renderer.material.SetColor("_Color", _renderer.color);
    //            objectsToReload.Add(obj);
    //        }
    //        else
    //        {
    //            Debug.Log("Shaders");
    //            float dist = Mathf.Abs(obj.transform.position.x - player.transform.position.x);
    //            float alpha = Mathf.Clamp(dist - playerFadeDist, 0.5f, 1.0f);
    //            _renderer.material = defaultMat;
    //            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
    //        }
    //    }
    //    objectsToUnload.Clear();

    //    foreach (Object obj in objectsToReload)
    //    {
    //        unloadedObjects.Remove(obj);
    //    }
    //    objectsToReload.Clear();
    //}

    private void Unload()
    {
        foreach (Object obj in unloadedObjects)
        {
            SpriteRenderer _renderer = obj.GetComponent<SpriteRenderer>();
            Material mat = _renderer.material;
            if (!objectsToUnload.Contains(obj))
            {
                mat.SetFloat("_PlayerDistX", 5.0f);
                _renderer.material = new Material(mat);
                objectsToReload.Add(obj);
            }
            else
            {
                float dist = Mathf.Abs(player.transform.position.x - obj.transform.position.x);
                dist = Mathf.Clamp(dist, mat.GetFloat("_OptimalPlayerDistance"), 5.0f);
                mat.SetFloat("_PlayerDistX", dist);
                _renderer.material = new Material(mat);

            }
        }
        objectsToUnload.Clear();

        foreach (Object obj in objectsToReload)
        {
            unloadedObjects.Remove(obj);
        }
        objectsToReload.Clear();
    }

    //private Material SetMaterial(int materialId)
    //{
    //    switch (materialId)
    //    {
    //        case 0:
    //            return larchMat;
    //        case 1:
    //            return oakMat;
    //        default:
    //            return larchMat;
    //    }
    //}
}
