using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// import ของที่จะใช้
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    
    

    public void NewNameEnter(string name)
    {
        // add code here to handle when a color is selected
        //Debug.Log("name ="+name);

        MyMainManager.Instance.playerName = name;//color;

        //Debug.Log("MyMainManager.Instance.playerName =" + MyMainManager.Instance.playerName);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {




        // MainManager.Instance.SaveColor();  เรียก SaveColor(); ตอนปิดโปรแกรม






        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // สัั่งปิดปกติ แต่ไม่แสดงผลใน Unity Editor
        #endif

        /* แปลว่า
                if (UNITY_EDITOR)
                {
                    // run this code
                }
                else
                {
                    // run this code
                }

        ข้างบน ต้องใส่ namespace ด้วย

                #if UNITY_EDITOR
                    using UnityEditor;
                #endif
         */
    }


}
