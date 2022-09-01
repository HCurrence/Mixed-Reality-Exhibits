using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;

public class buttonController : MonoBehaviour
{

    public GameObject spawnObj;
    public GameObject user;

    private GameObject instanceObj;

    private string anchorID;
    private CloudSpatialAnchor currentCloudAnchor;
    
    private CloudSpatialAnchorSession cloudSession;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if(cloudSession == null)
            {
                cloudSession = new CloudSpatialAnchorSession();
            }

            //Configure Session
            cloudSession.Configuration.AccountKey = @"fgC3YahtNiYD7qZvU+Goib7tZiS8VTDgFP0V6CB1b2g=";

            //Start Session
            cloudSession.Start();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }

        anchorID = "";
        currentCloudAnchor = null;
        instanceObj = null;
    }

    public void spawnObject()
    {
        if(GameObject.Find(spawnObj.name) == null)
        {
            Quaternion towardsHead = Quaternion.LookRotation(user.transform.position - Vector3.zero, Vector3.up);
            instanceObj = GameObject.Instantiate(spawnObj, user.transform.position, towardsHead);

#if WINDOWS_UWP
            CloudNativeAnchor localAnchor = instanceObj.AddComponent<CloudNativeAnchor>();
            
            if(localAnchor.CloudAnchor == null)
            {
                await localAnchor.NativeToCloud();
            }
            
            
            currentCloudAnchor = localAnchor.CloudAnchor;
            anchorID = currentCloudAnchor.Identifier;

            currentCloudAnchor.AppProperties[@"model-type"] = @"CoffeeCup";
            currentCloudAnchor.AppProperties[@"label"] = @"Coffee Cup";

            await cloudSession.CreateAnchorAsync(currentCloudAnchor);
#endif

        }
    }

    public void deSpawnObject()
    {
        GameObject obj = GameObject.Find(spawnObj.name);
        if(obj != null)
        {
            Destroy(obj);
        }
    }

    public void saveAnchors()
    {
        // Save the position of the gameObject as an anchor
        // Can the object itself be saved as well?

#if WINDOWS_UWP
        if(instanceObj != null)
        {
            CloudNativeAnchor localAnchor = instanceObj.AddComponent<CloudNativeAnchor>();
            if(localAnchor.CloudAnchor == null)
            {
                /*await*/ localAnchor.NativeToCloud();
            }
            
            
            currentCloudAnchor = localAnchor.CloudAnchor;
            anchorID = currentCloudAnchor.Identifier;

            currentCloudAnchor.AppProperties[@"model-type"] = @"CoffeeCup";
            currentCloudAnchor.AppProperties[@"label"] = @"Coffee Cup";

            // if (!cloudSession.GetNearbyAnchorIdsAsync().contains(currentCloudAnchor.Identifier))
            /*await*/ cloudSession.CreateAnchorAsync(currentCloudAnchor);
            // else
            // cloudSession.UpdateAnchorPropertiesAsync(currentCloudAnchor);
        }
#endif
    }

    public void previewLayout()
    {
        //Go to the preview scene
        SceneManager.LoadScene("PreviewExhibitTest");
    }

    // Update is called once per frame
    void Update()
    {
        //if the scene changes to the preview scene
        //CloudManager.StopSession();
    }
}
