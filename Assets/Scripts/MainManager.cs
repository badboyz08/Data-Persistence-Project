using System.Collections;
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
    private int king_Points;
    private string m_Name;
    private string king_Name;

    private bool m_GameOver = false;

    

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBlock();
        LoadAndShowFromSave();
    }

    private void LoadAndShowFromSave()
    {
        MyMainManager.Instance.LoadHighScore();
        king_Name = MyMainManager.Instance.playerName;
        king_Points = MyMainManager.Instance.lastHighScore;
        BestScoreText.text = $"{king_Name} Score : {king_Points}";
    }

    private void CreateBlock()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //CreateBlock();
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
        

        if(m_Points > MyMainManager.Instance.lastHighScore)
        {
            

            //ตรงนี้ถูกแล้ว เพราะ highscore save ได้
            MyMainManager.Instance.playerName = m_Name;
            MyMainManager.Instance.highScore = m_Points.ToString();
            
            MyMainManager.Instance.SaveHighScore();
            LoadAndShowFromSave();
        }
        else if(m_Points == MyMainManager.Instance.lastHighScore)
        {
            LoadAndShowFromSave();
        }
        else
        {
            LoadAndShowFromSave();
        }
    }
}
