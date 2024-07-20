using UnityEngine;

namespace SaveSystemTutorial
{    
    public class PlayerData : MonoBehaviour
    {        
        #region Fields

        [SerializeField] string playerName = "Player Name";
        [SerializeField] int level = 0;
        [SerializeField] int coin = 0;

        [System.Serializable] //要生成Json的类必须要序列化
        class SaveData
        {
            public string playerName;
            public int playerLevel;
            public int playerCoin;
            public Vector3 playerPosition;
            [SerializeField]private int school; //私有的不会被生成Json,除非加上[SerializeField]。要传给Json的都用[SerializeField]就行了。
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
            //方法二

            SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, SavingData());
            //方法一
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
            //方法二
            var json = SaveSystem.LoadByPlayerPrefs(PLAYER_DATA_KEY);// 使用SaveSystem.LoadByPlayerPrefs(PLAYER_DATA_KEY)不用new是因为LoadByPlayerPrefs是static类，不用实例化就可以用。工具类一般设定为static。
            var saveData = JsonUtility.FromJson<SaveData>(json);
            //给到Unity
            LoadData(saveData);
            //方法一
            //playerName = PlayerPrefs.GetString("PlayerName", "somebody");//第二个参数是数据表中没有这个key的时候默认读成
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
            //SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());//单存档
            SaveSystem.SaveByJson($"{System.DateTime.Now:yyyy.dd.M HH-mm-ss}.json", SavingData()); //多存档
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

        //删除PlayerPrefsData
        [UnityEditor.MenuItem("DataController/Delete Player Data (regedit)")]//记得静态函数才生效
        public static void DeleteByPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        //删除JsonData
        [UnityEditor.MenuItem("DataController/Delete Player Data (Json File)")] 
        public static void DeleteByJson()
        {
            SaveSystem.DeleteByJson(PLAYER_DATA_FILE_NAME);
        }

        #endregion
    }
}