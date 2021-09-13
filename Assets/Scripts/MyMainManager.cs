using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
//ต้องใช้ ตอน Serializable
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MyMainManager : MonoBehaviour
{
    public static MyMainManager Instance;

    public string playerName;
    public string kingPlayerName;
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

        LoadHighScore();

        BestScoreTextMenu.text = $"Name : {kingPlayerName} Score : {highScore}";
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string highScore;
    }


    public void SaveHighScore()
    {
        SaveData data = new SaveData(); //ให้ data เท่ากับ ค่าใหม่


        data.playerName = playerName;
        data.highScore = highScore;


        string json = JsonUtility.ToJson(data); // เรียก ToJson เก็บ data ลง json


        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // ใช้ File.WriteAllText สร้างเซฟเป็น text ด้วยค่า json ก่อนหน้า

    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            kingPlayerName = data.playerName;
            highScore = data.highScore;

            

            if (highScore != null) 
            {
                 int.TryParse(highScore, out lastHighScore);
            }
                
            
        }
    }





}
