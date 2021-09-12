using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
//ต้องใช้ ตอน Serializable
using System.IO;


public class MyMainManager : MonoBehaviour
{
    public static MyMainManager Instance;
    
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
        public string highScore;
        public int lastHighScore;
        public Text BestScoreTextMenu;
    }

    public void SaveName()
    {
        SaveData data = new SaveData(); //ให้ data เท่ากับ ค่าใหม่
        data.playerName = playerName; 
        
        data.highScore = highScore;

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
            highScore = data.highScore;

            BestScoreTextMenu.text = $"{playerName} Got Best Score : {highScore}";

            if (highScore != null) 
            {
                 int.TryParse(highScore, out lastHighScore);
            }
                
            
        }
    }

}
