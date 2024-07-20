using UnityEngine;

namespace SaveSystemTutorial
{    
    public class PlayerData : MonoBehaviour
    {        
        #region Fields

        [SerializeField] string playerName = "Player Name";
        [SerializeField] int level = 0;
        [SerializeField] int coin = 0;

        [System.Serializable] //Ҫ����Json�������Ҫ���л�
        class SaveData
        {
            public string playerName;
            public int playerLevel;
            public int playerCoin;
            public Vector3 playerPosition;
            [SerializeField]private int school; //˽�еĲ��ᱻ����Json,���Ǽ���[SerializeField]��Ҫ����Json�Ķ���[SerializeField]�����ˡ�
        }

        const string PLAYER_DATA_KEY = "PlayerData";
        private const string PLAYER_DATA_FILE_NAME = "PlayerData.json";

        #endregion

        #region Properties

        public string Name => playerName;

        public int Level => level;
        public int Coin => coin;

        public Vector3 Position => transform.position;

        #endregion

        #region Save and Load

        public void Save()
        {
            //SaveByPlayerPrefs();
            SaveByJson();
        }

        public void Load()
        {
            //LoadByPlayerPrefs();
            LoadByJson();
        }

        #endregion

        #region PlayerPrefs

        void SaveByPlayerPrefs()
        {
            //������

            SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, SavingData());
            //����һ
            //PlayerPrefs.SetString("PlayerName", playerName);
            //PlayerPrefs.SetInt("PlayerLevel", level);
            //PlayerPrefs.SetInt("PlayerCoin", coin);
            //PlayerPrefs.SetFloat("PlayerPositionX", transform.position.x);
            //PlayerPrefs.SetFloat("PlayerPositionY", transform.position.y);
            //PlayerPrefs.SetFloat("PlayerPositionZ", transform.position.z);
            //PlayerPrefs.Save();
            //---------------------

        }

        void LoadByPlayerPrefs()
        {
            //������
            var json = SaveSystem.LoadByPlayerPrefs(PLAYER_DATA_KEY);// ʹ��SaveSystem.LoadByPlayerPrefs(PLAYER_DATA_KEY)����new����ΪLoadByPlayerPrefs��static�࣬����ʵ�����Ϳ����á�������һ���趨Ϊstatic��
            var saveData = JsonUtility.FromJson<SaveData>(json);
            //����Unity
            LoadData(saveData);
            //����һ
            //playerName = PlayerPrefs.GetString("PlayerName", "somebody");//�ڶ������������ݱ���û�����key��ʱ��Ĭ�϶���
            //level = PlayerPrefs.GetInt("PlayerLevel", 0);
            //coin = PlayerPrefs.GetInt("PlayerCoin", 0);
            //transform.position = new Vector3(
            //    PlayerPrefs.GetFloat("PlayerPositionX", 0f),
            //    PlayerPrefs.GetFloat("PlayerPositionY", 0f),
            //    PlayerPrefs.GetFloat("PlayerPositionZ", 0f));
            //---------------------

        }


        #endregion

        #region Json

        void SaveByJson()
        {
            //SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());//���浵
            SaveSystem.SaveByJson($"{System.DateTime.Now:yyyy.dd.M HH-mm-ss}.json", SavingData()); //��浵
        }

        void LoadByJson()
        { 
            var saveData = SaveSystem.LoadByJson<SaveData>(PLAYER_DATA_FILE_NAME);
            LoadData(saveData);
        }

        #endregion

        #region Help Function

        SaveData SavingData()
        {
            SaveData saveData = new SaveData();

            saveData.playerName = playerName;
            saveData.playerLevel = level;
            saveData.playerCoin = coin;
            saveData.playerPosition = transform.position;

            return saveData;
        }

        private void LoadData(SaveData saveData)
        {
            playerName = saveData.playerName;
            level = saveData.playerLevel;
            coin = saveData.playerCoin;
            transform.position = saveData.playerPosition;
        }

        //ɾ��PlayerPrefsData
        [UnityEditor.MenuItem("DataController/Delete Player Data (regedit)")]//�ǵþ�̬��������Ч
        public static void DeleteByPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        //ɾ��JsonData
        [UnityEditor.MenuItem("DataController/Delete Player Data (Json File)")] 
        public static void DeleteByJson()
        {
            SaveSystem.DeleteByJson(PLAYER_DATA_FILE_NAME);
        }

        #endregion
    }
}