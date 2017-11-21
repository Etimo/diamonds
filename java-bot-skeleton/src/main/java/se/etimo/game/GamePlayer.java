package se.etimo.game;

import com.fasterxml.jackson.databind.JsonNode;
import se.etimo.api.DiamondsApi;
import se.etimo.api.UnirestAPI;
import se.etimo.bot.BaseBot;

import java.util.Optional;

public class GamePlayer {
    public  final BaseBot bot;
    public final DiamondsApi api;
    private GameUtility util = new GameUtility();

    /**
     * Bot should be created from stored info, or from call to
     * DiamondsApi.registerBot
     * @param bot Bot created with necessary info.
     * @param api @{code DiamondsApi} implementation
     */
    public GamePlayer(BaseBot bot, DiamondsApi api){
         this.api = api;
         this.bot=bot;
    }
    /**
     * If bot has no token it will be registered, otherwise
     * bot will immediatley try to join the provided boardId.
     * @param boardId @{code String} Id of the board to join.
     */
    public String playBoard(String boardId) throws Exception {
        if(!api.joinBoard(boardId,bot.getToken())){
           return String.format("Error joining {}",boardId);
        };

        JsonNode boardJson = api.pollBoard(boardId);
        Optional<JsonNode> botNode=util.getBotSelf(boardJson,bot);

        while(boardJson!=null &&
                (botNode=util.getBotSelf(boardJson,bot)).isPresent()){
            util.sleepUntilNextMove(botNode.get(),boardJson);
            String nextMove = bot.getNextMove(boardJson);
             boardJson = api.moveBot(boardId,bot.getToken(),nextMove);
        }


        return String.format("End of run for {} on {}",
                new Object[]{bot.getToken(),boardId});


    }
}
