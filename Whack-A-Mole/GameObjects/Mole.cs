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
			MoleMaterial.SetColor("color", Color.HSV(40/100, 70/100, 80/100));
			this.initialTransform = Matrix;
			currentTransform = initialTransform;
			SK.AddStepper(this);
		}

		public bool Enabled => true;

		public bool Initialize()
		{
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
			bounds.Scale(0.1f);
			bool hasHit = bounds.Intersect(worldSpaceRay, out at);
			if (hasHit)
			{
				OnHitByHammer();
			}
			else
			{
				Console.WriteLine("not hit !");
			}
			return hasHit;
		}

		private void OnHitByHammer()
		{
			Console.WriteLine("hit by hammer !");
		}
	}
}
