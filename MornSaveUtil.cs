using System;
using System.IO;
using MornSteam;
using MornUtil;
using UnityEngine;

namespace MornSave
{
    public static class MornSaveUtil
    {
        private static string _cryptIv;
        /// <summary>16バイトの初期化ベクトル（IV）を設定します。 </summary>
        public static string CryptIv
        {
            set
            {
                if (value == null)
                {
                    MornSaveGlobal.LogError("CryptIvにnullは設定できません");
                    return;
                }

                var byteCount = System.Text.Encoding.UTF8.GetByteCount(value);
                if (byteCount != 16)
                {
                    MornSaveGlobal.LogError($"CryptIvは16バイトでなければなりません。現在のバイト数: {byteCount}");
                    return;
                }

                _cryptIv = value;
            }
        }
        private static string _cryptKey;
        /// <summary>32バイトの暗号化キーを設定します。</summary>
        public static string CryptKey
        {
            set
            {
                if (value == null)
                {
                    MornSaveGlobal.LogError("CryptKeyにnullは設定できません");
                    return;
                }

                var byteCount = System.Text.Encoding.UTF8.GetByteCount(value);
                if (byteCount != 32)
                {
                    MornSaveGlobal.LogError($"CryptKeyは32バイトでなければなりません。現在のバイト数: {byteCount}");
                    return;
                }

                _cryptKey = value;
            }
        }

        public static string GetCorePlayerPrefsKey()
        {
            return MornSaveGlobal.I.CorePlayerPrefsKey;
        }

        /// <summary> Steamも考慮したセーブディレクトリのパスを返す</summary>
        public static string GetSaveDirPath()
        {
            var basePath = Application.persistentDataPath;
            var folderPath = MornSaveGlobal.I.SaveDir;
#if USE_STEAM
            if (MornSteamManager.Initialized)
            {
                folderPath = MornSteamManager.UserId;
            }
#endif
            var dirPath = Path.Combine(basePath, folderPath);
            return dirPath;
        }

        /// <summary> Steamも考慮したCoreファイルのパスを返す</summary>
        public static string GetCoreFilePath()
        {
            var fileName = MornSaveGlobal.I.CoreFileName + MornSaveGlobal.I.CoreExtensionName;
            var filePath = Path.Combine(GetSaveDirPath(), fileName);
            return filePath;
        }

        private static void EnsurePath(string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (directoryName != null && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
                MornSaveGlobal.Log($"セーブディレクトリを作成しました: {directoryName}");
            }
        }

        private static byte[] ToEncrypt(byte[] data)
        {
            if (string.IsNullOrEmpty(_cryptIv) || string.IsNullOrEmpty(_cryptKey))
            {
                MornSaveGlobal.LogError("CryptIvまたはCryptKeyが設定されていません。暗号化を使用する場合は設定してください。");
            }
            else
            {
                try
                {
                    data = MornCrypt.EncryptBytes(data, _cryptIv, _cryptKey);
                }
                catch (Exception e)
                {
                    MornSaveGlobal.LogError($"暗号化に失敗: {e.Message}");
                }
            }

            return data;
        }

        private static byte[] ToDecrypt(byte[] data)
        {
            if (string.IsNullOrEmpty(_cryptIv) || string.IsNullOrEmpty(_cryptKey))
            {
                MornSaveGlobal.LogError("CryptIvまたはCryptKeyが設定されていません。暗号化を使用する場合は設定してください。");
            }
            else
            {
                try
                {
                    data = MornCrypt.DecryptBytes(data, _cryptIv, _cryptKey);
                }
                catch (Exception e)
                {
                    MornSaveGlobal.LogError($"復号化に失敗: {e.Message}");
                }
            }

            return data;
        }

        public static bool LoadFromPlayerPrefs<T>(string key, out T data, bool useDecrypt = false) where T : class
        {
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    var json = PlayerPrefs.GetString(key);
                    if (useDecrypt)
                    {
                        json = ToDecrypt(json.ToBytesUTF8()).ToStringUTF8();
                    }

                    data = JsonUtility.FromJson<T>(json);
                    if (data != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"PlayerPrefs読み込み時にエラー発生: {e.Message}");
            }

            data = null;
            return false;
        }

        public static bool LoadFromPlayerPrefs(string key, out byte[] data, bool useDecrypt = false)
        {
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    var json = PlayerPrefs.GetString(key);
                    data = json.ToBytesBase64();
                    if (useDecrypt)
                    {
                        data = ToDecrypt(data);
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"PlayerPrefs読み込み時にエラー発生: {e.Message}");
            }

            data = null;
            return false;
        }

        public static bool LoadFromFile<T>(string filePath, out T data, bool useDecrypt = false) where T : class
        {
            try
            {
                if (LoadFromFile(filePath, out var bytes, useDecrypt))
                {
                    data = JsonUtility.FromJson<T>(bytes.ToStringUTF8());
                    if (data != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"ファイル読み込み時にエラー発生: {e.Message}");
            }

            data = null;
            return false;
        }

        public static bool LoadFromFile(string filePath, out byte[] data, bool useDecrypt = false)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    data = File.ReadAllBytes(filePath);
                    if (useDecrypt)
                    {
                        data = ToDecrypt(data);
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"ファイル読み込み時にエラー発生: {e.Message}");
            }

            data = null;
            return false;
        }

        public static bool SaveToPlayerPrefs<T>(string key, T data, bool useEncrypt = false) where T : class
        {
            var json = JsonUtility.ToJson(data, true);
            try
            {
                if (useEncrypt)
                {
                    json = ToEncrypt(json.ToBytesUTF8()).ToStringUTF8();
                }

                PlayerPrefs.SetString(key, json);
                PlayerPrefs.Save();
                MornSaveGlobal.Log($"PlayerPrefs保存成功: {key}");
                return true;
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"PlayerPrefs保存時にエラー発生: {e.Message}");
            }

            MornSaveGlobal.LogError($"PlayerPrefs保存失敗: {key}");
            return false;
        }

        public static bool SaveToPlayerPrefs(string key, byte[] data, bool useEncrypt = false)
        {
            try
            {
                if (useEncrypt)
                {
                    data = ToEncrypt(data);
                }

                PlayerPrefs.SetString(key, data.ToStringBase64());
                PlayerPrefs.Save();
                MornSaveGlobal.Log($"PlayerPrefs保存成功: {key}");
                return true;
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"PlayerPrefs保存時にエラー発生: {e.Message}");
            }

            MornSaveGlobal.LogError($"PlayerPrefs保存失敗: {key}");
            return false;
        }

        public static bool SaveToFile<T>(string filePath, T data, bool useEncrypt = false) where T : class
        {
            var json = JsonUtility.ToJson(data, true);
            return SaveToFile(filePath, json.ToBytesUTF8(), useEncrypt);
        }

        public static bool SaveToFile(string filePath, byte[] data, bool useEncrypt = false)
        {
            try
            {
                EnsurePath(filePath);
                if (useEncrypt)
                {
                    data = ToEncrypt(data);
                }

                File.WriteAllBytes(filePath, data);
                MornSaveGlobal.Log($"ファイル保存成功: {filePath}");
                return true;
            }
            catch (Exception e)
            {
                MornSaveGlobal.LogError($"ファイル保存時にエラー発生: {e.Message}");
            }

            MornSaveGlobal.LogError("ファイル保存失敗");
            return false;
        }
    }
}