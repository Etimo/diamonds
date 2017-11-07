
using System.Collections.Generic;
using Diamonds.Common.Entities;
using System.Linq;
using Diamonds.Common.Models;
using Diamonds.Common.GameEngine.GameObjects;
namespace Diamonds.Common.GameEngine.GameObjects.Teleporters
{
    public class Teleporter : IGameObject
    {
        public const string NameString = "Teleporter";
        private string generateRandomString(){
            string  uuid = System.Guid.NewGuid().ToString();
            return uuid;
        }
        /**
        * Creates an instance of Teleporter with a random 
        * LinkedTeleporterString that can be used to link with another
        * teleporter.
         */
        public Teleporter(){
            this.LinkedTeleporterString=generateRandomString();

        }
        /**
        * Creates an instance of Teleporter with a preset 
        * that will be used to link it to another teleporter.
         */
        public Teleporter(string linkedTeleporterString){
            this.LinkedTeleporterString=linkedTeleporterString;
        }
        public string LinkedTeleporterString { get; 
        set;} //Teleporters with the same string will link.
        public string Name
        {
            get
            {
                return NameString;
            }
        }

        public Position Position { get; set; }
        public bool IsBlocking
        {
            get { return false; }
            set { }
        }

        public void PerformInteraction(Board board, BoardBot bot)
        {
            IGameObject targetTeleporter = board.GameObjects.Where(gf=>gf is Teleporter 
            && (gf as Teleporter).LinkedTeleporterString==this.LinkedTeleporterString 
            && !this.Position.Equals(gf.Position)).FirstOrDefault();
            if(targetTeleporter==null){
                return;
            }
            if(targetTeleporter !=null && !board.IsPositionBlocked(targetTeleporter.Position)){
                bot.Position = targetTeleporter.Position;
            }

        }
    }
}