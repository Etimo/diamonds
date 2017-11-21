package se.etimo.bot;

import com.fasterxml.jackson.databind.JsonNode;
import se.etimo.constants.Directions;
import se.etimo.game.GameUtility;


public class RandomBot extends UtilityBot{
    GameUtility util = new GameUtility();
   public RandomBot(JsonNode object){
        super(object);
   }
    public  RandomBot(String email,String name,String token,String id){
       super(email,name,token,id);
    }


    @Override
    public String getNextMove(JsonNode boardJson) {
        int index = (int)(Math.random() * Directions.values().length);
        return Directions.values()[index].toString();
    }
}
