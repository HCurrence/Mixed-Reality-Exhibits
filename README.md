# Mixed-Reality-Exhibits

## Table of Contents:
1. [Project Goal](https://github.com/HCurrence/Mixed-Reality-Exhibits/blob/main/README.md#project-goal)
2. [Use-Case Research](https://github.com/HCurrence/Mixed-Reality-Exhibits/blob/main/README.md#use-case-research)
3. [Design](https://github.com/HCurrence/Mixed-Reality-Exhibits/blob/main/README.md#design)
4. [Milestone Schedule](https://github.com/HCurrence/Mixed-Reality-Exhibits/blob/main/README.md#milestones)
5. [Research and Development Notes](https://github.com/HCurrence/Mixed-Reality-Exhibits/blob/main/README.md#research-and-development)

## Project Goal

Create a MR application that allows users to place 3D holograms of statues or other exhibits at a location, add exhibit information, and display them to the public as an interactive 3D experience. 

Goals:
1. Create an interface for users to be able to place 3D holograms of historic artifacts.
2. Save the location and model of those artifacts via Cloud Spatial Anchors and a database
3. Recall those spatial

## Use-Case Research

- [AR Glass]()
   - [Monument Avenue Tour]()
- Published Research:
   - (TBA)

## Design 

There are 3 primary modes to the application:
- **Edit Mode**: Where the user can place historic artifacts and edit informational displays.
- **Preview Mode**: Where the user can view any changes they saved in Edit Mode.
- **Live Mode**: Where any users can view the current layout of the virtual exhibit.

### Virtual Plaques/Podiums Inspired by Horizon Zero Dawn's "Vantage Points"
Example Vantage Point:
![Unactivated Vantage](https://oyster.ignimgs.com/mediawiki/apis.ign.com/horizon-zero-dawn/e/e7/Vantage_01-3_ExplorerMuseum_03-A.jpg)
![Vantage Example 1](https://miro.medium.com/max/1400/0*PWEMRBmnBb-qn9N_.)
![Vantage Example 2](https://static.gosunoob.com/img/1/2017/02/Horizon-Zero-Dawn-Where-to-Find-Vantage-Points.jpg)

### The Phasing-In of Objects Inspired by Bioshock Infinite's "Tears"
Example Tear:
![Tear Example 1](https://cdnb.artstation.com/p/assets/images/images/011/033/947/large/chx-king-tears-hangpointsetpreview.jpg?1527530378)
![Tear Example 2](https://cdna.artstation.com/p/assets/images/images/011/033/948/large/chx-king-tears-mgturret.jpg?1527530380)
![Tear Example 3](https://static.wikia.nocookie.net/bioshock/images/f/f5/BI_Shanty_Tear_Foods.png/revision/latest/scale-to-width-down/1000?cb=20151207112327)
![Tear Example 4](https://static.wikia.nocookie.net/bioshock/images/b/bd/BI_MusicMarch_Tear.png/revision/latest/scale-to-width-down/1000?cb=20151207121220)
[About Tears](https://bioshock.fandom.com/wiki/Tear#In-Game_Events)

### Menu to Spawn Multiple Objects Insides by The Walking Dead's Inventory UX
![Inventory Example](https://cdnb.artstation.com/p/media_assets/images/images/000/558/853/original/Backpack_UI.gif?1587428037)

## Milestones

| Milestone | Description | Date | Date Achieved |
| --- | --- | --- | --- |
| 1. Edit Mode Functionality | The editor scene enables an object to be spawned by the user. The user can then manipulate the position and rotation of that object and save its position and rotation for later use. The user can also remove the object, if necessary. | N/A | 9/12/2022 |
| 2. Preview Mode Functionality | The Editor can transition the user to a "Preview Mode". In Preview Mode, the editor can view the position and rotation of the object they manipulated in Edit Mode. Any changed published in Edit Mode should be reflected in Preview Mode.| 10/1/2022 | TBD |
| 3. Persistant Anchors | Anchors are currently bound to the session they were created in, but different sessions are started when transistioning between scenes. Different sessions will also need to be started for other users to share the application. This means anchors will need to be [persistant across sessions and devices](https://docs.microsoft.com/en-us/azure/spatial-anchors/tutorials/tutorial-share-anchors-across-devices?tabs=azure-portal%2CVS%2CUnityHoloLens). | 10/15/2022 | TBD |
| 4. Spawn and Manipulate Multiple Objects in Edit Mode | Will need to add the functionality for the user to spawn and save the location of more than one object. | 10/31/2022 | TBD |
| 5. Add the Capability for Certian Objects to be Snapped to the Ground in Edit Mode | Certian objects, like statues, will need to be and should be attached to the ground. Other objects may also benefit from an optional, On-And-Off button for this. (See [Solver](https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk2/features/ux-building-blocks/solvers/solver?view=mrtkunity-2022-05).) | 11/15/2022 | TBD | 
| 6. Virtual Informational Plaques and Podiums | Informational Plaques and Podiums should be paired with each object spawned into the scene. The plaques and podiums should be able to be manipulated about the scene as well. Information should be added to the plaque/podium in Edit Mode and viewed in Preview Mode. | 11/30/2022 | TBD |
| 7. Aesthetics | In preview mode, objects and thier info plaques/podiums should be hidden from view and replaced with an activation marker. Once the marker is activated, the object should phase-in to the scene and the marker would transform into the info plaque/podium. Once the information is closed, the object should phase out of view and the info plaque/podium should transform back into an activation marker. (See Design Photos for reference.) | 12/15/2022 | TBD |
| 8. Live Mode Functionality | Live Mode should have all the capabilities of Preview Mode, but Live Mode will also have multiplayer functionality. Multiple Hololens users should be able to view and interact with the objects in the scene at the same time. Any changed published in Edit Mode should be reflected in Live Mode. | 1/15/2023 | TBD |

## Research and Development

### Spatial Anchors
- [Introductory Video to Azure Spatial Anchors](https://docs.microsoft.com/en-us/shows/mixed-reality/intro-to-azure-mixed-reality-services-azure-spatial-anchors)
- [Azure Spatial Anchors Unity SDK](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.spatialanchors?view=spatialanchors-dotnet)
- [Microsoft's Documentation on Azure Spatial Anchors](https://docs.microsoft.com/en-us/azure/spatial-anchors/)

#### Contradictory Coding Instructions/Methods
- [Coding Instructions on Creating Spatial Anchors in Unity](https://docs.microsoft.com/en-us/azure/spatial-anchors/how-tos/create-locate-anchors-unity)
   - Uses the CloudAnchorSession
- [Microsoft Spatial Anchors using OpenXR sample](https://github.com/microsoft/OpenXR-Unity-MixedReality-Samples/blob/main/AzureSpatialAnchorsSample/Assets/Scripts/SpatialAnchorsSample.cs) [Current Coding Basis]
   - Uses Spatial Anchor Manager
   - Uses MRTK and OpenXR
- [Quickstart Sample for Spatial Anchors in Unity](https://github.com/Azure/azure-spatial-anchors-samples/blob/master/Unity/Assets/AzureSpatialAnchors.Examples/Scripts/AzureSpatialAnchorsBasicDemoScript.cs)
   - Uses Spatial Anchor Manager
   - Does not use MRTK

#### Working Method
- Used ASA with MRTK and OpenXR using the sample as a coding basis.
- The SpatialAnchorManager script has a threading issue where the checks for if the ARSessionOrigin and ARSessionManager exist fail.
   - Could be a threading issue.
   - To fix, pull the ARSessionOrigin and the ARSessionManager again in the ValidConfiguration method in the SpatialAnchorManager script. (See code below.)
   - [Original Fix](https://github.com/Azure/azure-spatial-anchors-samples/issues/348)
   
```CSharp
protected async virtual Task<bool> IsValidateConfiguration()
{

   /* Code */

   // Just Before the Check Happens...
   arSessionOrigin = FindObjectOfType<ARSessionOrigin>(); //HAD TO ADD THIS TO GET IT TO PULL THE ORIGIN PROPERLY - HALEY
   arAnchorManager = FindObjectOfType<ARAnchorManager>(); //HAD TO ADD THIS TO GET IT TO PULL THE MANAGER PROPERLY - HALEY

   // All applications must have an enabled AR Foundation ARAnchorManager and ARSessionOrigin
   // in the scene. The ARSessionOrigin object should be added automatically when the
   // ARAnchorManager is added to the scene through the Unity inspector.
   if (arSessionOrigin == null || !arSessionOrigin.enabled)
   {
       Debug.LogError("Need an enabled ARSessionOrigin in the scene");
       return false;
   }

   if (arAnchorManager == null || !arAnchorManager.enabled)
   {
       Debug.LogError("Need an enabled ARAnchorManager in the scene");
       return false;
   }

   /* Code */

}

```

### Other Tutorials
- [Youtube Tutorial on Phasing In Objects](https://www.youtube.com/watch?v=taMp1g1pBeE)
- [Microsoft Tutorial on Connecting Multiple Users](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-sharing-03)
- [Microsoft Tutorial on Persisting Anchors Across Sessions and Devices](https://docs.microsoft.com/en-us/azure/spatial-anchors/tutorials/tutorial-share-anchors-across-devices?tabs=azure-portal%2CVS%2CUnityHoloLens)
- On World Locking:
   - [World Locking](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/spatial-anchors-in-unity?tabs=anchorstore)
   - [World Locking and Azure Spatial Anchors](https://docs.microsoft.com/en-us/mixed-reality/world-locking-tools/documentation/howtos/wlt_asa)
