using UnityEngine;
using System.IO;

namespace SaveSystemTutorial
{
    public static class SaveSystem
    {
        #region PlayerPrefs

        public static void SaveByPlayerPrefs(string key, object data)
        {
            var json = JsonUtility.ToJson(data,true); //ToJson�ǵô�����󰡡�
            Debug.Log(json);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();

            #if UNITY_EDITOR
                Debug.Log("Successfully saved data to PlayerPrefs.");
            #endif
        }

        public static string LoadByPlayerPrefs(string key)
        { 
            return PlayerPrefs.GetString(key, null);
        }

        #endregion

        #region Json

        public static void SaveByJson(string saveFileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.WriteAllText(path, json);

                #if UNITY_EDITOR
                    Debug.Log($"Successfully save the json file. path: {path}");
                #endif
            }
            catch(System.Exception exception)
            {
                #if UNITY_EDITOR
                    Debug.Log($"Fail to save the json file. path: {path}.\n{exception}");
                #endif
            }
        }

        public static T LoadByJson<T>(string saveFileName)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);

                return data;
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                    Debug.LogError($"Fail to load the json file. path: {path}.\n{exception}");
                #endif
                return default; //Ĭ��ֵbool�Ļ���false��������null��0��ȡ����T
            }
        }

        public static void DeleteByJson(string saveFileName)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                    Debug.LogError($"Fail to delete the json file. path: {path}.\n{exception}");
                #endif
            }
        }

        #endregion
    }
}
