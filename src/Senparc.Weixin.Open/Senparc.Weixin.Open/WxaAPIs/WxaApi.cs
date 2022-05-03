﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
  
    文件名：WxaApi.cs
    文件功能描述：小程序接口
    
    
    创建标识：Senparc - 20191009

    修改标识：mc7246 - 20191009
    修改描述：小程序审核事件移动到Senparc.Weixin.WxOpen，第三方平台-扫码关注组件，小程序支付后获取unionid 
              Commit: #0f282806008a4fff51e113add9f9e52379b929e7

    修改标识：mc7246 - 20211209
    修改描述：v4.13.2 添加“小程序违规和申诉管理”接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 小程序接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class WxaApi
    {
        #region 同步方法
        #region 扫码关注组件
        /// <summary>
        /// 【同步方法】获取展示的公众号信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetShowWxaItemJsonResult GetShowWxaItem(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/wxa/getshowwxaitem?access_token={accessToken.AsUrlData()}";


            return CommonJsonSend.Send<GetShowWxaItemJsonResult>(null, url, null);
        }

        /// <summary>
        /// 【同步方法】获取可以用来设置的公众号列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="page">页码，从 0 开始</param>
        /// <param name="num">每页记录数，最大为 20</param>
        /// <returns></returns>
        public static GetWxaMpLinkForShowJsonRsult GetWxaMpLinkForShow(string accessToken, int page, int num)
        {
            var url = $"{Config.ApiMpHost}/wxa/getwxamplinkforshow?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                page = page,
                num = num
            };

            return CommonJsonSend.Send<GetWxaMpLinkForShowJsonRsult>(null, url, data, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 【同步方法】设置展示的公众号信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="wxa_subscribe_biz_flag">是否打开扫码关注组件，0 关闭，1 开启</param>
        /// <param name="appid">如果开启，需要传新的公众号 appid</param>
        /// <returns></returns>
        public static WxJsonResult UpdateShowWxaItem(string accessToken, int wxa_subscribe_biz_flag, string appid)
        {
            var url = $"{Config.ApiMpHost}/wxa/updateshowwxaitem?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                wxa_subscribe_biz_flag = wxa_subscribe_biz_flag,
                appid = appid
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        #endregion
        #region 违规和申诉记录
        /// <summary>
        /// 获取小程序违规处罚记录
        /// 如果start_time和end_time都没有指定，则表示查询距离当前时间最近的90天内的记录。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="start_time">查询时间段的开始时间，如果不填，则表示end_time之前90天内的记录</param>
        /// <param name="end_time">查询时间段的结束时间，如果不填，则表示start_time之后90天内的记录</param>
        /// <returns></returns>
        public static GetIllegalRecordsJsonResult GetIllegalRecords(string accessToken, long? start_time, long? end_time)
        {
            var url = $"{Config.ApiMpHost}/wxa/getillegalrecords?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                start_time = start_time,
                end_time = end_time
            };

            return CommonJsonSend.Send<GetIllegalRecordsJsonResult>(null, url, data);
        }

        /// <summary>
        /// 获取小程序申诉记录
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="illegal_record_id">违规处罚记录id（通过getillegalrecords接口返回的记录id）</param>
        /// <returns></returns>
        public static GetAppealRecordsJsonResult GetAppealRecords(string accessToken, string illegal_record_id)
        {
            var url = $"{Config.ApiMpHost}/wxa/getappealrecords?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                illegal_record_id
            };

            return CommonJsonSend.Send<GetAppealRecordsJsonResult>(null, url, data);
        }

        #endregion
        #region 隐私接口
        /// <summary>
        /// 获取隐私接口列表
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/apply_api/get_privacy_interface.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetPrivacyInterfaceJsonResult GetPrivacyInterface(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/wxa/security/get_privacy_interface?access_token={accessToken.AsUrlData()}";


            return CommonJsonSend.Send<GetPrivacyInterfaceJsonResult>(null, url, null, CommonJsonSendType.GET);

        }

        /// <summary>
        /// 申请隐私接口
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/apply_api/apply_privacy_interface.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="api_name">申请的api英文名，例如wx.choosePoi，严格区分大小写</param>
        /// <param name="content">申请说原因，不超过300个字符；需要以utf-8编码提交，否则会出现审核失败</param>
        /// <param name="url_list">(辅助网页)例如，上传官网网页链接用于辅助审核</param>
        /// <param name="pic_list">(辅助图片)填写图片的url ，最多10个</param>
        /// <param name="video_list">(辅助视频)填写视频的链接 ，最多支持1个；视频格式只支持mp4格式</param>
        /// <returns></returns>
        public static ApplyPrivacyInterfaceJsonResult ApplyPrivacyInterface(string accessToken, string api_name, string content, List<string> url_list = null,List<string> pic_list=null,List<string> video_list=null)
        {
            var url = $"{Config.ApiMpHost}/wxa/security/apply_privacy_interface?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                api_name,
                content,
                url_list,
                pic_list,
                video_list
            };

            return CommonJsonSend.Send<ApplyPrivacyInterfaceJsonResult>(null, url, data);
        }
        #endregion
        #endregion

        #region 异步方法
        #region 扫码关注组件
        /// <summary>
        /// 【异步方法】获取展示的公众号信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<GetShowWxaItemJsonResult> GetShowWxaItemAsync(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/wxa/getshowwxaitem?access_token={accessToken.AsUrlData()}";


            return await CommonJsonSend.SendAsync<GetShowWxaItemJsonResult>(null, url, null).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取可以用来设置的公众号列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="page">页码，从 0 开始</param>
        /// <param name="num">每页记录数，最大为 20</param>
        /// <returns></returns>
        public static async Task<GetWxaMpLinkForShowJsonRsult> GetWxaMpLinkForShowAsync(string accessToken, int page, int num)
        {
            var url = $"{Config.ApiMpHost}/wxa/getwxamplinkforshow?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                page = page,
                num = num
            };

            return await CommonJsonSend.SendAsync<GetWxaMpLinkForShowJsonRsult>(null, url, data, CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】设置展示的公众号信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="wxa_subscribe_biz_flag">是否打开扫码关注组件，0 关闭，1 开启</param>
        /// <param name="appid">如果开启，需要传新的公众号 appid</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpdateShowWxaItemAsync(string accessToken, int wxa_subscribe_biz_flag, string appid)
        {
            var url = $"{Config.ApiMpHost}/wxa/updateshowwxaitem?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                wxa_subscribe_biz_flag = wxa_subscribe_biz_flag,
                appid = appid
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion
        #region 违规和申诉记录
        /// <summary>
        /// 【异步方法】获取小程序违规处罚记录
        /// 如果start_time和end_time都没有指定，则表示查询距离当前时间最近的90天内的记录。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="start_time">查询时间段的开始时间，如果不填，则表示end_time之前90天内的记录</param>
        /// <param name="end_time">查询时间段的结束时间，如果不填，则表示start_time之后90天内的记录</param>
        /// <returns></returns>
        public static async Task<GetIllegalRecordsJsonResult> GetIllegalRecordsAsync(string accessToken, long? start_time, long? end_time)
        {
            var url = $"{Config.ApiMpHost}/wxa/getillegalrecords?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                start_time = start_time,
                end_time = end_time
            };

            return await CommonJsonSend.SendAsync<GetIllegalRecordsJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取小程序申诉记录
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="illegal_record_id">违规处罚记录id（通过getillegalrecords接口返回的记录id）</param>
        /// <returns></returns>
        public static async Task<GetAppealRecordsJsonResult> GetAppealRecordsAsync(string accessToken, string illegal_record_id)
        {
            var url = $"{Config.ApiMpHost}/wxa/getappealrecords?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                illegal_record_id
            };

            return await CommonJsonSend.SendAsync<GetAppealRecordsJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion
        #region 隐私接口
        /// <summary>
        /// 【异步方法】获取隐私接口列表
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/apply_api/get_privacy_interface.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<GetPrivacyInterfaceJsonResult> GetPrivacyInterfaceAsync(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/wxa/security/get_privacy_interface?access_token={accessToken.AsUrlData()}";


            return await CommonJsonSend.SendAsync<GetPrivacyInterfaceJsonResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】申请隐私接口
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/apply_api/apply_privacy_interface.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="api_name">申请的api英文名，例如wx.choosePoi，严格区分大小写</param>
        /// <param name="content">申请说原因，不超过300个字符；需要以utf-8编码提交，否则会出现审核失败</param>
        /// <param name="url_list">(辅助网页)例如，上传官网网页链接用于辅助审核</param>
        /// <param name="pic_list">(辅助图片)填写图片的url ，最多10个</param>
        /// <param name="video_list">(辅助视频)填写视频的链接 ，最多支持1个；视频格式只支持mp4格式</param>
        /// <returns></returns>
        public static async Task<ApplyPrivacyInterfaceJsonResult> ApplyPrivacyInterfaceAsync(string accessToken, string api_name, string content, List<string> url_list = null, List<string> pic_list = null, List<string> video_list = null)
        {
            var url = $"{Config.ApiMpHost}/wxa/security/apply_privacy_interface?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                api_name,
                content,
                url_list,
                pic_list,
                video_list
            };

            return await CommonJsonSend.SendAsync<ApplyPrivacyInterfaceJsonResult>(null, url, data).ConfigureAwait(false);
        }
        #endregion

        #endregion

    }
}
