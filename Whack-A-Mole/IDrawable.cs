using StereoKit;
using StereoKit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhackAMole
{
	internal interface IDrawable : IStepper
	{
		Model Model
		{
			get;
			set;
		}

		Material Material
		{
			get;
			set;
		}

		Pose Pose
		{
			get;
			set;
		}


		public virtual void Draw()
		{

		}
	}
}
