using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;

using UnityEngine.XR.ARFoundation;

public class PreviewMenuController : MonoBehaviour
{

    public GameObject extObj;

    private GameObject instanceObj;

    private string anchorID;
    private CloudSpatialAnchor currentCloudAnchor;
    private SpatialAnchorManager anchorManager;

    void Awake()
    {
        instanceObj = null;

        anchorID = "";
        currentCloudAnchor = null;
    }

    private async void StartAnchorSession()
    {
        await anchorManager.StartSessionAsync();
    }

    // Start is called before the first frame update
    void Start()
    {
        anchorManager = GetComponent<SpatialAnchorManager>();

        anchorManager.LogDebug += (sender, args) => Debug.Log($"ASA - Debug: {args.Message}");
        anchorManager.Error += (sender, args) => Debug.LogError($"ASA - Error: {args.ErrorMessage}");
        anchorManager.AnchorLocated += SpatialAnchorManager_AnchorLocated;

        StartAnchorSession();
        LocateAnchors();
    }

    private void LocateAnchors()
    {
        if(!PlayerPrefs.HasKey("anchorIdentifier"))
        {
            Debug.LogError("ASA - Unable to Create Watcher for the Identifier.");
            return;
        }

        AnchorLocateCriteria locateCriteria = new AnchorLocateCriteria();

        Debug.LogError("ASA - Creating Watcher.");

        string anchorID = PlayerPrefs.GetString("anchorIdentifier");

        Debug.LogError($"Found Anchor ID - {anchorID}");

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
                currentCloudAnchor = args.Anchor;

                //TODO: Figure out how to spawn model from Anchor properties

                Debug.LogError($"Attempting to Spawn Object - {"/Prefabs/" + currentCloudAnchor.AppProperties[@"model-type"]}...");

                instanceObj = (GameObject)Resources.Load("/Prefabs/" + currentCloudAnchor.AppProperties[@"model-type"]);

                instanceObj.AddComponent<CloudNativeAnchor>().CloudToNative(currentCloudAnchor);

                GameObject.Instantiate(instanceObj);
            });
        }
    }

    public void backToEditor()
    {

        //Debug.LogError("HALEY NOTE: Anchor Manager =" + anchorManager);

        while(anchorManager == null)
        {
            //throw new Exception("Somehow the manager is not here.");
            anchorManager = GetComponent<SpatialAnchorManager>();
        }

        //Stop the current session and go to the editor
        if(anchorManager.IsSessionStarted)
        {
            anchorManager.DestroySession();
        }
        
        SceneManager.LoadScene("PlaceObjectTest");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void cleanUpAnchors()
    {
        if(instanceObj != null)
        {
            //TODO: If the object has an assocaited spatial anchor, remove the anchor.
            CloudNativeAnchor anchor = instanceObj.GetComponent<CloudNativeAnchor>();
            
            if(anchor != null)
            {
                CloudSpatialAnchor spatialAnchor = anchor.CloudAnchor;

                await anchorManager.DeleteAnchorAsync(spatialAnchor);
            }
        }
    }

    async void OnDestroy()
    {
        cleanUpAnchors();
    }
}
