using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class ListBiliPlayer
    {
        private static ListBiliPlayer instance;
        public static ListBiliPlayer Instance { get { return instance ??= new ListBiliPlayer(); } }

        /// <summary>
        /// 所有玩家数据
        /// </summary>
        private List<BiliPlayer> biliPlayers = new List<BiliPlayer>();

        /// <summary>
        /// 游戏队列中玩家
        /// </summary>
        private List<BiliPlayer> biliPlaying = new List<BiliPlayer>();


        public List<BiliPlayer> GetBiliPlayingPlayer()
        {
            return biliPlaying;
        }

        /// <summary>
        /// 设置淘汰
        /// </summary>
        public void SetFail()
        {
            biliPlaying.RemoveAt(0);
        }

        /// <summary>
        /// 结束游戏，清空队列玩家
        /// </summary>
        public void FinishGame()
        {
            biliPlaying.Clear();
        }

        /// <summary>
        /// 设置下一个玩家
        /// </summary>
        public void SetNextPlayer()
        {
            var player = biliPlaying.FirstOrDefault();
            biliPlaying.RemoveAt(0);
            biliPlaying.Add(player);
        }

        /// <summary>
        /// 获取当前玩家
        /// </summary>
        /// <returns></returns>
        public BiliPlayer GetFirstPlayeringPlayer()
        {
            return (BiliPlayer)biliPlaying.FirstOrDefault(); 
        }

        /// <summary>
        /// 获取第一个玩家
        /// </summary>
        /// <returns></returns>
        public BiliPlayer GetFirstPlayer()
        {
            return GetBiliPlayingPlayer().FirstOrDefault();
        }
        
        /// <summary>
        /// 初次加载库中所有记录
        /// </summary>
        public void LoadPlayerInfo()
        {
            List<BsonDocument> lst_doc = DBHelper.Instance.getAllInfo("rank");
            foreach (BsonDocument doc in lst_doc)
            {
                BiliPlayer player = new BiliPlayer
                {
                    uid = doc["uid"].AsInt64,
                    uname = doc["uname"].AsString,
                    uface = doc["uface"].AsString,
                    score = doc["score"].AsInt64
                };
                biliPlayers.Add(player);
            }
        }

        /// <summary>
        /// 获取缓存中记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public BiliPlayer GetBiliPlayer(long uid)
        {
            foreach (BiliPlayer player in biliPlayers)
            {
                if (player.uid == uid)
                {
                    return player;
                }
            }

            return null;
        }

        public BiliPlayer GetBiliPlayingPlayer(long uid)
        {
            foreach (BiliPlayer player in biliPlaying)
            {
                if (player.uid == uid)
                {
                    return player;
                }
            }

            return null;
        }

        /// <summary>
        /// 是否在游戏队列中
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool isPlayingPlayer(long uid)
        {
            foreach (BiliPlayer player in biliPlaying)
            {
                if (player.uid == uid)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 进入游戏队列
        /// </summary>
        /// <param name="player"></param>
        public void addPlayingPlayer(BiliPlayer player)
        {
            biliPlaying.Add(player); 
        }

        public void addAllPlayer(BiliPlayer player)
        {
            biliPlayers.Add(player);
        }
    }
}
