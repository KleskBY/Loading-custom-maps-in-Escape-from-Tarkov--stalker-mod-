using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FixStalkerMaterails : EditorWindow
{
    [MenuItem("KLESKBY/Fix stalker materials", false)]
    static void OnClick()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in objects)
        {
            string textureName = gameObject.name.Substring(gameObject.name.LastIndexOf("/") + 1, gameObject.name.Length - gameObject.name.LastIndexOf("/") - 1);
            if (textureName.IndexOf(".") > 0)
            {
                textureName = textureName.Substring(0, textureName.IndexOf(".")) + ".dds";
                textureName = textureName.Replace("\\", "/");
                Material mat = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/" + textureName + ".mat", typeof(Material));
                if (!mat)
                {
                    Texture main = (Texture)AssetDatabase.LoadAssetAtPath("Assets/textures/" + textureName, typeof(Texture));
                    if (main)
                    {
                        mat = new Material(Shader.Find("Diffuse"));
                        mat.SetTexture("_MainTex", main);
                        AssetDatabase.CreateAsset(mat, "Assets/Materials/" + textureName + ".mat");
                        Debug.Log("Creating mat " + "Assets/Materials/" + textureName + ".mat");
                    }
                }
                else Debug.Log("Using mat: " + "Assets/Materials/" + textureName + ".mat");
                if (!mat) Debug.LogError("No mat for " +gameObject.name + " " + textureName );
                gameObject.GetComponent<Renderer>().material = mat;
                gameObject.GetComponent<Renderer>().sharedMaterial = mat;
            }
        }
    }

}
