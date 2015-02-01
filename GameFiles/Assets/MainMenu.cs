using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public enum Menu {
		MainMenu,
		NewGame,
		Continue
	}

	public Menu currentMenu;

	void OnGUI () {

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if(currentMenu == Menu.MainMenu) {

			GUILayout.Box("Space Warfare");
			GUILayout.Space(10);

			if(GUILayout.Button("New Game")) {
				Game.current = new Game();
				currentMenu = Menu.NewGame;
			}

			if(GUILayout.Button("Continue")) {
				SaveLoad.Load();
				currentMenu = Menu.Continue;
			}

			if(GUILayout.Button("Quit")) {
				Application.Quit();
			}
		}

		else if (currentMenu == Menu.NewGame) {
			Game.current.name=GUILayout.TextField(Game.current.name, 25);
			if(GUILayout.Button("Confirm")){
				SaveLoad.Save();
				Application.LoadLevel(1);
			}
		}

		else if (currentMenu == Menu.Continue) {
			
			GUILayout.Box("Load this save file?");
			GUILayout.Space(10);
			try{
				Game g=Game.current;
				GUILayout.Box(g.name+"\n\nScore: "+g.score);
					if(GUILayout.Button("OK")) {
						//Move on to game...
						Application.LoadLevel(1);
					}
				GUILayout.Space(10);
				if(GUILayout.Button("Cancel")) {
					currentMenu = Menu.MainMenu;
				}
			}catch{
				currentMenu=Menu.MainMenu;
			}
			
			
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}
}
