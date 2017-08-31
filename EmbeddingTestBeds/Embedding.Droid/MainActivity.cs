﻿using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Embedding.XF;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using View = Android.Views.View;
using Button = Android.Widget.Button;
using Debug = System.Diagnostics.Debug;

namespace Embedding.Droid
{
	// TODO hartez 2017/08/31 12:01:27 Enable warnings as errors in Embedding projects	
	[Activity(Label = "Embedding.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : FragmentActivity
	{
		Fragment _hello;
		Fragment _alertsAndActionSheets;
		Fragment _openUri;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Forms.Init(this, null);

			Debug.WriteLine($">>>>> MainActivity OnCreate 25: {this}, Forms.Context is {Forms.Context}");

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			
			var ft = SupportFragmentManager.BeginTransaction();
			ft.Replace(Resource.Id.fragment_frame_layout, new MainFragment(), "main");
			ft.Commit();
		}

		protected override void OnStart()
		{
			Debug.WriteLine($">>>>> MainActivity OnStart 42: {this}, Forms.Context is {Forms.Context}");
			base.OnStart();
		}

		protected override void OnRestart()
		{
			Debug.WriteLine($">>>>> MainActivity OnRestart 48: {this}, Forms.Context is {Forms.Context}");
			base.OnRestart();
		}

		protected override void OnResume()
		{
			Debug.WriteLine($">>>>> MainActivity OnResume 54: {this}, Forms.Context is {Forms.Context}");
			base.OnResume();
		}

		protected override void OnDestroy()
		{
			Debug.WriteLine($">>>>> MainActivity OnDestroy 61: {this}, Forms.Context is {Forms.Context}");
			base.OnDestroy();
		}

		public void ShowHello()
		{
			// Create a XF Hello page as a fragment
			if (_hello == null)
			{
				_hello = new Hello().CreateSupportFragment(this);
			}

			ShowEmbeddedPageFragment(_hello);
		}

		public void ShowOpenUri()
		{
			if (_openUri == null)
			{
				_openUri = new OpenUri().CreateSupportFragment(this);
			}

			ShowEmbeddedPageFragment(_openUri );
		}

		public void ShowAlertsAndActionSheets()
		{
			if (_alertsAndActionSheets== null)
			{
				_alertsAndActionSheets = new AlertsAndActionSheets().CreateSupportFragment(this);
			}

			ShowEmbeddedPageFragment(_alertsAndActionSheets);
		}

		void ShowEmbeddedPageFragment(Fragment fragment)
		{
			FragmentTransaction ft = SupportFragmentManager.BeginTransaction();

			ft.AddToBackStack(null);
			ft.Replace(Resource.Id.fragment_frame_layout, fragment, "hello");
			
			ft.Commit();
		}

		public void LaunchSecondActivity()
		{
			StartActivity(typeof(SecondActivity));
		}
	}

	public class MainFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view =  inflater.Inflate(Resource.Layout.MainFragment, container, false);
			var showEmbeddedButton = view.FindViewById<Button>(Resource.Id.showEmbeddedButton);
			var showAlertsActionSheets = view.FindViewById<Button>(Resource.Id.showAlertsActionSheets);
			var showOpenUri = view.FindViewById<Button>(Resource.Id.showOpenUri);
			var launchSecondActivity = view.FindViewById<Button>(Resource.Id.launchSecondActivity);

			showEmbeddedButton.Click += ShowEmbeddedClick;
			showAlertsActionSheets.Click += ShowAlertsActionSheetsClick;
			showOpenUri.Click += ShowOpenUriClick;
			launchSecondActivity.Click += LaunchSecondActivityOnClick;

			return view;
		}

		void LaunchSecondActivityOnClick(object sender, EventArgs e)
		{
			((MainActivity)Activity).LaunchSecondActivity();
		}

		void ShowAlertsActionSheetsClick(object sender, EventArgs eventArgs)
		{
			((MainActivity)Activity).ShowAlertsAndActionSheets();
		}

		void ShowEmbeddedClick(object sender, EventArgs e)
		{
			((MainActivity)Activity).ShowHello();
		}

		void ShowOpenUriClick(object sender, EventArgs e)
		{
			((MainActivity)Activity).ShowOpenUri();
		}
	}
}

