using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersitence : MonoBehaviour
{
    private GameData gameData;
    public static DataPersitence instance {get;private set;}

    private void Awake() {
        if(instance !=null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene");
        }
        instance=this;
    }
    private void Start() {
        LoadGame();
    }
    public void LoadGame()
    {
        if(this.gameData==null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            //NewGame();
        }

    }
    public void SaveGame()
    {
         
    }
    private void OnApplicationQuit() {
        SaveGame();
    }
}
