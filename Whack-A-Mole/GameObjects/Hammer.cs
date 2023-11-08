﻿using StereoKit;
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

		private Pose hammerCenteObjectSpace = new Pose(Vec3.Up*0.2f);
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
			Pose hammerCenterPose=(hammerCenteObjectSpace.ToMatrix() * _pose.ToMatrix()).Pose;

			Lines.AddAxis(hammerCenterPose, 0.05f, 0.01f);

			Ray worldSpaceRayLeft = new Ray(hammerCenterPose.position, hammerCenterPose.position + hammerCenterPose.ToMatrix() * Vec3.Right * 0.15f - hammerCenterPose.position);

			Lines.Add(worldSpaceRayLeft, 1, Color.LAB(0,1,1),0.01f);

			foreach(Mole mole in moles)
			{
				mole.CheckIntersection(worldSpaceRayLeft, out Vec3 _);
			}
		}
	}
}
