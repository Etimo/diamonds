package se.etimo.api;


import com.fasterxml.jackson.databind.JsonNode;
import com.mashape.unirest.http.HttpResponse;
import com.mashape.unirest.http.Unirest;
import com.mashape.unirest.http.exceptions.UnirestException;
import org.json.JSONObject;
import se.etimo.constants.Directions;
import static se.etimo.jsontools.Json.JSON_MAPPER;
import java.io.IOException;

/**
 * This is a skeleton for calling the Etimo Diamonds API as defined at:
 * https://github.com/Etimo/diamonds
 *
 * Most calls return a Unirest JsonNode for further parsing by the bot logic.
 */
public class UnirestAPI implements  DiamondsApi{
    //Url API parts.
    final static String BOARD_BASE = "/Boards/";
    final static String MOVE = "/move";
    final static String REGISTER = "/bots";
    final static String JOIN = "/join";
    final static String TOKEN_KEY = "botToken";
    final static String DIRECTION_KEY="direction";
    final static String EMAIL_KEY = "email";
    final static String NAME_KEY = "name";
    private final String urlWithHttp;
    public UnirestAPI(String urlWithHttp){
        this.urlWithHttp  = urlWithHttp.endsWith("/api") ?
                urlWithHttp : urlWithHttp+"/api";
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            try {
                Unirest.shutdown();
            } catch (IOException e) {
            }
        }));
    }

    /**
     * Polls the status of board with the provided ID.
     * Returns the reply as json.
     * @param boardId
     */
    public JsonNode pollBoard(String boardId) throws Exception {
        return
                JSON_MAPPER.readTree(Unirest.get(urlWithHttp+BOARD_BASE+boardId).
                        asString().getBody());
    }

    /**
     * Register a new bot with no previous token,
     * the token will be the return string if succefull.
     * @param email @code{String} value for the email associated with the bot.
     * @param botName @code{String} value will be displayed on the page.
     * @return @code{String} Token if succefull.
     */
    public JsonNode registerBot(String email,String botName) throws Exception{
        /*
        {
  "id": "string",
  "name": "string",
  "email": "string",
  "token": "string"*/

        JSONObject json = new JSONObject();
        json.put(EMAIL_KEY,email);
        json.put(NAME_KEY,botName);

        HttpResponse<String> stringHttpResponse = Unirest.post(urlWithHttp + REGISTER)
                .header("Content-Type", "application/json")
                .body(json).asString();
         return JSON_MAPPER.readTree(stringHttpResponse.getBody());

    }

    public boolean joinBoard(String boardId,String botToken) throws UnirestException {
        JSONObject json = new JSONObject();
        json.put(TOKEN_KEY, botToken);
        HttpResponse<String> response = Unirest.post(
                this.urlWithHttp + BOARD_BASE + boardId + JOIN)
                .header("Content-Type", "application/json").body(json).asString();
        return response.getStatus()==200;

    }

    /**
     * Sends a move command to the server.
     * @param boardId The id of the board
     * @param botToken The token of the bot.
     * @param direction Direction from the "directions" enum
     * @return @code{JsonNode} Returns an optional with Json if the response status is 200,
     * 409 is an illegal move according to the API and will return the output of @{link {@link #pollBoard(String)}
     * @throws Exception @{code UnirestException} can be thrown by the HTTP library used.
     */
    public JsonNode moveBot(String boardId,String botToken,String direction) throws Exception {
        JSONObject json = new JSONObject();
        json.put(TOKEN_KEY,botToken);
        json.put(DIRECTION_KEY,direction);
        HttpResponse<String> stringHttpResponse = Unirest.post(urlWithHttp + BOARD_BASE + boardId + MOVE)
                .header("Content-Type", "application/json")
                .body(json).asString();
        return stringHttpResponse.getStatus() == 409 ? pollBoard(boardId) :
                    JSON_MAPPER.readTree(stringHttpResponse.getBody());

    }
}
