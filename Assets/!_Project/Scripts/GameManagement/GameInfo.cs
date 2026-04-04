using System.IO;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public static class GameInfo
{
    public class SaveData
    {
        public int Money;
        public int Pollution;
        public bool IsWasteBinBuilt;
        public bool IsSortingBinBuilt;
        public bool IsRefineryBuilt;

        public SaveData()
        {
            Money = 0;
            Pollution = 0;
            IsWasteBinBuilt = false;
            IsSortingBinBuilt = false;
            IsRefineryBuilt = false;
        }

        public SaveData(int money, int pollution, bool isWasteBinBuilt, bool isSortingBinBuilt, bool isRefineryBuilt)
        {
            Money = money;
            Pollution = pollution;
            IsWasteBinBuilt = isWasteBinBuilt;
            IsSortingBinBuilt = isSortingBinBuilt;
            IsRefineryBuilt = isRefineryBuilt;
        }
    }

    public static UnityEvent<int> MoneyChangedEvent = new UnityEvent<int>();

    public static UnityEvent<int> PollutionChangedEvent = new UnityEvent<int>();

    private static string SavePath => Path.Combine(Application.persistentDataPath, "gameStats.xml");

    public static int Money { get; private set; }
    public static int Pollution { get; private set; }

    public static int InitialMoney { get; private set; }
    public static int InitialPollution { get; private set; }

    public static bool IsWasteBinBuilt { get; private set; }
    public static bool IsSortingBinBuilt { get; private set; }
    public static bool IsRefineryBuilt { get; private set; }

    public static Transform EnvironmentContainer
    {
        get
        {
            if (_environmentContainer == null)
                _environmentContainer = GameObject.FindWithTag("EnvironmentContainer")?.transform;
            return _environmentContainer;
        }
    }
    public static Transform NPC
    {
        get
        {
            if (_npc == null)
                _npc = GameObject.FindWithTag("NPC")?.transform;
            return _npc;
        }
    }
    public static Transform Items
    {
        get
        {
            if (_items == null)
                _items = GameObject.FindWithTag("Items")?.transform;
            return _items;
        }
    }

    private static Transform _environmentContainer;
    private static Transform _items;
    private static Transform _npc;

    public static bool XMLExists()
    {
        return File.Exists(SavePath);
    }

    public static void CreateDefaultFile()
    {
        var defaultData = new SaveData(0, 0, false, false, false);
        WriteSaveData(defaultData);
    }

    public static void ReadStats()
    {
        if (!XMLExists())
        {
            CreateDefaultFile();
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            using (FileStream stream = new FileStream(SavePath, FileMode.Open))
            {
                SaveData data = (SaveData)serializer.Deserialize(stream);
                Money = data.Money;
                Pollution = data.Pollution;
                IsWasteBinBuilt = data.IsWasteBinBuilt;
                IsSortingBinBuilt = data.IsSortingBinBuilt;
                IsRefineryBuilt = data.IsRefineryBuilt;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to read stats: {e.Message}");
        }
    }

    public static void WriteStats()
    {
        try
        {
            SaveData data = new SaveData(Money, Pollution, IsWasteBinBuilt, IsSortingBinBuilt, IsRefineryBuilt);
            WriteSaveData(data);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to write stats: {e.Message}");
        }
    }

    public static void ResetStats()
    {
        Money = 0;
        Pollution = 0;
        IsWasteBinBuilt = false;
        IsSortingBinBuilt = false;
        IsRefineryBuilt = false;
        WriteStats();
    }

    private static void WriteSaveData(SaveData data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        using (FileStream stream = new FileStream(SavePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }

    public static void SetInfo()
    {
        if (XMLExists())
        {
            ReadStats();
        }
        else
        {
            ResetStats();
        }
        InitialMoney = Money;
        InitialPollution = Pollution;
    }

    public static void AddMoney(int amount)
    {
        SetMoney(Money + Mathf.Abs(amount));
    }

    public static void SubtractMoney(int amount)
    {
        SetMoney(Money - Mathf.Abs(amount));
    }

    public static void AddPollution(int amount)
    {
        SetPollution(Pollution + Mathf.Abs(amount));
    }

    public static void SubtractPollution(int amount)
    {
        SetPollution(Pollution - Mathf.Abs(amount));
    }

    public static void RegisterWasteBin()
    {
        IsWasteBinBuilt = true;
    }

    public static void RegisterSortingBin()
    {
        IsSortingBinBuilt = true;
    }

    public static void RegisterRefinery()
    {
        IsRefineryBuilt = true;
    }

    private static void SetMoney(int value)
    {
        Money = Mathf.Max(0, value);
        MoneyChangedEvent.Invoke(Money);
    }

    private static void SetPollution(int value)
    {
        Pollution = Mathf.Clamp(value, 0, 100);
        PollutionChangedEvent.Invoke(Pollution);
    }
}
