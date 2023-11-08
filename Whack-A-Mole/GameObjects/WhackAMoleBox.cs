using StereoKit;
using StereoKit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhackAMole.GameObjects
{
	internal class WhackAMoleBox : IDrawable
	{

		public Model Model { get => _model; set => _model = value; }
		public Material Material { get => _material; set => _material = value; }
		public Pose Pose { get => _pose; set => _pose = value; }


		public Pose _pose = Pose.Identity;

		public Mole[] moles;


		private Model _model;
		private Material _material;

		public WhackAMoleBox()
		{
			SK.AddStepper(this);
		}


		private readonly Matrix[] molesLocalTransforms =
		{
			Matrix.T(new Vec3(0f,1f,0.05f))*Matrix.Identity,
			Matrix.T(new Vec3(0f,1f,0.55f))*Matrix.Identity,
			Matrix.T(new Vec3(0f,1f,-0.45f))*Matrix.Identity,
			Matrix.T(new Vec3(-0.5f,1f,0.05f))*Matrix.Identity,
			Matrix.T(new Vec3(-0.5f,1f,0.55f))*Matrix.Identity,
			Matrix.T(new Vec3(-0.5f,1f,-0.45f))*Matrix.Identity,

		};

		public bool Enabled => false;

		public bool Initialize()
		{
			moles = new Mole[molesLocalTransforms.Length];
			for (int i = 0; i < moles.Length; i++)
			{
				moles[i] = new Mole(molesLocalTransforms[i])
				{
					Box = this
				};
			}

			_model = Model.FromFile("Box.glb");
			_material = Material.Default.Copy();
			_material[MatParamName.DiffuseTex] = Tex.FromFile("Box_Color.png");
			_material[MatParamName.NormalTex] = Tex.FromFile("Box_Normal.png");

			return true;
		}

		public void Shutdown()
		{
			
		}

		public void Step()
		{
			Draw();
		}

		public void Draw()
		{
			_model.Draw(_material, _pose.ToMatrix(), Color.White);

		}
	}
}
