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
    internal class MainScene : Scene
    {

		public MainScene()
		{

			gameObjects = new List<IStepper>
			{
				new WhackAMoleBox(),
				new Hammer(),
			};

		}


		public override bool Initialize()
		{
			SHLight skyLight = new SHLight();
			skyLight.color = Color.White;
			skyLight.directionTo = new Vec3(0.25f, 1, 0.25f);
			SphericalHarmonics skylight =SphericalHarmonics.FromLights(new SHLight[] { skyLight });
			Renderer.SkyLight = skylight;

			((Hammer)gameObjects[1]).moles = ((WhackAMoleBox)gameObjects[0]).moles;

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
