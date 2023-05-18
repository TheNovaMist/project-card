using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class raycast : MonoBehaviour
{

    private int hitCount = 0;
    private int missCount = 0;

    public GameObject good;
    public GameObject ui;
    public GameObject life;

    private Vector2 touchPosition = default;
    Camera m_MainCamera;

    // 存储比较的卡牌
    Stack<GameObject> m_Stack = new Stack<GameObject>(2);

    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    Ray ray;
    void Update()
    {
        Debug.DrawRay(transform.position, ray.direction, UnityEngine.Color.green);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                ray = m_MainCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {

                    Debug.DrawRay(transform.position, ray.direction, UnityEngine.Color.green);
                    GameObject hitObj = hitObject.transform.gameObject;
                    Debug.Log(hitObj.name);
                    Debug.Log(hitObj.tag);

                    // 栈未满
                    if (m_Stack.Count < 2)
                    {
                        m_Stack.Push(hitObj);
                    }
                    // 栈满 开始比较
                    if (m_Stack.Count == 2)
                    {
                        // 比较
                        GameObject o2 = m_Stack.Pop();
                        GameObject o1 = m_Stack.Pop();
                        if (o1.tag == o2.tag)
                        {
                            Debug.Log("相同的牌：" + o1.tag);
                            hitCount++;
                            StartCoroutine(RotateImage(false, o1, o2));
                            Debug.Log("成功！");

                            if (hitCount == 6)
                            {
                                good.SetActive(true);
                            }
                        }
                        else
                        { 
                            int size = life.transform.childCount;
                            if (size > 0)
                            {
                                Destroy(life.transform.GetChild(size - 1).gameObject);
                            }

                            missCount++;
                            if (missCount == 7)
                            {
                                // 重启
                                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                            }
                            StartCoroutine(RotateImage(true, o1, o2));
                        }


                        Debug.Log("比较结束");
                    }
                }
            }
        }
    }

    IEnumerator RotateImage(bool needBack, params GameObject[] list)
    {
        float y = 180;
        GameObject myObject = list[0];

        list[0].transform.Rotate(new Vector3(0, 0, 180));
        list[1].transform.Rotate(new Vector3(0, 0, 180));

        yield return new WaitForSeconds(2);
        if (needBack == true)
        {
            list[0].transform.Rotate(new Vector3(0, 0, 180));
            list[1].transform.Rotate(new Vector3(0, 0, 180));
        }
        yield return null;
    }
}