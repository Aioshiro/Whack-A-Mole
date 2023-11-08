using StereoKit;
using StereoKit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhackAMole.Scenes
{
    internal abstract class Scene : StereoKit.Framework.IStepper
    {

        protected List<IStepper> gameObjects;

		public bool Enabled { get; private set; }


        public abstract bool Initialize();

		public abstract void Shutdown();


		public virtual void Step()
        {
        }

    }

}
