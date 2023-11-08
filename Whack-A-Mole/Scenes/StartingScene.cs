using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhackAMole.GameObjects;
using StereoKit.Framework;

namespace WhackAMole.Scenes
{
    internal class StartingScene : Scene
    {
		public StartingScene()
		{

			gameObjects = new List<IStepper>();


			Pose windowPose = new(0, 0, -0.6f, Quat.LookDir(0, 0, 1));
			StartMenu startMenu = new(windowPose);


			gameObjects.Add(startMenu);



			startMenu.OnPressExitGame += (s, ee) => SK.Quit();

		}


		public override bool Initialize()
		{
			gameObjects = new List<IStepper>();


			Pose windowPose = new(0, 0, -0.6f, Quat.LookDir(0, 0, 1));
			StartMenu startMenu = new(windowPose);


			gameObjects.Add(startMenu);




			startMenu.OnPressExitGame += (s, ee) => SK.Quit();
			return true;
		}

		public override void Shutdown()
		{

		}


		public override void Step()
		{
			base.Step();
			Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
			Material floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
			floorMaterial.Transparency = Transparency.Blend;
			if (SK.System.displayType == Display.Opaque)
				Default.MeshCube.Draw(floorMaterial, floorTransform);
		}

	}


}
