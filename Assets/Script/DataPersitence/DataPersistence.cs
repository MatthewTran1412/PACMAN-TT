using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataPersistence : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField]private string filename;
    public GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistence instance {get;private set;}

    private void Awake() {
        if(instance !=null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene");
        }
        instance=this;
    }
    private void Start() {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath,filename);
        this.dataPersistenceObjects=FindAllDataPersistenceObjects();
        //LoadGame();
    }
    public void NewGame()
    {
        this.gameData= new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load(); 
        if(this.gameData==null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
         foreach (IDataPersistence item in dataPersistenceObjects)
         {
            item.SaveData(ref gameData);
         }
         dataHandler.Save(gameData);
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
