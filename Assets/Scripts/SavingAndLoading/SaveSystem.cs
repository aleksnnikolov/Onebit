using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {


    public static void SaveData(Vector3 bitPos, Vector3 camPos, GameObject[] crawlers, GameObject[] boxes) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(bitPos, camPos, crawlers, boxes);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData() {
        string path = Application.persistentDataPath + "/save.data";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("save file missing");
            return null;
        }
    }
    
    //call on each game exit
    public static void DeleteData() {
        string path = Application.persistentDataPath + "/save.data";
        if (File.Exists(path)) {
            File.Delete(path);
            return;
        } else {
            Debug.LogError("save file missing");
            return;
        }
    }

}
