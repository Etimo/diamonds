package se.etimo;

import com.fasterxml.jackson.databind.JsonNode;
import se.etimo.api.DiamondsApi;
import se.etimo.api.UnirestAPI;
import se.etimo.bot.RandomBot;
import se.etimo.game.GamePlayer;

public class Play {
    public static void main(String[] args) throws Exception {
        DiamondsApi api = new UnirestAPI("https://diamonds.etimo.se");
        String name = "BotNameHere";
        String email = "emailhere@url.com";
        String token = null; //Add token if you have it...
        String id = null; // Same with ID.
        RandomBot randomBot;

        if(token == null||id == null){
            JsonNode registerBot
                = api.registerBot(email, name);
            randomBot=new RandomBot(registerBot);
            System.out.println("Token and ID: "+randomBot.getToken()+
                    " : "+randomBot.getId()); //Get id from API
        }

        else{
            randomBot=new RandomBot(email,name,token,id);
        }

        GamePlayer player = new GamePlayer(randomBot,api);
        player.playBoard("1");
    }
}
