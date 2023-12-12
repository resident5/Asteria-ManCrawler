using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance;

    [SerializeField]
    private string fileName;


    public List<IDataPersistence> dataPersistenceObjects;

    private GameData galleryData;

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.galleryData = new GameData();
    }

    public void LoadGame()
    {
        this.galleryData = dataHandler.Load();

        if (this.galleryData == null)
        {
            Debug.Log("No data was found");
            NewGame();
        }

        foreach (IDataPersistence pers in dataPersistenceObjects)
        {
            pers.LoadData(galleryData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence pers in dataPersistenceObjects)
        {
            pers.SaveData(ref galleryData);
        }

        Debug.Log("Loaded save = " + galleryData.seenLevel1);

        dataHandler.Save(galleryData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
