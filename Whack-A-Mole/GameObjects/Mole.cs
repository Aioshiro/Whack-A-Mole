using StereoKit;
using StereoKit.Framework;
using System;

namespace WhackAMole.GameObjects
{
	internal class Mole: IStepper
	{

		public WhackAMoleBox Box;

		private const float maxOutDistanceSqr = 0.025f;
		private const float epsilon = 0.001f;
		private float speed = 0.1f;

		private Matrix initialTransform;
		private Matrix currentTransform;

		private bool isGettingOut =false;
		private Material MoleMaterial;

		public Mole() : this(Matrix.Identity)
		{

		}

		public Mole(Matrix Matrix)
		{
			MoleMaterial = Material.Default.Copy();
			MoleMaterial.SetColor("color", Color.HSV(0, 1, 1, 1));
			this.initialTransform = Matrix;
			currentTransform = initialTransform;
			SK.AddStepper(this);
		}

		public bool Enabled { get => _Enabled; set => _Enabled = value; }
		private bool _Enabled = true;

		public bool Initialize()
		{
			_Enabled = true;
			return true;
		}

		public void Shutdown()
		{
			
		}

		public void Step()
		{
			Move();
			Draw();
		}

		private void Move()
		{
			if (isGettingOut)
			{
				currentTransform.Translation += Vec3.Up * (speed * Time.Stepf);
				if ((currentTransform.Translation - initialTransform.Translation).LengthSq > maxOutDistanceSqr)
				{
					isGettingOut = false;
				}
			}
			else
			{
				currentTransform.Translation -= Vec3.Up * (speed * Time.Stepf);
				if ((currentTransform.Translation - initialTransform.Translation).LengthSq < epsilon)
				{
					isGettingOut = true;
				}
			}
		}

		private void Draw()
		{
			Hierarchy.Push(Box.Pose.ToMatrix());
			Mesh moleMesh = Mesh.Cube;
			moleMesh.Draw(MoleMaterial, Matrix.S(Vec3.One * 0.1f)*currentTransform);
			Hierarchy.Pop();
		}

		public bool CheckIntersection(Ray worldSpaceRay,out Vec3 at)
		{
			Bounds bounds = Mesh.Cube.Bounds;
			Ray localSpaceRay = (Matrix.S(Vec3.One * 0.1f) * currentTransform).Inverse.Transform(worldSpaceRay);
			bool hasHit = bounds.Intersect(localSpaceRay, out at);
			return hasHit;
		}

		public bool CheckIntersection(Ray[] rays, out Vec3[] at)
		{
			at = new Vec3[rays.Length];
			bool wasItByOne = false;
			for(int i=0;i<rays.Length; i++)
			{
				wasItByOne = wasItByOne || CheckIntersection(rays[i], out at[i]);
			}
			if (wasItByOne)
			{
				OnHitByHammer();
			}
			else
			{
				MoleMaterial[MatParamName.ColorTint] = new Color(1, 0, 0, 1);
			}
			return wasItByOne;
		}

		private void OnHitByHammer()
		{
			MoleMaterial[MatParamName.ColorTint] = new Color(0, 1, 0, 1);

		}
	}
}
