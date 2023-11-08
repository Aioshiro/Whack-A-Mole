using StereoKit;
using StereoKit.Framework;
using System;
using System.Diagnostics;
using WhackAMole.GameObjects;
using WhackAMole.Scenes;

namespace WhackAMole
{
    internal class Program
	{
		static void Main(string[] args)
		{
			// Initialize StereoKit
			SKSettings settings = new SKSettings
			{
				appName = "Whack_A_Mole",
				assetsFolder = "Assets",
			};

			/*
			StartingScene startingScene = new StartingScene();
			SK.AddStepper(startingScene as IStepper);
			*/

			MainScene mainScene = new MainScene();
			SK.AddStepper(mainScene);

			if (!SK.Initialize(settings))
				Environment.Exit(1);


			// Core application loop
			while (SK.Step(() =>
			{

			})) ;
			SK.Shutdown();
		}
	}
}
