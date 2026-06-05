using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class TimeZoneClockManager : MonoBehaviour
{
    [System.Serializable]
    public class TimeZoneDisplay
    {
        public string timeZoneName;
        public string timeZoneId;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI dateText;
    }
    
    [SerializeField] private List<TimeZoneDisplay> timeZones = new List<TimeZoneDisplay>();
    [SerializeField] private TextMeshProUGUI localTimeText;
    [SerializeField] private TextMeshProUGUI utcTimeText;
    [SerializeField] private float updateInterval = 0.1f;
    
    private float updateTimer = 0f;
    
    private void Start()
    {
        // Initialize with some common time zones
        if (timeZones.Count == 0)
        {
            InitializeDefaultTimeZones();
        }
    }
    
    private void Update()
    {
        updateTimer += Time.deltaTime;
        
        if (updateTimer >= updateInterval)
        {
            UpdateAllClocks();
            updateTimer = 0f;
        }
    }
    
    private void UpdateAllClocks()
    {
        DateTime utcNow = DateTime.UtcNow;
        
        // Update UTC time
        if (utcTimeText)
        {
            utcTimeText.text = $"UTC\n{utcNow:HH:mm:ss}\n{utcNow:dddd, MMMM dd, yyyy}";
        }
        
        // Update local time
        if (localTimeText)
        {
            DateTime localTime = DateTime.Now;
            localTimeText.text = $"Local Time\n{localTime:HH:mm:ss}\n{localTime:dddd, MMMM dd, yyyy}";
        }
        
        // Update each time zone
        foreach (TimeZoneDisplay tzDisplay in timeZones)
        {
            if (tzDisplay.timeText == null) continue;
            
            try
            {
                TimeZoneInfo tzInfo = TimeZoneInfo.FindSystemTimeZoneById(tzDisplay.timeZoneId);
                DateTime tzTime = TimeZoneInfo.ConvertTime(utcNow, tzInfo);
                
                tzDisplay.timeText.text = $"{tzDisplay.timeZoneName}\n{tzTime:HH:mm:ss}";
                
                if (tzDisplay.dateText)
                {
                    tzDisplay.dateText.text = $"{tzTime:dddd, MMMM dd, yyyy}";
                }
            }
            catch (TimeZoneNotFoundException)
            {
                if (tzDisplay.timeText)
                    tzDisplay.timeText.text = $"{tzDisplay.timeZoneName}\n[TZ Not Found]";
            }
        }
    }
    
    private void InitializeDefaultTimeZones()
    {
        // Default time zones to display
        string[] defaultZones = new string[]
        {
            "Pacific Standard Time",
            "Mountain Standard Time",
            "Central Standard Time",
            "Eastern Standard Time",
            "GMT Standard Time",
            "Central European Standard Time",
            "India Standard Time",
            "China Standard Time",
            "Tokyo Standard Time",
            "Australia/Sydney"
        };
        
        Debug.Log("Initialize with default time zones from code");
    }
    
    public void AddTimeZone(string timeZoneName, string timeZoneId, TextMeshProUGUI timeText, TextMeshProUGUI dateText = null)
    {
        TimeZoneDisplay newTz = new TimeZoneDisplay
        {
            timeZoneName = timeZoneName,
            timeZoneId = timeZoneId,
            timeText = timeText,
            dateText = dateText
        };
        
        timeZones.Add(newTz);
    }
    
    public void RemoveTimeZone(int index)
    {
        if (index >= 0 && index < timeZones.Count)
        {
            timeZones.RemoveAt(index);
        }
    }
    
    public static string[] GetAvailableTimeZones()
    {
        TimeZoneInfo[] zones = TimeZoneInfo.GetSystemTimeZones();
        string[] zoneIds = new string[zones.Length];
        
        for (int i = 0; i < zones.Length; i++)
        {
            zoneIds[i] = zones[i].Id;
        }
        
        return zoneIds;
    }
}