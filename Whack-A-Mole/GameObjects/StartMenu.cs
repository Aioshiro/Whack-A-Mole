using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using StereoKit.Framework;

namespace WhackAMole.GameObjects
{
    internal class StartMenu : IStepper
    {
        Pose windowPose = new Pose(0, 0, -0.6f, Quat.LookDir(0, 0, 1));
        bool showHeader = true;

        public EventHandler OnPressStartGame;
        public EventHandler OnPressExitGame;

		public bool Enabled => throw new NotImplementedException();

		public StartMenu()
        {
            Initialize();
        }
        public StartMenu(Pose initialPose = new Pose())
        {
            windowPose = initialPose;
			Initialize();
		}

		public bool Initialize()
        {
            OnPressExitGame += (s,ee)=> SK.Quit();
            return true;
        }


        public void Step()
        {
            UI.WindowBegin("Whack-A-Mole", ref windowPose, new Vec2(20, 0) * U.cm, showHeader ? UIWin.Normal : UIWin.Body);

            if (UI.Button("Start game"))
            {
                OnPressStartGame.Invoke(this,null);
            }


            if (UI.Button("Exit"))
            {
                OnPressExitGame.Invoke(this,null);
            }


            UI.WindowEnd();
        }

		public void Shutdown()
		{
			
		}
	}

}
