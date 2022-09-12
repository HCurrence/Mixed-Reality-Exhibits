# Mixed-Reality-Exhibits

## Goals

Create a MR application that allows users to place 3D holograms of statues or other exhibits at a location, add exhibit information, and display them to the public as an interactive 3D experience. 

Goals:
1. Create an interface for users to be able to place 3D holograms of historic artifacts.
2. Save the location and model of those artifacts via Cloud Spatial Anchors and a database
3. Recall

## Use-Case Research

- [AR Glass]()
   - [Monument Avenue Tour]()
- Published Research:
   - (TBA)

## Design

### Virtual Plaques/Podiums Inspired by Horizon Zero Dawn's "Vantage Points"
Example Vantage Point:
- https://miro.medium.com/max/1400/0*PWEMRBmnBb-qn9N_. 
- https://static.gosunoob.com/img/1/2017/02/Horizon-Zero-Dawn-Where-to-Find-Vantage-Points.jpg

### The Phasing-In of Objects Inspired by Bioshock Infinite's "Tears"
Example Tear:
- https://cdnb.artstation.com/p/assets/images/images/011/033/947/large/chx-king-tears-hangpointsetpreview.jpg?1527530378
- https://cdna.artstation.com/p/assets/images/images/011/033/948/large/chx-king-tears-mgturret.jpg?1527530380
- https://static.wikia.nocookie.net/bioshock/images/f/f5/BI_Shanty_Tear_Foods.png/revision/latest/scale-to-width-down/1000?cb=20151207112327
- https://static.wikia.nocookie.net/bioshock/images/b/bd/BI_MusicMarch_Tear.png/revision/latest/scale-to-width-down/1000?cb=20151207121220
- https://bioshock.fandom.com/wiki/Tear#In-Game_Events

## Research Docs

### World Locking
- [World Locking](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/spatial-anchors-in-unity?tabs=anchorstore)
- [World Locking and Azure Spatial Anchors](https://docs.microsoft.com/en-us/mixed-reality/world-locking-tools/documentation/howtos/wlt_asa)

### Spatial Anchors
- [Introductory Video to Azure Spatial Anchors](https://docs.microsoft.com/en-us/shows/mixed-reality/intro-to-azure-mixed-reality-services-azure-spatial-anchors)
- [Azure Spatial Anchors API](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.spatialanchors?view=spatialanchors-dotnet)
- [Microsoft's Documentation on Azure Spatial Anchors](https://docs.microsoft.com/en-us/azure/spatial-anchors/)

#### Contradictory Coding Instructions/Methods
- [Coding Instructions on Creating Spatial Anchors in Unity](https://docs.microsoft.com/en-us/azure/spatial-anchors/how-tos/create-locate-anchors-unity)
   - Uses the CloudAnchorSession
- [Microsoft Spatial Anchors using OpenXR sample](https://github.com/microsoft/OpenXR-Unity-MixedReality-Samples/blob/main/AzureSpatialAnchorsSample/Assets/Scripts/SpatialAnchorsSample.cs) [Current Coding Basis]
   - Need to confirm that this one works in Unity
   - Uses Spatial Anchor Manager
   - AR Session and AR Session Origin are empty gameobjects in the scene
      - AR Session Origin contains the Origin and Manager Script, and is the parent to an AR Camera
   - Uses MRTK and OpenXR
- [Quickstart Sample for Spatial Anchors in Unity](https://github.com/Azure/azure-spatial-anchors-samples/blob/master/Unity/Assets/AzureSpatialAnchors.Examples/Scripts/AzureSpatialAnchorsBasicDemoScript.cs)
   - Can confirm that this one works in Unity
   - Uses Spatial Anchor Manager
   - Does not use MRTK

#### Working Method
- Used ASA with MRTK and OpenXR using the sample as a coding basis.
- This method is bugged initally with the SpatialAnchorManager script believing it was not set-up correctly. The fix for this can be found below.

#### Notes:
- [Fix for "Not configured properly" Spatial Anchor Manager error](https://github.com/Azure/azure-spatial-anchors-samples/issues/348)

### Phasing Objects In
- [Youtube Tutorial](https://www.youtube.com/watch?v=taMp1g1pBeE)

### Connecting Multiple Users to a Scene
- [Microsoft Tutorial on Connecting Multiple Users](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-sharing-03)
