# Loading custom maps in Escape from Tarkov (stalker mod)
 An attempt to move stalker location into Esacpe From Tarkov

<h2>Step 0: Preparations</h2>
You will need: <br>
1. Unity 2018 (EFT using 2018 version). Download UnityHub from https://unity.com/ and install any 2018 version. I use 2018.4.35f1<br>
2. Blender https://www.blender.org/ <br>
3. DnSpy -  .NET debugger and assembly editor. https://github.com/dnSpy/dnSpy <br>
4. Levels you want to import. I used Levels from S.T.A.L.K.E.R. Clear Sky https://p3dm.ru/files/architecture/other_architecture/4923-levels-from-s.t.a.l.k.e.r.-clear-sky-.html <br>
5. Autodesk 3DS max or thing that can work with .max models (only if you want to use models from step 4, otherwrise blender will be enough). <br>
<br>
<h2>Step 1: Creating a simple Unity3D asset bundle</h2><br>
<ol>
  <li>Create a new unity3d 2018 project.</li>
  <li>Create folder "Editor" and move CreateAssetBundles.cs inside (%ProjectDirectory%/Assets/Editor/CreateAssetBundles.cs)</li>
  <li>Create cube with name "Kordon" in a scene and scale it (1000, 1 , 1000), then drug and drop it inside testbundle to create a prefab.</li>
  <li>Select prefab in project window. In bottom right corner set AssetBundle to testbundle.</li>
  <li>Click KLESKBY/Build AseetBundles in toolbar.</li>
</ol> 
Full asset bundle tutorials: https://learn.unity.com/tutorial/introduction-to-asset-bundles
<br>
<h2>Step 2: Making EFT load our bundle</h2>
To make the game load your bundle we need to add some code that will do it. We may create our custom DLL and load it inside the game via Harmony or NLOG, but modofying original code is more simple to me.<br>
<ol>
	<li>Open DnSpy.</li>
	<li>Open %PathToTarkov%\EscapeFromTarkov_Data\Managed\Assembly-CSharp.dll in it.</li>
	<li>Find some script that is always running. I like to use EFT.CameraControl.PlayerCameraController </li>
	<li>Find Update() method, right click on it > Edit method (C#) and add code from LoaderExample.cs</li>
	<li>Click compile, then click File > Save module</li>
	<li>Launch the game, go to raid, press F11 when you finally join the game. You should be able to see your cube, if not check console (` key) for errors </li>
</ol>
<br>
<h2>Step 3: Converting stalker levels</h2>
<ol>
	<li>Convert .max level dump to .fbx using 3ds max or other software.</li>
	<li>Scale model by 0.65 to better fit EFT player size (optional)</li>
	<li>Install Blender plugins from this git (move .py files to %BlenderInstallLocation%\2.91\scripts\addons). </li>
	<li>Launch blender, import level fbx. Click on Object>RemoveEverythingContains. This should delete all terrain objects.</li>
	<li>Export it as .fbx and import inside unity.</li>
	<li>Place it as a child object of the cube.</li>
	<li>Move FixStalkerMaterails.cs inside "Editor" folder(%Unity3DProjectDirectory%/Assets/Editor/FixStalkerMaterails.cs)</li>
	<li>Copy stalker textures inside your assets to fix stalker structure.</li>
	<li>Create a "Materials" folder. Inside this folder create folders with stalker texture names ex: %Unity3DProjectDirectory%/Assets/Materials/Bricks/</li>
	<li>Click KLESKBY/Fix stalker materials in the toolbar. Most of the white untextured models should become texture now.</li>
</ol>	
<h2>Step 4: Converting stalker terrain to Unity3d terrain</h2>
<ol>
	<li>Launch blender, import level fbx. Click on Object > RemoveEverythingBut (Top left corner). This should delete all non-terrain objects. Select everything that is left and combine meshes (Press Ctrl + J). Now you should have single terrain object.</li>
	<li>Export it as .fbx and import inside unity.</li>
	<li>Place it as a child object of the cube.</li>
	<li>Move Mesh2Terrain.cs inside "Editor" folder(%Unity3DProjectDirectory%/Assets/Editor/Mesh2Terrain.cs)</li>
	<li>Select your mesh terrain object. Click KLESKBY/Mesh2Terrain. You should now see a new unity3d terrain.</li>
	<li>Move and edit it a bit to fit the original terrain.</li>
</ol>	

<h2>Step 5: Finalizing </h2>
<ol>
	<li>Create AudioSource, enable "loop", "Play on Awake". Make it play the ambient song. For stalker this should fit: https://www.youtube.com/watch?v=V92zVorYaqo</li>
	<li>Spawn trees, grass, brushes. Add some light, decals, etc.</li>
	<li>Create prefab like in Step 1</li>
	<li>Bake NavMesh Window>AI>Naigation>Bake</li>
	<li>Bake occlusion culling Window>Rendering>Occlusion culling>Bake</li>
	<li>Place it as a child object of cube.</li>
	<li>Move Mesh2Terrain.cs inside "Editor" folder(%Unity3DProjectDirectory%/Assets/Editor/Mesh2Terrain.cs)</li>
	<li>Select your mesh terrain object. Click KLESKBY/Mesh2Terrain. You should see now a new unity3d terrain.</li>
	<li>Move and edit it a bit to fir the origian terrain.</li>
</ol>	

<h2>Problems and ToDo list:</h2>
However it is not that easy and to make it work noramly you need a good understanding how tarkov, Unity3d works and enough reverse engineering experance.
<ol>
	<li>Something teleports bots back to normal navmesh/terrain even though I delete it. Maybe instead of removing terrain we can just remove props and add our own models?</li>
	<li>A lot of errors that make a significant performance impact.</li>
	<li>No bullet impact effect (need to create ballistic colliders). It is very dreary.</li>
	<li>Idk how to create loot and loot pools</li>
	<li>Moving exit triggers are not quite accurate. Idk how to add my own exits.</li>
	<li>Idk how to create bot spawn zones.</li>
</ol>	
Instead of creating mods I decided to work on my own game so most likely I will not continue this unless someone helps me.