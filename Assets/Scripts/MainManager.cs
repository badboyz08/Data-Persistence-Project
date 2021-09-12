﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private string m_Name;

    private bool m_GameOver = false;

    public static MainManager Instance2;

    void Awake()
    {
        //MyMainManager.Instance.LoadName();
        if (Instance2 != null)       // ทำลาย Instance เพื่อไม่ให้ซ้ำซ้อนตอนเข้าออกเมนูบ่อยๆ
        {
            Destroy(gameObject);
            return;
        }

        Instance2 = this;
        DontDestroyOnLoad(gameObject);

        //MyMainManager.Instance.LoadName();
    }

    // Start is called before the first frame update
    void Start()
    {
        

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        HighScoreCHK();
        m_Points = 0;
    }


    public void HighScoreCHK()
    {
        //................ค่อย เอา คอมเม้นออกทีหลัง ขี้เกียจ beat high score................//

 

        if(m_Points > MyMainManager.Instance.lastHighScore)
        {
            m_Name = MyMainManager.Instance.playerName;
            BestScoreText.text = $"{m_Name} Got New Best Score : {m_Points}";

            //ตรงนี้ถูกแล้ว เพราะ highscore save ได้
            MyMainManager.Instance.playerName = m_Name;
            MyMainManager.Instance.highScore = m_Points.ToString();
            
            MyMainManager.Instance.SaveName();
        }
        else if(m_Points == MyMainManager.Instance.lastHighScore)
        {
            MyMainManager.Instance.LoadName();
            BestScoreText.text = $"{MyMainManager.Instance.playerName} Still Got Best Score : {MyMainManager.Instance.highScore}";
        }
        else
        {
            MyMainManager.Instance.LoadName();
            BestScoreText.text = $"{MyMainManager.Instance.playerName} Still Got Best Score : {MyMainManager.Instance.highScore}";
        }
    }
}
