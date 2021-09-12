using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
//ต้องใช้ ตอน Serializable
using System.IO;


public class MyMainManager : MonoBehaviour
{
    public static MyMainManager Instance;
    public static MainManager Instance2;

    public string playerName;
    public string highScore;
    public int lastHighScore;
    public Text BestScoreTextMenu;

    private void Awake()
    {
        // start of new code
        if (Instance != null)       // ทำลาย Instance เพื่อไม่ให้ซ้ำซ้อนตอนเข้าออกเมนูบ่อยๆ
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadName();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    class SaveData2
    {

        public string highScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData(); //ให้ data เท่ากับ ค่าใหม่
        SaveData2 data2 = new SaveData2();

        data.playerName = playerName;     
        data2.highScore = highScore;

        string json = JsonUtility.ToJson(data); // เรียก ToJson เก็บ data ลง json
        string json2 = JsonUtility.ToJson(data2); // เรียก ToJson เก็บ data ลง json

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // ใช้ File.WriteAllText สร้างเซฟเป็น text ด้วยค่า json ก่อนหน้า
        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json2);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string path2 = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path) && File.Exists(path2))
        {
            string json = File.ReadAllText(path);
            string json2 = File.ReadAllText(path2);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            SaveData2 data2 = JsonUtility.FromJson<SaveData2>(json2);
            playerName = data.playerName;
            highScore = data2.highScore;

            BestScoreTextMenu.text = $"{playerName} Got Best Score : {highScore}";

            if (highScore != null) 
            {
                 int.TryParse(highScore, out lastHighScore);
            }
                
            
        }
    }

}
