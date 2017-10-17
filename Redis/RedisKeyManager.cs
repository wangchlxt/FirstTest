using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    /// <summary>
    /// redis Kes管理类，统一命名
    /// </summary>
   public class RedisKeyManager
    {
        ///// <summary>
        ///// 企业子账号的key
        ///// </summary>
        ///// <param name="userGrouId">企业子账号的id</param>
        ///// <returns></returns>
        //public static string GetUserGroupKey(int userGrouId)
        //{
        //    var key = string.Format("usergroup:{0}", userGrouId);
        //    return key;
        //}

        /// <summary>
        /// 企业子账号的key
        /// </summary>
        /// <param name="platform">企业子账号的id</param>
        /// <returns></returns>
        public static string GetPlatformTokenKey(string platform)
        {
            var key = string.Format("PfToken:{0}", platform);
            return key;
        }

        /// <summary>
        /// 企业子账号首页数据的key
        /// </summary>
        /// <param name="userGrouId">企业子账号的id</param>
        /// <returns></returns>
        public static string GetUserGroupDataKey(int userGroupId)
        {
            var key = string.Format("ug:{0}:cache", userGroupId);
            return key;
        }


        /// <summary>
        /// 销售员首页数据的key
        /// </summary>
        /// <param name="userGrouId">销售人员的userid</param>
        /// <returns></returns>
        public static string GetSalerDataKey(int userId)
        {
            var key = string.Format("Saler:{0}:cache", userId);
            return key;
        }

        /// <summary>
        /// 获取是否同步某供应商卡信息的key,1同步0不同步
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public static string GetSupplierUpdateCardKey(string supplier)
        {
            var key = string.Format("UpdateCard:{0}",supplier);
            return key;
        }

        /// <summary>
        /// 实时通知key
        /// </summary>
        /// <returns></returns>
        public static string GetNoticeKey()
        {
            var key = string.Format("notice:cache");
            return key;
        }

        /// <summary>
        /// 获取单个问题key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetQuestionKey(int id)
        {
            string key = string.Format("question:{0}", id);
            return key;
        }

        /// <summary>
        /// 获取所有问题键列表key
        /// </summary>
        /// <returns></returns>
        public static string GetQuestionKeyList()
        {
            return "question_key_list";
        }
    }
}
