using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.VL.Scripts
{
    public class Constraints
    {
        public const string Player_PlayerName = "Player";
        public const string Player_Action_Chop = "playerChop";
        public const string Player_Action_Hit = "playerHit";

        public const string Enemy_Action_Attack = "enemyAttack";

        public const string Axis_Horizontal = "Horizontal";
        public const string Axis_Vertical = "Vertical";

        public const string Tag_Exit = "Exit";
        public const string Tag_Food = "Food";
        public const string Tag_Soda = "Soda";

        public const string Text_Level = "LevelText";
        public const string Image_Level = "LevelImage";



        public static string GetFunctionName(Action method)
        {
            return method.Method.Name;
        }
    }
}
