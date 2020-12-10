using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveLoad
{

    public static void SavePlayer(SaveData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.xml";
        FileStream stream = new FileStream(path, FileMode.Create);


        formatter.Serialize(stream, player);
        stream.Close();
    }

    public static void LoadPlayer()
    {
        try
        {
            string path = Application.persistentDataPath + "/player.xml";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                SaveData data = formatter.Deserialize(stream) as SaveData;
                stream.Close();

                StaticClass.score = data.score;
                StaticClass.actualLevel = data.level;

            }
            else
            {
                StaticClass.score = 0;
                StaticClass.actualLevel = 0;
            }
        }
        catch (Exception ex) {
            StaticClass.score = 0;
            StaticClass.actualLevel = 0;
        }
    }
}
