using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public class UserInput : Singleton<UserInput>
    {
        public Action<UserInputType> UserInteracted { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.OnUserInteracted(UserInputType.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.OnUserInteracted(UserInputType.Right);
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.OnUserInteracted(UserInputType.Up);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.OnUserInteracted(UserInputType.Down);
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                this.OnUserInteracted(UserInputType.Space);
            }
            else if(Input.GetKeyDown(KeyCode.Tab))
            {
                this.OnUserInteracted(UserInputType.ToggleCubeMapMode);
            }
        }

        private void OnUserInteracted(UserInputType type)
        {
            if (UserInteracted ==  null)
                return;

            this.UserInteracted(type);
        }
    }
}
