using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using MahaWiki.Activities;
using Android.Webkit;

namespace MahaWiki
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button butOK;
        private WebView webView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            webView = FindViewById<WebView>(Resource.Id.webView2);
            var htmlText = "<body><h1>महाराष्ट्र संस्कृतीच्या पाऊलखुणा</h1><p>"+
                "समाजबदलांमुळे, वाढत्या जागतिकीकरणामुळे मराठी भाषेतील अनेक शब्द लोप पावत चालले आहेत. उदाहरणार्थ पडवी, माजघर..अनेक वस्तू कालबाह्य होत चाललेल्या आहेत.उदाहरणार्थ उखळ, पाटा वरवंटा."+
                "अनेक पाककृती नामशेष होत चाललेल्या आहेत उदाहरणार्थ मांडे, पेंडपाला.<p>" +
                "जुने मराठी साहित्य वाचताना या विस्मरणात चाललेल्या गोष्टींची कोठेतरी नोंद ठेवण्याची गरज आहे अशी जाणिव झाली.<p>"+
                "<b> हा मराठी शब्दकोष नाही.</b>फक्त विस्मरणात चाललेल्या मराठी संस्कृतीच्या पाऊलखुणा...</body>";
            webView.LoadDataWithBaseURL("blarg://ignored", htmlText, "text/html", "utf-8", null);
            butOK = FindViewById<Button>(Resource.Id.butOK);
            butOK.Click += delegate
                {
                    StartActivity(typeof(Content));
                };
  
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}