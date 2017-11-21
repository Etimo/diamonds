package se.etimo.api;

import com.fasterxml.jackson.databind.JsonNode;
import org.junit.Test;

import static org.junit.Assert.*;

/**
 * V1 of this test assumes a running backend, this will be gradually replaced by mocks and separated tests.
 * This is used for rapid development.
 *
 */
public class UnirestAPITest {
    String botName = System.currentTimeMillis()/1000+"";
    String botEmail = botName +"@etimo.se";
    String fullUrl = "http://localhost:80";

    @Test
    public void registerJoinAndMoveTest() throws Exception {
        UnirestAPI caller = new UnirestAPI(fullUrl);
        JsonNode registerNode = caller.registerBot(botEmail, botName);
        System.out.println(registerNode.toString());
        assertTrue(registerNode.get("token")!=null);
        assertTrue(caller.joinBoard("1", registerNode.get("token").asText()));
        JsonNode boardNode = caller.pollBoard("" + 1);
        System.out.println(boardNode.toString());
        //Will trigger exceptions if not present.
         assertTrue(boardNode.has("gameObjects"));
        assertTrue(boardNode.has("bots"));


    }

}
