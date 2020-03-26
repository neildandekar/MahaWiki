using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Text;
using Android.Text;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MahaWiki.Activities
{
    [Activity(Label = "Content")]
    public class Content : Activity
    {
        private Button butGet;
        private RadioButton rbMarathi;
        private RadioButton rbEnglish;
        private ImageView ivImage;
        private bool isMarathi;
        private bool isEnglish;
        private EditText etSearch;
        private WebView htmlContent;
        private content cont = new content();
        private Dictionary<string,string> strCont = new Dictionary<string, string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Content);
            isEnglish = true;
            htmlContent = FindViewById<WebView>(Resource.Id.webView1);
            butGet = FindViewById<Button>(Resource.Id.butGet);
            ivImage = FindViewById<ImageView>(Resource.Id.ivImage);
            ivImage.SetImageResource(Resource.Drawable.Parat);
            rbMarathi = FindViewById<RadioButton>(Resource.Id.rbMarathi);
            rbEnglish = FindViewById<RadioButton>(Resource.Id.rbEnglish);
            etSearch = FindViewById<EditText>(Resource.Id.etSearch);
            butGet.Click += async delegate
            {
                etSearch.ClearFocus();
                HttpClient client = new HttpClient();
                string url = null;
                if (isMarathi)
                {
                    url = "https://mahawikiapi20200324114734.azurewebsites.net/api/MH?ukey=" + etSearch.Text.ToString();
                }
                else
                {
                    url = "https://mahawikiapi20200324114734.azurewebsites.net/api/MH?akey=" + etSearch.Text.ToString();
                }
                var result = await client.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();
                try
                {
                    //cont = Newtonsoft.Json.JsonConvert.DeserializeObject<content>(json);
                    strCont = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Failure from server", ToastLength.Short).Show();
                }

                htmlContent.LoadDataWithBaseURL("blarg://ignored", convertToHTML(strCont), "text/html", "utf-8", null);
            };
            rbMarathi.Click += delegate 
            {
                isMarathi = true;
                isEnglish = false;
                butGet.Text = "शोधा";
            };
            rbEnglish.Click += delegate 
            {
                isEnglish = true;
                isMarathi = false;
                butGet.Text = "Find";
            };
            etSearch.Click += delegate 
            {
                etSearch.Text = "";
            };
             string convertToHTML(Dictionary<string,string> inDict)
            {
                string ph;
                StringBuilder sb = new StringBuilder();
                sb.Append("<body>");
                sb.Append("<p>");
                inDict.TryGetValue("uncKey", out ph);
                sb.Append("<h1>");
                inDict.TryGetValue("uncKey", out ph);
                sb.Append(ph);
                sb.Append("/");
                inDict.TryGetValue("aka", out ph);
                sb.Append(ph);
                sb.Append("/");
                inDict.TryGetValue("ansiKey", out ph);
                sb.Append(ph);
                sb.Append("</h1>");

                sb.Append("<h2>या बाबतचे विवरण</h2><p>");
                inDict.TryGetValue("description", out ph);
                sb.Append(ph);
                sb.Append("<p>");

                sb.Append("<h2>वर्गीकरण</h2><p>");
                inDict.TryGetValue("category", out ph);
                sb.Append(ph);
                sb.Append("<p>");

                sb.Append("<h2>अधिक माहिती</h2><p>");
                inDict.TryGetValue("notes", out ph);
                sb.Append(ph);
                sb.Append("<p>");

                sb.Append("</body>");

                sb.Append("<h2>हा शब्द या भागांत वापरला जातो</h2><p>");
                inDict.TryGetValue("usage Region", out ph);
                sb.Append(ph);
                sb.Append("<p>");

                sb.Append("<h2>हाही शब्द पहावा</h2><p>");
                inDict.TryGetValue("aka", out ph);
                sb.Append(ph);
                sb.Append("<p>");

                return sb.ToString();
            }
            // Create your application here, 
        }
    }
}