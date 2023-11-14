using StereoKit;
using StereoKit.Framework;
using System;

namespace WhackAMole.GameObjects
{
	internal class Hammer : IDrawable
	{

		
		public bool Enabled => true;

		public Model Model { get => _model; set => _model=value; }
		public Material Material { get => _material; set => _material=value; }
		public Pose Pose { get => _pose; set => _pose=value; }


		private Model _model;
		private Material _material;

		private Pose hammerCenterObjectSpace = new Pose(Vec3.Up*0.28f);
		private Pose _pose;

		public Mole[] moles;


		public Hammer()
		{
			SK.AddStepper(this);
		}

		public bool Initialize()
		{
			_model = Model.FromFile("Hammer.glb");
			_material = Material.Default.Copy();
			_material[MatParamName.DiffuseTex] = Tex.FromFile("Hammer_Color.png");
			_material[MatParamName.RoughnessAmount] = Tex.FromFile("Hammer_Metallic.png");
			
			return true;
		}

		public void Shutdown()
		{

		}

		private void Draw()
		{
			_model.Draw(_material, Matrix.S(0.1f) * _pose.ToMatrix(), Color.White);
		}

		public void Step()
		{
			Hand rightHand = Input.Hand(Handed.Right);
			_pose = rightHand.palm;

			Draw();

			CheckIfHitting();
		}

		private void CheckIfHitting()
		{
			var hammerCenterPose=(hammerCenterObjectSpace.ToMatrix()* _pose.ToMatrix());

			//Lines.AddAxis(hammerCenterPose.Pose, 0.5f, 0.01f);

			float raySize = 0.03f;

			Ray worldSpaceRayCenter = new Ray(hammerCenterPose * (-Vec3.Right*0.15f), (hammerCenterPose * Vec3.Right - hammerCenterPose * (-Vec3.Right))*0.15f); //Go through the hammer head

			Ray worldSpaceRayUp = new Ray(worldSpaceRayCenter.position + (hammerCenterPose * Vec3.Up) * raySize, worldSpaceRayCenter.direction);
			Ray worldSpaceRayDown = new Ray(worldSpaceRayCenter.position + (hammerCenterPose * Vec3.Up) * -raySize, worldSpaceRayCenter.direction);


			Ray worldSpaceRayLeft = new Ray(worldSpaceRayCenter.position + (hammerCenterPose * Vec3.Forward) * -raySize, worldSpaceRayCenter.direction);
			Ray worldSpaceRayRight = new Ray(worldSpaceRayCenter.position + (hammerCenterPose * Vec3.Forward) * raySize, worldSpaceRayCenter.direction);




			foreach (Mole mole in moles)
			{

				mole.CheckIntersection(new Ray[]{ worldSpaceRayCenter ,worldSpaceRayDown,worldSpaceRayLeft,worldSpaceRayRight,worldSpaceRayUp},out Vec3[] _);

			}
		}
	}
}
