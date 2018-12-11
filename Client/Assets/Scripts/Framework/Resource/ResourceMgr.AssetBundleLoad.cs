/********************************************************************************
** auth:  https://github.com/HushengStudent
** date:  2018/12/09 16:12:18
** desc:  AssetBundle��Դ����;
*********************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MEC;
using Object = UnityEngine.Object;
using System.IO;

namespace Framework
{
    public partial class ResourceMgr
    {
        #region AssetBundle Load

        /// <summary>
        /// Asset sync load from AssetBundle;
        /// </summary>
        /// <typeparam name="T">ctrl</typeparam>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <returns>ctrl</returns>
        [Obsolete("Warning,Suggest use 'ResourceMgr.LoadAssetProxy' instead!")]
        public T LoadAssetSync<T>(AssetType assetType, string assetName) where T : Object
        {
            T ctrl = null;
            AssetBundle assetBundle = AssetBundleMgr.Instance.LoadAssetBundleSync(assetType, assetName);
            if (assetBundle != null)
            {
                var name = Path.GetFileNameWithoutExtension(assetName);
                T tempObject = assetBundle.LoadAsset<T>(name);
                ctrl = AssetLoader.GetAsset(assetType, tempObject);
            }
            if (ctrl == null)
            {
                LogHelper.PrintError(string.Format("[ResourceMgr]LoadAssetSync Load Asset {0} failure!", assetName));
            }
            return ctrl;
        }

        /// <summary>
        /// Asset�첽����;
        /// </summary>
        /// <typeparam name="T">ctrl</typeparam>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <returns>����</returns>
        public AssetAsyncProxy LoadAssetProxy<T>(AssetType assetType, string assetName) where T : Object
        {
            return LoadAssetProxy<T>(assetType, assetName, null, null);
        }

        /// <summary>
        /// Asset�첽����;
        /// </summary>
        /// <typeparam name="T">ctrl</typeparam>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <param name="action">��Դ�ص�</param>
        /// <returns>����</returns>
        public AssetAsyncProxy LoadAssetProxy<T>(AssetType assetType, string assetName, Action<T> action) where T : Object
        {
            return LoadAssetProxy<T>(assetType, assetName, action, null);
        }

        /// <summary>
        /// Asset�첽����;
        /// </summary>
        /// <typeparam name="T">ctrl</typeparam>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <param name="action">��Դ�ص�</param>
        /// <param name="progress">progress�ص�</param>
        /// <returns>����</returns>
        public AssetAsyncProxy LoadAssetProxy<T>(AssetType assetType, string assetName
            , Action<T> action, Action<float> progress) where T : Object
        {
            AssetAsyncProxy proxy = PoolMgr.Instance.Get<AssetAsyncProxy>();
            proxy.InitProxy(assetType, assetName);
            CoroutineMgr.Instance.RunCoroutine(LoadAssetAsync<T>(assetType, assetName, proxy, action, progress));
            return proxy;
        }

        /// <summary>
        /// Asset async load from AssetBundle;
        /// </summary>
        /// <typeparam name="T">ctrl</typeparam>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <param name="proxy">����</param>
        /// <param name="action">��Դ�ص�</param>
        /// <param name="progress">progress�ص�</param>
        /// <returns></returns>
        private IEnumerator<float> LoadAssetAsync<T>(AssetType assetType, string assetName, AssetAsyncProxy proxy
            , Action<T> action, Action<float> progress)
            where T : Object
        {
            T ctrl = null;
            AssetBundle assetBundle = null;

            //--------------------------------------------------------------------------------------
            //���ڼ��ص������������ֵ,����һ֡��;
            if (AsyncMgr.CurCount() > AsyncMgr.ASYNC_LOAD_MAX_VALUE)
                yield return Timing.WaitForOneFrame;

            var loadID = AsyncMgr.LoadID;
            AsyncMgr.Add(loadID);
            //--------------------------------------------------------------------------------------

            IEnumerator itor = AssetBundleMgr.Instance.LoadAssetBundleAsync(assetType, assetName,
                ab => { assetBundle = ab; }, progress);//�˴�����ռ90%;
            while (itor.MoveNext())
            {
                yield return Timing.WaitForOneFrame;
            }
            var name = Path.GetFileNameWithoutExtension(assetName);
            AssetBundleRequest request = assetBundle.LoadAssetAsync<T>(name);
            //�˴�����ռ10%;
            while (request.progress < 0.99)
            {
                if (progress != null)
                {
                    progress(0.9f + 0.1f * request.progress);
                }
                yield return Timing.WaitForOneFrame;
            }
            while (!request.isDone)
            {
                yield return Timing.WaitForOneFrame;
            }
            ctrl = AssetLoader.GetAsset(assetType, request.asset as T);
            if (null == ctrl)
            {
                LogHelper.PrintError(string.Format("[ResourceMgr]LoadAssetAsync Load Asset {0} failure!", assetName));
            }
            //--------------------------------------------------------------------------------------
            //�ȵ�һ֡;
            yield return Timing.WaitForOneFrame;
            var finishTime = AsyncMgr.GetCurTime();
            var timeOver = false;
            var isloading = AsyncMgr.IsContains(loadID);
            while (isloading && !timeOver && AsyncMgr.CurLoadID != loadID)
            {
                timeOver = AsyncMgr.IsTimeOverflows(finishTime);
                if (timeOver)
                {
                    LogHelper.PrintWarning(string.Format("[ResourceMgr]LoadAssetAsync excute callback over time, type:{0},name{1}."
                        , assetType, assetName));
                    break;
                }
                yield return Timing.WaitForOneFrame;
            }
            //--------------------------------------------------------------------------------------
            if (!proxy.isCancel && action != null)
            {
                action(ctrl);
            }
            if (proxy != null)
            {
                proxy.OnFinish(ctrl);
            }
            //--------------------------------------------------------------------------------------
            if (!isloading)
            {
                yield break;
            }
            if (timeOver && AsyncMgr.CurLoadID != loadID)
            {
                AsyncMgr.Remove(loadID);

                if (AsyncMgr.CurLoadTimeOverflows())
                {
                    AsyncMgr.CurLoadID = 0;
                }
            }
            else
            {
                AsyncMgr.CurLoadID = 0;
            }
            //--------------------------------------------------------------------------------------
        }

        #endregion
    }
}