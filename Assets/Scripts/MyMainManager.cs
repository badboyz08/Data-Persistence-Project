using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ต้องใช้ ตอน Serializable
using System.IO;


public class MyMainManager : MonoBehaviour
{
    public static MyMainManager Instance;
    
    public string playerName;

    private void Awake()
    {

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    public void SaveName()
    {
        SaveData data = new SaveData(); //ให้ data เท่ากับ ค่าใหม่
        data.playerName = playerName; //เก็บสีใหม่ลง สีของ data

        string json = JsonUtility.ToJson(data); // เรียก ToJson เก็บ data ลง json

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // ใช้ File.WriteAllText สร้างเซฟเป็น text ด้วยค่า json ก่อนหน้า
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }

}
