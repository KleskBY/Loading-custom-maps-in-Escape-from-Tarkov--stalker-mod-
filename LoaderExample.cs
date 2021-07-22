if (Input.GetKeyDown(KeyCode.F11))
{
	//Remove all old terrains
	Terrain[] array = UnityEngine.Object.FindObjectsOfType<Terrain>();
	for (int i = 0; i < array.Length; i++)
	{
		UnityEngine.Object.Destroy(array[i].gameObject);
	}
	
	//Remove GPUInstancerTerrainManager
	GPUInstancerTerrainManager[] array2 = UnityEngine.Object.FindObjectsOfType<GPUInstancerTerrainManager>();
	for (int j = 0; j < array2.Length; j++)
	{
		UnityEngine.Object.Destroy(array2[j].gameObject);
	}
	//Remove GPUInstancerManager
	GPUInstancerManager[] array3 = UnityEngine.Object.FindObjectsOfType<GPUInstancerManager>();
	for (int k = 0; k < array3.Length; k++)
	{
		UnityEngine.Object.Destroy(array[k].gameObject);
	}
	
	//remove stuff that we don't need (prors/other models);
	GameObject[] array4 = UnityEngine.Object.FindObjectsOfType<GameObject>();
	for (int l = 0; l < array4.Length; l++)
	{
		if (array4[l].name.ToLower().Contains("tree") || array4[l].name.ToLower().Contains("bush") || array4[l].name.ToLower().Contains("rock") || array4[l].name.ToLower().Contains("trash") || array4[l].name.ToLower().Contains("garbage") || array4[l].name.ToLower().Contains("decal"))
		{
			UnityEngine.Object.Destroy(array4[l]);
		}
	}
	
	string name = "Kordon";		//Name of our parent object
	string path = "testbundle"; //Name of our bundle
	AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, path));
	if (assetBundle == null)
	{
		Debug.LogError("Failed to load AssetBundle!");
		return;
	}
	GameObject myshit = UnityEngine.Object.Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(name));
	myshit.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z);
	assetBundle.Unload(false);
	// GameObject gameObject = this.myshit.transform.Find("exit").gameObject;
	// ScavExfiltrationPoint scavExfiltrationPoint = base.gameObject.AddComponent<ScavExfiltrationPoint>();
	// base.gameObject.AddComponent<ExfiltrationPoint>();
	// scavExfiltrationPoint.EligibleIds.Add("AID56129427038474990lEef");
	// scavExfiltrationPoint.EligibleIds.Add("56129427038474990lEef");
	// scavExfiltrationPoint.EligibleIds.Add("56129427038474990");
}


//Noclip cheat (for debug)
//
//Add private bool noclip; to class

if (Input.GetKeyDown(KeyCode.F12))
{
	this.noclip = !this.noclip;
}
if (Input.GetKey(KeyCode.Space) && this.noclip)
{
	base.transform.root.position = new Vector3(base.transform.root.position.x, base.transform.root.position.y + 1f, base.transform.root.position.z);
	if (base.transform.root.GetComponent<CharacterController>())
	{
		base.transform.root.GetComponent<CharacterController>().enabled = false;
	}
	if (base.transform.root.GetComponent<Rigidbody>())
	{
		base.transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
if (Input.GetKey(KeyCode.LeftControl) && this.noclip)
{
	base.transform.root.position = new Vector3(base.transform.root.position.x, base.transform.root.position.y - 1f, base.transform.root.position.z);
}
if (Input.GetKey(KeyCode.LeftShift) && this.noclip)
{
	base.transform.root.position = base.transform.root.position + base.transform.root.forward * 20f * Time.deltaTime;
}