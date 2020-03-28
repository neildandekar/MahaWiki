using Android.App;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LayoutParams = Android.Views.ViewGroup.LayoutParams;
using System.Runtime.Remoting.Contexts;

namespace MahaWiki.Activities
{
    [Activity(Label = "Content")]
    public class Content : Activity
    {
        private Button butGet;
        private RadioButton rbMarathi;
        private RadioButton rbEnglish;
        private PopupWindow _popupImage;
        private bool isMarathi;
        private bool isEnglish;
        private EditText etSearch;
        private WebView htmlContent;
        private ViewStub viewStub;
        private content cont = new content();
        private Dictionary<string,string> strCont = new Dictionary<string, string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Content);
            isEnglish = true;
            htmlContent = FindViewById<WebView>(Resource.Id.webView1);
            butGet = FindViewById<Button>(Resource.Id.butGet);
            rbMarathi = FindViewById<RadioButton>(Resource.Id.rbMarathi);
            rbEnglish = FindViewById<RadioButton>(Resource.Id.rbEnglish);
            etSearch = FindViewById<EditText>(Resource.Id.etSearch);
            butGet.Click += async delegate
            {
                InputMethodManager inputManager = (InputMethodManager)GetSystemService(InputMethodService);
                inputManager.HideSoftInputFromWindow(etSearch.WindowToken, 0);   

                if (etSearch.Text.ToString().Length < 1) { Toast.MakeText(this, "Please enter the search string", ToastLength.Short).Show(); return; }
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
                inDict.TryGetValue("description", out ph);
                if (ph != null && ph.Length > 0)
                {
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

                    sb.Append("<h2>हा शब्द या भागांत वापरला जातो</h2><p>");
                    inDict.TryGetValue("usage Region", out ph);
                    sb.Append(ph);
                    sb.Append("<p>");

                    sb.Append("<h2>हाही शब्द पहावा</h2><p>");
                    inDict.TryGetValue("aka", out ph);
                    sb.Append(ph);
                    sb.Append("<p>");
                    sb.Append("</body>");
                    return sb.ToString();
                }
                else
                {
                    sb.Append("<body>");
                    sb.Append("<h1>या शब्दाचे विवरण सध्या उपलब्ध नाही</h1><p>");
                    sb.Append("आपल्याला जर या शब्दाचे विवरण देण्यात स्वारस्य असेल तर, आपले नाव आणि गाव यासकट खालील पत्त्यावर संदेश पाठवावा. आपल्या नावाचा उल्लेख विवरणात केला जाईल.");
                    sb.Append("या सामुदायिक प्रयासात आपला सहभाग आवश्यक आहे.<p>");
                    sb.Append("neil.dandekar@outlook.com");
                    return sb.ToString();
                }
            }
        }
    }
}