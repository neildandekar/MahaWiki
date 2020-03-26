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
            var htmlText = "<body><h1>Contents</h1><p><h2>Create stickers for</h2><p><b>Gboard</b><p> For the board meeting" +
                        "fdsfadfadsf df dsfa df da sf adf  adfs ad f adf  dasf a f asdf a sf adsf a df a f adsaf" +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g<p>" +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g < p > " +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g < p > " +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g < p > " +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g < p > " +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g < p > " +
                        "sgsfg fgsfgs sfgsf dgs fgs fg fg f g sfg sf g sfd gs fg sf g g < p >  </body>";
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