using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    string filePath;
    int playerCurrency;
    int playerEXP;

    int lastScene;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        if (File.Exists(filePath + "/playerOptions.dat"))
        {
            LoadOptions();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RetrievePlayer()
    {
        player = GameObject.Find("Player");
        if (File.Exists(filePath + "/playerInfo.dat"))
        {
            LoadPlayerData();
        }
    }

    //SAVES THE INFORMATION ON THE PLAYER
    public void SavePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath + "/playerInfo.dat");

        //LoggedQuotes send = new LoggedQuotes();
        //send.theQuotes = quotes;

        //bf.Serialize(file, send);
        file.Close();
    }

    //LOADS THE FILE CONTAINING ALL OF THE PLAYER INFORMATION
    public void LoadPlayerData()
    {
        if (File.Exists(filePath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath + "/playerInfo.dat", FileMode.Open);
            //LoggedQuotes pulled = (LoggedQuotes)bf.Deserialize(file);
            file.Close();

            //quotes = pulled.theQuotes;
        }
    }


    //SAVES THE OPTIONS IN A FILE
    public void SaveOptions()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath + "/playerOptions.dat");

        //LoggedQuotes send = new LoggedQuotes();
        //send.theQuotes = quotes;

        //bf.Serialize(file, send);
        file.Close();
    }

    //LOADS THE FILE CONTAINING THE OPTIONS
    public void LoadOptions()
    {
        if (File.Exists(filePath + "/playerOptions.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath + "/playerOptions.dat", FileMode.Open);
            //LoggedQuotes pulled = (LoggedQuotes)bf.Deserialize(file);
            file.Close();

            //quotes = pulled.theQuotes;
        }
    }
}