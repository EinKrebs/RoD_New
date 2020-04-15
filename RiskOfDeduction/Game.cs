using System.Collections.Generic;
using System.Windows.Forms;

namespace RiskOfDeduction
{
    public class Game
    {
        private HashSet<Keys> Keys { get; set; } = new HashSet<Keys>();

        public List<ListItem> List { get; set; } = new List<ListItem>();
        
        public Player player { get; }

        public Game(Player player)
        {
            this.player = player;
        }

        public void Act()
        {
            var count = List.Count;
            for (int i = 0; i < count; i++)
            {
                List[i].Action = List[i].GameObject.Act();
            }

            for (var i = 0; i < count; i++)
            {
                var newFirstHitBox = new Rectangle(List[i].GameObject, List[i].Action);
                for (var j = i + 1; j < count; j++)
                {
                    var secondNewHitBox = new Rectangle(List[j].GameObject, List[j].Action);
                    if (!(newFirstHitBox & secondNewHitBox)) continue;
                    List[i].Dies = List[i].Dies || List[i].GameObject.DiesInConflict(List[j].GameObject);
                    List[j].Dies = List[j].Dies || List[j].GameObject.DiesInConflict(List[i].GameObject);
                }
            }

            for (var i = count - 1; i >= 0; i--)
            {
                if (List[i].Dies)
                {
                    List.RemoveAt(i);
                }
                else
                {
                    List[i].GameObject.Update(List[i].Action);
                }
            }
        }

        public void HandleKey(Keys e, bool down)
        {
            if (down)
            {
                Keys.Add(e);
            }
            else
            {
                Keys.Remove(e);
            }
        }

        public void AddObject(IGameObject gameObject)
        {
            List.Add(new ListItem(gameObject));
        }
    }
}