using System.Collections.Generic;
using Game.Characters;

namespace Game.GameData
{
    public class GamePlayData
    {
        public MainCharacterData _mainCharacterData;

        public Dictionary<int, NetCharacterData> _dic = new Dictionary<int, NetCharacterData>();
    }
}