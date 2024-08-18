using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

[Serializable]
public class Data
{
    // �ر� �Ϸ��� �������� ���� ����
    public int stageLevel = 1;

    // ���� ��� â ���� ����
    public bool isHowToShown;

    // ���� ������ ��û ����
    public bool isEndingPlayed;

    // �ΰ��� ���� ���� ����
    public float volume = .5f;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Data currentData = new Data();
    public string path { get; set; }

    private void Awake()
    {
        #region �̱���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        path = Application.persistentDataPath + "/StageSave";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(currentData);
        string encryptedData = Encrypt(data);
        File.WriteAllText(path, encryptedData);
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            string decryptedData = Decrypt(data);
            currentData = JsonUtility.FromJson<Data>(decryptedData);
        }
    }

    public void DataClear()
    {
        currentData = new Data();
    }

    // AES ��ȣȭ Ű �� �ʱ�ȭ ����(IV)�� ����
    private static readonly string encryptionKey = "1234567890123456"; // 16 characters, 128-bit key
    private static readonly string iv = "6543210987654321"; // 16 characters, 128-bit IV

    private string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }
    }

    private string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
