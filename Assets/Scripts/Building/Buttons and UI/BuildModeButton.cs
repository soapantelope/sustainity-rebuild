﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BuildModeButton : MonoBehaviour
{
    public bool on;
    public bool transitioning;
    public int speed;

    public GameObject BuildMode;
    public GameObject BuildCursor;
    public GameObject BuildMenu;

    public Button button;
    public RectTransform bm;

    void Start()
    {
        button.onClick.AddListener(onButtonPress);
        bm = BuildMenu.GetComponent<RectTransform>();
    }

    // Overrides turnOn and turnOff functions
    public void turnOn()
    {
        if (!transitioning)
        {
            StartCoroutine(transition(270, speed));
            setActivity(true);
            on = true;
        }
    }

    public void turnOff()
    {
        if (!transitioning)
        {
            StartCoroutine(transition(550, speed));
            setActivity(false);
            on = false;
        }
    }

    public void setActivity(bool mode)
    {
        BuildCursor.SetActive(mode);
        BuildMenu.SetActive(mode);
    }

    void onButtonPress() {
        if (on) turnOff();
        else turnOn();
    }

    IEnumerator transition(int goalX, int speed) {
        transitioning = true;
        while (Mathf.Abs(bm.anchoredPosition.x - goalX) > 2f) {
            bm.anchoredPosition = Vector2.Lerp(new Vector2(bm.anchoredPosition.x, bm.anchoredPosition.y), 
                new Vector2(goalX, bm.anchoredPosition.y), speed * Time.deltaTime);
            yield return null;
        }
        bm.anchoredPosition = new Vector2(goalX, bm.anchoredPosition.y);
        transitioning = false;
    }
}
