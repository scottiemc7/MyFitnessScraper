﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace MyFitnessScraper
{
	public class HttpDataGrabber
	{
		private readonly string _userName;
		private readonly string _password;
        private readonly IHttpWebRequestFactory _requestFactory;

		private HttpDataGrabber() { }
        public HttpDataGrabber(IHttpWebRequestFactory requestFactory, string userName, string password)
		{
			_userName = userName;
			_password = password;
            _requestFactory = requestFactory;
		}

		private Cookie _authCookie = null;
		private Cookie AuthCookie
		{
			get
			{
				if (_authCookie == null)
				{
					//Log in
                    IHttpWebRequestProxy login = _requestFactory.Create("http://www.myfitnesspal.com/account/login");					
					string strPostData = String.Format("username={0}&password={1}&remember_me={2}", _userName, _password, "false");

					login.Method = "POST";
					login.ContentType = "application/x-www-form-urlencoded";
					login.ContentLength = strPostData.Length;
					login.CookieContainer = new CookieContainer();
					using (StreamWriter sw = new StreamWriter(login.GetRequestStream()))
					{
						sw.Write(strPostData);
						sw.Close();
					}//end using

					using (IHttpWebResponseProxy loginResponse =login.GetResponse())
					{
						foreach (Cookie cookie in loginResponse.Cookies)
						{
							if (String.Compare("_myfitnesspal_session", cookie.Name, true) == 0)
							{
								_authCookie = cookie;
								break;
							}//end if
						}//end foreach				   
					}//end using
				}//end if

				return _authCookie;
			}//end get
		}

		public string GrabFoodDataForDate(DateTime date)
		{
            IHttpWebRequestProxy request = _requestFactory.Create(String.Format("http://www.myfitnesspal.com/food/diary/{0}?date={1}", _userName, date.ToShortDateString()));
            
			request.CookieContainer = new CookieContainer();
			request.CookieContainer.Add(AuthCookie);
            using (IHttpWebResponseProxy response = request.GetResponse())
			using (Stream str = response.GetResponseStream())
			using (StreamReader read = new StreamReader(str, Encoding.UTF8))
			{
				return read.ReadToEnd();
			}//end using
		}
    }
}
