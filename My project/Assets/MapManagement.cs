using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MapManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject _sun;
    string _currentTime;
    float _nextTime;
    public static int _hours;
    public static int _minute;

    [SerializeField]
    private TextMeshProUGUI timeDisplay;
    // Start is called before the first frame update
    void Start()
    {
        _nextTime = Time.time + 60f;
        _hours = DateTime.Now.TimeOfDay.Hours;
        _minute = DateTime.Now.TimeOfDay.Minutes;

        SetupSun();
        UpdateNewTime();
        DisplayTime(_currentTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        RealTime();
    }

    private void RealTime()
    {
        if (Time.time > _nextTime)
        {
            _minute += 1;
            if (_minute == 60)
            {
                SunMove();
                if (_hours == 23)
                    _hours = 0;
                else
                    _hours += 1;

                _minute = 0;
            }
            UpdateNewTime();
            _nextTime = Time.time + 60f;
        }

        DisplayTime(_currentTime);
    }

    private void SetupSun()
    {
        int sumMinutes = _hours * 60 + _minute;
        float _nextRota = _sun.transform.rotation.x + 0.125f * sumMinutes;
        _sun.transform.rotation = Quaternion.Euler(_nextRota, 0f, 0f);
        //Light rootSun = _sun.transform.GetChild(0).GetComponent<Light>();

        if (_hours < 12)
        {
            if (_hours < 6)
            {
                //rootSun.intensity = 0f;
                RenderSettings.ambientIntensity = 0.6f;
                return;
            }
            //float intensitySunByTime = (12 - _hours)/10f * 1.2f;
            //rootSun.intensity = 1.2f - intensitySunByTime;
            float intensityEnvByTime = (12 - _hours) / 10f * 6f;
            RenderSettings.ambientIntensity = 6f - intensityEnvByTime;
        }
        else
        {
            if (_hours == 12)
            {
                //rootSun.intensity = 1.2f;
                RenderSettings.ambientIntensity = 6f;
                return;
            }

            if (_hours > 19)
            {
                RenderSettings.ambientIntensity = 1.2f;
                return;
            }
            //float intensitySunByTime = (12 - _hours) / 10f * 1.2f;
            //rootSun.intensity = 1.2f + intensitySunByTime;
            float intensityEnvByTime = (12 - _hours) / 10f * 6f;
            RenderSettings.ambientIntensity = 6f + intensityEnvByTime;
        }
    }

    private void SunMove()
    {
        float _nextRota = _sun.transform.rotation.x + 0.125f;
        _sun.transform.rotation = Quaternion.Euler(_nextRota, 0f, 0f);
        //Light rootSun = _sun.transform.GetChild(0).GetComponent<Light>();

        if (_hours < 12)
        {
            if (_hours < 6)
            {
                RenderSettings.ambientIntensity = 0.6f;
                return;
            }
            RenderSettings.ambientIntensity += 0.6f;
        }
        else
        {
            if (_hours == 12)
            {
                RenderSettings.ambientIntensity = 6f;
                return;
            }

            if (_hours > 19)
            {
                RenderSettings.ambientIntensity = 1.2f;
                return;
            }
            RenderSettings.ambientIntensity -= 0.6f;
        }

    }

    private void UpdateNewTime()
    {
        if (_hours < 10)
        {
            if (_minute < 10)
                _currentTime = "0" + _hours + ":0" + _minute;
            else
                _currentTime = "0" + _hours + ":" + _minute;
        }
        else
        {
            if (_minute < 10)
                _currentTime = _hours + ":0" + _minute;
            else
                _currentTime = _hours + ":" + _minute;
        }


    }

    void DisplayTime(string timeText)
    {
        timeDisplay.text = timeText;
    }
}
