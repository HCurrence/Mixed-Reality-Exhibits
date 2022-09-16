using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;

public class EditMenuController : MonoBehaviour
{

    public GameObject spawnObj;
    public GameObject user;

    private GameObject instanceObj;

    private string anchorID;
    private CloudSpatialAnchor currentCloudAnchor;
    
    private SpatialAnchorManager anchorManager;

    void Awake()
    {
        anchorID = "";
        currentCloudAnchor = null;
        instanceObj = null;
    }

    private async void StartAnchorSession()
    {
        await anchorManager.StartSessionAsync();
    }

    private void LocateAnchors()
    {
        if(!PlayerPrefs.HasKey("anchorIdentifier"))
        {
            return;
        }
        
        anchorID = PlayerPrefs.GetString("anchorIdentifier");

        Debug.LogError("In LocateAnchors() - Identifier: " + anchorID);

        AnchorLocateCriteria locateCriteria = new AnchorLocateCriteria();

        Debug.LogError("ASA - Creating Watcher...");

        //TODO: Figure out how to store anchor identifiers.
        locateCriteria.Identifiers = new string[]{anchorID};
        anchorManager.Session.CreateWatcher(locateCriteria);

        Debug.LogError("ASA - Watcher Created!");
    }

    private void SpatialAnchorManager_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        Debug.LogError($"ASA - Possible Anchor {args.Identifier} {args.Status}");

        if(args.Status == LocateAnchorStatus.Located)
        {
            UnityDispatcher.InvokeOnAppThread( () =>
            {
                CloudSpatialAnchor anchor = args.Anchor;

                //TODO: Figure out how to spawn model from Anchor properties
                GameObject obj = (GameObject)Resources.Load("/Prefabs/" + anchor.AppProperties[@"model-type"]);

                obj.AddComponent<CloudNativeAnchor>().CloudToNative(anchor);
            });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anchorManager = GetComponent<SpatialAnchorManager>();
        anchorManager.LogDebug += (sender, args) => Debug.Log($"ASA - Debug: {args.Message}");
        anchorManager.Error += (sender, args) => Debug.LogError($"ASA - Error: {args.ErrorMessage}");
        anchorManager.AnchorLocated += SpatialAnchorManager_AnchorLocated;

        StartAnchorSession();

        //TODO: Check if anchors already exist in the workspace
        LocateAnchors();
    }


    public void spawnObject()
    {
        if(GameObject.Find(spawnObj.name) == null)
        {
            Quaternion towardsHead = Quaternion.LookRotation(user.transform.position - Vector3.zero, Vector3.up);
            Vector3 spawnPos = user.transform.position + user.transform.forward;
            instanceObj = GameObject.Instantiate(spawnObj, spawnPos, towardsHead);
        }
    }

    public async void deSpawnObject()
    {
        GameObject obj = GameObject.Find(spawnObj.name);
        if(obj != null)
        {
            //TODO: If the object has an assocaited spatial anchor, remove the anchor.
            CloudNativeAnchor anchor = obj.GetComponent<CloudNativeAnchor>();
            
            if(anchor != null)
            {
                CloudSpatialAnchor spatialAnchor = anchor.CloudAnchor;

                await anchorManager.DeleteAnchorAsync(spatialAnchor);
            }

            Destroy(obj);
        }
    }

    private async Task CreateAnchor(GameObject obj)
    {
        //TODO: Check if the anchor already exists
        CloudNativeAnchor localAnchor = obj.GetComponent<CloudNativeAnchor>();

        if(localAnchor != null)
        {
            await localAnchor.NativeToCloud();
        }
        else
        {
            localAnchor = obj.AddComponent<CloudNativeAnchor>();
            if(localAnchor.CloudAnchor == null)
            {
                await localAnchor.NativeToCloud();
            }
        }
        
        // Save Anchor Info
        currentCloudAnchor = localAnchor.CloudAnchor;
        anchorID = currentCloudAnchor.Identifier;

        // Configure anchor to save the prefab model of the object.
        currentCloudAnchor.AppProperties[@"model-type"] = @"CoffeeCup";
        currentCloudAnchor.AppProperties[@"label"] = @"Coffee Cup";

        //Wait until the manager has enough enviornmental data to create the anchor
        while(!anchorManager.IsReadyForCreate)
        {
            //Do Nothing; could display create progress
        }

        // Creates the anchor
        await anchorManager.CreateAnchorAsync(currentCloudAnchor);

        // Makes Sure the Anchor was successfully created.
        bool saveSucceeded = currentCloudAnchor != null;
        if (!saveSucceeded)
        {
            Debug.LogError("ASA - Failed to save, but no exception was thrown.");
            return;
        }

        // Saves the AnchorID for future use.
        PlayerPrefs.SetString("anchorIdentifier", anchorID);
    }

    public async void saveAnchors() => await CreateAnchor(instanceObj);

    public void previewLayout()
    {
        //Stop the current session and go to the preview scene
        if(anchorManager.IsSessionStarted)
        {
            anchorManager.DestroySession();
        }
        SceneManager.LoadScene("PreviewExhibitTest");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private async void cleanUpAnchors()
    {
        GameObject obj = GameObject.Find(spawnObj.name);
        if(obj != null)
        {
            //TODO: If the object has an assocaited spatial anchor, remove the anchor.
            CloudNativeAnchor anchor = obj.GetComponent<CloudNativeAnchor>();
            
            if(anchor != null)
            {
                CloudSpatialAnchor spatialAnchor = anchor.CloudAnchor;

                await anchorManager.DeleteAnchorAsync(spatialAnchor);
            }
        }
    }

    void OnDestroy()
    {
        cleanUpAnchors();
    }
}
