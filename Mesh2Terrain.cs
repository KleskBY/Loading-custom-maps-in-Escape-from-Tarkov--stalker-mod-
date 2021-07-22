using UnityEngine;
using UnityEditor;
 
public class Mesh2Terrain : EditorWindow 
{
	//Code not by me, I just optimized and fixed it a little bit.
	private static int resolution = 1024;
	[MenuItem("KLESKBY/Object to Terrain", false, 9998)] static void OnClick () 
	{
		//EditorWindow.GetWindow<Mesh2Terrain>(true);
		if(Selection.activeGameObject == null)
		{
			Debug.LogError("Select an object you want to convert to terrain");
			return;
		}
		else
		{
			TerrainData terrain = new TerrainData();
			terrain.heightmapResolution = resolution;
			GameObject terrainObject = Terrain.CreateTerrainGameObject(terrain);
		
			MeshCollider collider = Selection.activeGameObject.GetComponent<MeshCollider>();
			if(!collider) collider = Selection.activeGameObject.AddComponent<MeshCollider>();

			Bounds bounds = collider.bounds;	
			terrain.size = collider.bounds.size;
			bounds.size = new Vector3(terrain.size.x, collider.bounds.size.y, terrain.size.z);
	
			// Do raycasting samples over the object to see what terrain heights should be
			float[,] heights = new float[terrain.heightmapWidth, terrain.heightmapHeight];	
			Ray ray = new Ray(new Vector3(bounds.min.x, bounds.max.y + bounds.size.y, bounds.min.z), -Vector3.up);
			RaycastHit hit = new RaycastHit();
			float meshHeightInverse = 1 / bounds.size.y;
			Vector3 rayOrigin = ray.origin;
	
			int maxHeight = heights.GetLength(0);
			int maxLength = heights.GetLength(1);
	
			Vector2 stepXZ = new Vector2(bounds.size.x / maxLength, bounds.size.z / maxHeight);
	
			for(int i = 0; i < maxHeight; i++)
			{
				for(int j = 0; j < maxLength; j++)
				{
					float height = 0.0f;
	
					if(collider.Raycast(ray, out hit, bounds.size.y * 3)){
	
						height = (hit.point.y - bounds.min.y) * meshHeightInverse;
						height = height * (collider.bounds.size.y / collider.bounds.size.y); //sizeFactor
						//clamp
						if(height < 0) height = 0;
						if(height > 100f) height = 100f;
					}
	
					heights[i, j] = height;
					rayOrigin.x += stepXZ[0];
					ray.origin = rayOrigin;
				}
	
				rayOrigin.z += stepXZ[1];
				rayOrigin.x = bounds.min.x;
				ray.origin = rayOrigin;
			}
	
			terrain.SetHeights(0, 0, heights);
			AssetDatabase.CreateAsset(terrain, "Assets/GeneratedTerrain.asset");
		}
	}

	[MenuItem("KLESKBY/All terrains to asset", false, 9999)] static void OnClick2() 
	{
		TerrainData[] terrainDatas = FindObjectsOfType<TerrainData>();
		for(int i = 0; i < terrainDatas.Length; i++)
		{
			AssetDatabase.CreateAsset(terrainDatas[i], "KLESKBY/GeneratedTerrain" + i.ToString() + ".asset");
		}
	}
}