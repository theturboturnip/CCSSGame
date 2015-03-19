#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using GoLinearEditorInternal;

public class SceneShaderMapping {
	public string origShaderName;
	public string remappedShaderName;
	
	public int remappedShaderIndex;
}

public class GoLinear : EditorWindow 
{
	private List<SceneShaderMapping> shadersMapping;
	private string[] allMappingOptions = null;
	private int nonRemappedShaders = 0;
	
	private Shader shaderToFix = null;
	
	private Vector2 scrollPosition = Vector2.zero;
	private int scrollbarHeight = 0;
	
	private bool builtShadersList = false;
	
	private string msgToUser = null;
	private Color msgToUserColor;

	[MenuItem("Window/Go Linear")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow( typeof(GoLinear) );
	}
	
	void Start() {
		//availableLLShaders = new string[]{"none", "LL Diffuse", "LL Vertexlit", "LL Skybox"};
		nonRemappedShaders = 0;
		builtShadersList = false;
	}
	
    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width*height];
 
        for(int i = 0; i < pix.Length; i++)
            pix[i] = col;
 
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
 
        return result;
    }
	
	void OnGUI()
	{
		Color bgColor = new Color(135f/255f, 206f/255f, 1f);//new Color(1f, 120f / 250f, 0f);
		Color contentColor = Color.white;//new Color(1f, 220f / 250f, 0.5f);
		Color LLUIColor = new Color(30f/255f, 100f/255f, 1f);//new Color(1f, 156f / 250f, 0f);
		Color highlightBGColor = new Color(100f/255f, 150f/255f, 1f);
		GUI.backgroundColor = bgColor;
		GUI.contentColor = contentColor;
		
		GUIStyle logoLabelStyle = new GUIStyle();
		logoLabelStyle.normal.textColor = LLUIColor;
		logoLabelStyle.fontSize = 20;
		logoLabelStyle.alignment = TextAnchor.MiddleCenter;
		
		GUIStyle separatorLabelStyle = new GUIStyle();
		separatorLabelStyle.normal.textColor = Color.black;
		//separatorLabelStyle.fontSize = 20;
		
		
		int yPos = 5;
		
		GUI.Label(new Rect(10, yPos, 350, 30), "Go Linear (Mobile & Indie)", logoLabelStyle);
		
		//yPos += 20;
		
		GUI.Label(new Rect(10, yPos += 20, 350, 20), "__________________________________________________", separatorLabelStyle);

		GUIStyle mainLabelStyle = new GUIStyle();
			mainLabelStyle.normal.textColor = Color.white;
			mainLabelStyle.fontStyle = FontStyle.Bold;
			mainLabelStyle.fontSize = 15;
			mainLabelStyle.alignment = TextAnchor.MiddleLeft;
		
		yPos += 20;
		
		if (GUI.Button(new Rect(10, yPos, 295, 30), "(1) Process scene") ) {
			UnityTerrainMessage();
			msgToUser = null;
			UpdateShadersList();
		}
		
		GUI.backgroundColor = Color.red;
		
		if (GUI.Button(new Rect(305, yPos, 60, 30), "Restore") ) { 
			RestoreAll();
		}
		
		GUI.backgroundColor = bgColor;
		
		yPos += 35;
		
	
		if (shadersMapping != null) {
			if (builtShadersList) {
				if (shadersMapping.Count == 0) {
					GUI.contentColor = Color.green;
					GUI.Label(new Rect(10, yPos, 350, 20), "All of the shaders have been converted.");
					GUI.contentColor = contentColor;
					yPos += 25;
				}
			}
			
			if (shadersMapping.Count > 0) {
				int scrollBarRectHeight = Mathf.Min(scrollbarHeight, 800);
				
				scrollPosition = GUI.BeginScrollView(new Rect(10, yPos, 400, scrollBarRectHeight), scrollPosition, new Rect(0, 0, 360, scrollbarHeight), false, false);
				// GUI.BeginScrollView(new Rect(10, 300, 100, 100), scrollPosition, new Rect(0, 0, 220, 200));
				scrollbarHeight = 0;
				
				GUI.Label(new Rect(15, scrollbarHeight, 300, 20), "Shaders mapping:", mainLabelStyle);
				
				scrollbarHeight += 25;
				
				foreach (SceneShaderMapping sMapping in shadersMapping) {
					//Debug.Log(sMapping.origShaderName + " => " + sMapping.remappedShaderName + " index: " + sMapping.remappedShaderIndex);
					EditorGUI.SelectableLabel(new Rect(10, scrollbarHeight, 130, 20), sMapping.origShaderName);
					GUI.Label(new Rect(150, scrollbarHeight, 50, 20), "=>");
					if (sMapping.remappedShaderName == "none") {
						GUI.backgroundColor = highlightBGColor;
						GUI.contentColor = Color.white;
					}
					//	EditorGUI.SelectableLabel(new Rect(180, scrollbarHeight, 150, 20), sMapping.remappedShaderName);
					//} else {
						sMapping.remappedShaderIndex = EditorGUI.Popup( new Rect(180, scrollbarHeight, 150, 20), sMapping.remappedShaderIndex, allMappingOptions);
					//}
					GUI.backgroundColor = bgColor;
					GUI.contentColor = contentColor;
					
					if (GUI.Button(new Rect(335, scrollbarHeight, 20, 20), "?") ) {
						SelectAllObjectsThatUseShader(sMapping.origShaderName);
					}
					
					scrollbarHeight += 25;
				}
				
				//scrollbarHeight += 45;
				
				GUI.EndScrollView();
				
				yPos += scrollBarRectHeight;
				
				int curActionIndex = 2;
				
				//yPos += 35;
				
				if (nonRemappedShaders > 0) {
					if (GUI.Button(new Rect(15, yPos, 350, 30), "(" + (curActionIndex++) + ") Parse all") ) {
						msgToUser = "Shaders parsing may take some time,\ndepending on the amount of shaders in the scene.";
						msgToUserColor = Color.yellow;
						SaveShadersToSkip();
						ParseAll();
						//ReadShadersToSkip();
					}
					yPos += 35;
				}
				
				if (GUI.Button(new Rect(15, yPos, 350, 30), "(" + (curActionIndex++) + ") Apply gamma correction") ) {
					SaveShadersToSkip();
					RemapAll();
					//ReadShadersToSkip();
				}
				
				yPos += 35;
				
				if (!LLHelper.IsNullOrWhiteSpace( msgToUser ) ) {
					GUI.contentColor = msgToUserColor;
					GUI.Label(new Rect(15, yPos, 300, 60), msgToUser);
					GUI.contentColor = contentColor;
					
					yPos += 65;
				}
			}
		}
		
		GUI.Label(new Rect(10, yPos, 350, 20), "__________________________________________________", separatorLabelStyle);
		
		GUI.Label(new Rect(10, yPos += 20, 300, 20), "Parse custom shader:");
		shaderToFix = EditorGUI.ObjectField( new Rect(10, yPos += 25, 300, 20), shaderToFix, typeof( Shader ), true) as Shader;
		if (GUI.Button(new Rect(10, yPos += 35, 350, 30), "Parse shader") ) { 
			FixShader( shaderToFix );
		}
	}
	
	private void SaveShadersToSkip() {
		if (shadersMapping == null)
			return;
		
		foreach (SceneShaderMapping sMapping in shadersMapping) {
			if (sMapping.remappedShaderIndex == 1) {
				EditorPrefs.SetBool("golin_skip: " + sMapping.origShaderName, true);
				continue;
			}
			
			//Don't skip - remove skip state
			if ( EditorPrefs.HasKey( "golin_skip: " + sMapping.origShaderName ) ) {
				EditorPrefs.DeleteKey( "golin_skip: " + sMapping.origShaderName );
			}
		}
	}
	
	private void ReadShadersToSkip() {
		if (shadersMapping == null)
			return;
		
		foreach (SceneShaderMapping sMapping in shadersMapping) {
			if ( EditorPrefs.HasKey("golin_skip: " + sMapping.origShaderName) ) {
				sMapping.remappedShaderIndex = 1;
			}
		}
	}
	
	private void SelectAllObjectsThatUseShader(string _shaderName) {
		if ( LLHelper.IsNullOrWhiteSpace( _shaderName) )
			return;
		
		List<GameObject> selectedObjects = new List<GameObject>();
		
		Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		foreach (Renderer rend in allRenderers) {
			if (null != rend.sharedMaterial.shader) {
				if (rend.sharedMaterial.shader.name == _shaderName) {
					selectedObjects.Add(rend.gameObject);	
				}
				
			}
		}
		
		Selection.objects = selectedObjects.ToArray();
	}
	
	private void UnityTerrainMessage() {
		if ( EditorPrefs.HasKey("golin_shown_ter_msg") )
			return;
		
		Terrain ter = GameObject.FindObjectOfType( typeof(Terrain) ) as Terrain;
		
		if (null != ter) {
			EditorUtility.DisplayDialog("Go Linear", "Unity terrain '" + ter.name + "' has been found in the scene.\n" +
				"In order for the system to work correctly with terrains, please extract the contents of 'GoLinear/Shaders/Built-in/TerrainNature' and restart Unity.", "Ok");
			
			EditorPrefs.SetBool("golin_shown_ter_msg", true);
		}
	}
	
	private string[] GetAllShaderNamesInAScene() {
		List<string> allShaderNames = new List<string>();
		
		Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		foreach (Renderer rend in allRenderers) {
			if (null != rend.sharedMaterial.shader)
				allShaderNames.Add( rend.sharedMaterial.shader.name );
		}
		
		//Skyboxes
		Skybox[] allSkyboxes = GameObject.FindObjectsOfType( typeof(Skybox) ) as Skybox[];
		foreach (Skybox skybox in allSkyboxes) {
			if (null != skybox.material.shader)
				allShaderNames.Add( skybox.material.shader.name );
		}
		
		if (null != RenderSettings.skybox) {
			if (null != RenderSettings.skybox.shader)
				allShaderNames.Add( RenderSettings.skybox.shader.name );
		}
		
		return allShaderNames.ToArray();
	}
	
	private void ReplaceAllShadersInAScene(string _src, string _dst) {
		Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		foreach (Renderer rend in allRenderers) {
			if (rend.sharedMaterial.shader.name != _src)
				continue;
			
			//Store original shader name for restoring
			string matPath = AssetDatabase.GetAssetPath( rend.sharedMaterial );
			
			//Debug.Log("material " + rend.sharedMaterial.name + " path is " + matPath);
			if (null != matPath) {
				string matGUID = AssetDatabase.AssetPathToGUID( matPath );
				if ( !LLHelper.IsNullOrWhiteSpace( matGUID ) ) {
					LLEditorInternal.AddMaterialMapping(matGUID, rend.sharedMaterial.name, rend.sharedMaterial.shader.name);
				}
			}
			
			Shader dstShader = Shader.Find( _dst );
			
			if (null == dstShader) {
				Debug.LogError("Unable to find the shader '" + _dst + "'");
				continue;
			}
			
			rend.sharedMaterial.shader = dstShader;
		}
		
		//Skyboxes
		Skybox[] allSkyboxes = GameObject.FindObjectsOfType( typeof(Skybox) ) as Skybox[];
		foreach (Skybox skybox in allSkyboxes) {
			if (skybox.material.shader.name != _src)
				continue;
			
			Shader dstShader = Shader.Find( _dst );
			
			if (null == dstShader) {
				Debug.LogError("Unable to find the shader '" + _dst + "'");
				continue;
			}
			
			skybox.material.shader = dstShader;
		}
		
		if (null != RenderSettings.skybox) {
			if (RenderSettings.skybox.shader.name == _src) {
				Shader dstShader = Shader.Find( _dst );
				
				if (null == dstShader) {
					Debug.LogError("Unable to find the shader '" + _dst + "'");
				}
				
				RenderSettings.skybox.shader = dstShader;
			}
		}
	}
	
	private void ParseAll() {
		foreach (SceneShaderMapping sMapping in shadersMapping) {
			if (sMapping.remappedShaderIndex == 0) {	//Option = parse
				string shaderName = sMapping.origShaderName;
				
				Shader origShader = Shader.Find( shaderName );
				
				if (null == origShader) {
					Debug.LogError("Unable to find the shader '" + shaderName + "'");	
				}
				
				
				if ( LLEditorInternal.FixShader( origShader, false, true ) ) {			
					//int mappingIndex = -1;
					string mappingName = LLEditorInternal.GetShaderMapping( sMapping.origShaderName );
					
					//Debug.Log("mappingName: " + mappingName + "; mappingIndex: " + mappingIndex);
					
					if ( !LLHelper.IsNullOrWhiteSpace(mappingName) ) {
						//sMapping.remappedShaderIndex = mappingIndex + 2;
						sMapping.remappedShaderName = mappingName;
					}
				} else {
					sMapping.remappedShaderIndex = 1;	//Skip
				}
			}
		}
		
		BuildMappingOptions();
	}
	
	private void RemapAll() {
		foreach (SceneShaderMapping sceneShaderMapping in shadersMapping) {
			if (sceneShaderMapping.remappedShaderIndex <= 0) {
				msgToUser = "Shader '" + sceneShaderMapping.origShaderName + "' hasn't \nbeen mapped. " +
								"Either parse it, select an existing \nshader from the list (e.g. Linear Lighting/Diffuse) or skip it.";
				
				msgToUserColor = Color.yellow;
				
				Debug.LogWarning(msgToUser);
				return;
			}
		}
		
		//Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		foreach ( string curShaderName in GetAllShaderNamesInAScene() ) {
			
			foreach (SceneShaderMapping sceneShaderMapping in shadersMapping) {
				if (sceneShaderMapping.origShaderName == curShaderName) {
					//Skip?
					if (sceneShaderMapping.remappedShaderIndex == 1)
						continue;
					
					Shader dstShader = Shader.Find(sceneShaderMapping.remappedShaderName);
					
					if (null == dstShader) {
						Debug.LogError("Unable to find shader '" + sceneShaderMapping.remappedShaderName + "'.");
						sceneShaderMapping.remappedShaderIndex = 1;	//Skip
						break;
					}
					
					//rend.sharedMaterial.shader = dstShader;
					//curShader = dstShader;
					ReplaceAllShadersInAScene(curShaderName, sceneShaderMapping.remappedShaderName);
						
					break;
				}
			}	
		}
		
		UpdateShadersList();
	}
	
	private void RestoreAll() {
		//Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		//foreach (Renderer rend in allRenderers) {
//		foreach ( string curShaderName in GetAllShaderNamesInAScene() ) {
//			
//			//Skip non - GoLinear shaders
//			if ( !curShaderName.Contains("Linear Lighting/") )
//				continue;
//			
//			string srcShaderName = LLEditorInternal.GetSrcShaderMappingFromDst( curShaderName );
//			
//			if (null == srcShaderName) {
//				Debug.LogWarning("Unable to find reverse mapping for shader " + curShaderName);
//				continue;
//			}
//			
//			Shader srcShader = Shader.Find( srcShaderName );
//			
//			if (null == srcShader) {
//				Debug.LogError("Unable to find the reverse mapping shader '" + srcShaderName + "'");
//				continue;
//			}
//			
//			ReplaceAllShadersInAScene(curShaderName, srcShaderName);
//		}
		
		Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		foreach (Renderer rend in allRenderers) {
			string matPath = AssetDatabase.GetAssetPath( rend.sharedMaterial );
			
			//Debug.Log("material " + rend.sharedMaterial.name + " path is " + matPath);
			if (null != matPath) {
				string matGUID = AssetDatabase.AssetPathToGUID( matPath );
				if ( !LLHelper.IsNullOrWhiteSpace( matGUID ) ) {
					string originalShaderName = LLEditorInternal.GetOrigMaterialShader( matGUID );
					
					if ( !LLHelper.IsNullOrWhiteSpace( originalShaderName ) ) {
						Shader origShader = Shader.Find( originalShaderName );
						
						if (null != origShader ) {
							rend.sharedMaterial.shader = origShader;
						} else {
							Debug.LogWarning("Unable to find shader '" + originalShaderName + "'. Skipping.");
						}
						
					} else {
						//Debug.LogError("Material '" + rend.sharedMaterial.name + "' originalShaderName is null. Skipping.");	
					}
				} else {
					Debug.LogWarning("GUID of material '" + rend.sharedMaterial.name + "' is null. Skipping.");	
				}
			}
		}
		
		UpdateShadersList();
	}
	
	private void FixShader(Shader _shader) {
		if (null == _shader) {
			//ParseAllShadersInDirectory( LLHelper.AssetPathToFilePath("Assets/GoLinear/Shaders/TerrainNature/TerrainShaders") );
			return;
		}
		
		bool res = LLEditorInternal.FixShader( _shader, false, true );
		
		if (!res) {
			Debug.LogError("Failed to fix the shader '" + _shader.name + "'");	
		}
	}
	
	private void ParseAllShadersInDirectory(string _path) {
		System.DateTime startTime = System.DateTime.Now;
		if ( !System.IO.Directory.Exists(_path) ) {
			Debug.LogError("Directory at path '" + _path + "' doesn't exist.");
			return;
		}
		
		string[] shadersPaths = System.IO.Directory.GetFiles(_path, "*.shader", System.IO.SearchOption.AllDirectories);

		if (shadersPaths.Length <= 0) {
			Debug.LogError("No shaders found at path '" + _path + "'.");
			return;
		}
		
		List<string> failedShaders = new List<string>();
		
		foreach (string shaderPath in shadersPaths) {
			string shaderAssetPath = LLHelper.FilePathToAssetPath( shaderPath );
			Debug.Log("Fixing shader '" + shaderAssetPath + "'");
			bool res = LLEditorInternal.FixShaderAtAssetPath( shaderAssetPath, false, true );
			
			if (!res) {
				Debug.LogError("Failed shader " + shaderAssetPath);
				failedShaders.Add( shaderPath );
			}
			//break;
		}
		
		Debug.Log("Done fixing shaders at '" + _path + "' in " + (System.DateTime.Now - startTime).TotalSeconds + "seconds. Success rate: " +
					(shadersPaths.Length - failedShaders.Count) + "/" + shadersPaths.Length);
		
		if (failedShaders.Count > 0)
			Debug.Log("Failed shaders:");
			
		foreach (string shaderName in failedShaders) {
			Debug.Log("\t\tFailed: " + shaderName);
		}
	}
	
	private void BuildMappingOptions() {
		//Debug.Log("BuildMappingOptions");
		//Fill available shaders
		List<string> mappingOptions = new List<string>();
		
		mappingOptions.Add("parse");
		mappingOptions.Add("skip");
		
		foreach (string dstShaderName in LLEditorInternal.GetAllShaderMappingsDst() ) {
			mappingOptions.Add( dstShaderName );	
		}
			
		allMappingOptions = mappingOptions.ToArray();
		
		//LLHelper.PrintArray("allMappingOptions", allMappingOptions);
		
		//Load for all shaders
		
		List<SceneShaderMapping> nonMappedShaders = new List<SceneShaderMapping>();
		
		//Automatically map to the correct shader
		foreach (SceneShaderMapping sMapping in shadersMapping) {
			string dstShaderName = LLEditorInternal.GetShaderMapping( sMapping.origShaderName );
			//int dstShaderIndex;
			
			
			
			if (dstShaderName != null) {
				sMapping.remappedShaderName = dstShaderName;
				
				sMapping.remappedShaderIndex = System.Array.IndexOf(allMappingOptions, dstShaderName);
				
				if (sMapping.remappedShaderIndex == -1) {
					Debug.LogError("Cannot find shader '" + dstShaderName + "' in allMappingOptions.");
					sMapping.remappedShaderIndex = 0;
				}
			} else {
				sMapping.remappedShaderName = "none";
				sMapping.remappedShaderIndex = 0;
				nonRemappedShaders++;
			}
			
			
			if (null == dstShaderName) {
				nonMappedShaders.Add( sMapping );
			}
			
			//Debug.Log(sMapping.origShaderName + " => " + sMapping.remappedShaderName + ", index: " + sMapping.remappedShaderIndex);
			
			/*
			if (sMapping.remappedShaderIndex < 0) {
				Debug.LogError("Cannot find a shader with name " + sMapping.remappedShaderName);
				sMapping.remappedShaderIndex = 1;
			}*/
		}
		
		//Remove empty mappings
		foreach (SceneShaderMapping sMapping in nonMappedShaders) {
			shadersMapping.Remove( sMapping );
		}
		
		//Add empty mappings to the beginning
		foreach (SceneShaderMapping sMapping in nonMappedShaders) {
			shadersMapping.Insert(0, sMapping);
		}
		
		ReadShadersToSkip();
	}
	
	//private void LoadMappingOptions
	
	
	private void UpdateShadersList() {
		LLEditorInternal.LoadMappingXML();
		
		nonRemappedShaders = 0;
		
		//Get mappings
		shadersMapping = new List<SceneShaderMapping>();
		
		List <string> shadersUsed = new List<string>();
		
		//Renderer[] allRenderers = GameObject.FindObjectsOfType( typeof(Renderer) ) as Renderer[];
		//foreach (Renderer rend in allRenderers) {
		foreach ( string curShaderName in GetAllShaderNamesInAScene() ) {
			//Check for duplicates
			if ( !shadersUsed.Contains(curShaderName) ) {
				//Ignore LL shaders
				if ( curShaderName.Contains("Linear Lighting/") )
					continue;
				
				if ( curShaderName.Contains("Hidden/") )
					continue;
				
				if ( LLHelper.IsNullOrWhiteSpace(curShaderName) )
					continue;
				
				//Debug.Log("Adding " + shaderName);
				
				SceneShaderMapping sMapping = new SceneShaderMapping();
				
				sMapping.origShaderName = curShaderName;
				
				//for (int i = 0; i < 10; i++) {
				shadersMapping.Add( sMapping );
				
				shadersUsed.Add( curShaderName );
				//}
			}
		}
		
		
		BuildMappingOptions();
		
		builtShadersList = true;
	}
}
#endif