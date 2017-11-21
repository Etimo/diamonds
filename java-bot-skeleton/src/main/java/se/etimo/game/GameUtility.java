package se.etimo.game;

import com.fasterxml.jackson.databind.JsonNode;
import se.etimo.bot.BaseBot;

import java.text.DateFormat;
import java.text.ParseException;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeFormatterBuilder;
import java.time.temporal.ChronoField;
import java.time.temporal.ChronoUnit;
import java.time.temporal.TemporalAccessor;
import java.util.Date;
import java.util.Optional;
import java.util.TimeZone;

import static se.etimo.jsontools.Json.getStream;

public class GameUtility {
    final static String DATE_FORMAT_STRING = "";
    final static String WIDTH_KEY = "width";
    final static String HEIGHT_KEY = "width";
    final static String X_KEY = "x";
    final static String Y_KEY = "y";
    final static String POSITION = "Position";
    final static String NEXT_MOVE = "nextMoveAvailableAt";
    final static String GAME_OBJECTS = "gameObjects";
    final static String DIAMONDS = "diamonds";
    final static String BOTS = "bots";
    final static String BOT_ID = "botId";
    final static String MINIMUM_DELAY ="minimumDelayBetweenMoves";

    private class Position{
        final int x;
        final int y;

        Position(int x,int y){
            this.x=x;
            this.y=y;
        }

        boolean compare(Position pos){
            return this.x==pos.x && pos.y==pos.y;
        }


    }

    public Optional<Position> createPosition(JsonNode node){
        if(node.has(POSITION)){
            return  Optional.of(new
                    Position(node.get(POSITION).get(X_KEY).asInt(),node.get(POSITION).get(Y_KEY).asInt()));
        }
        if(node.has(X_KEY)&&node.has(Y_KEY)){
            return  Optional.of(new
                    Position(node.get(X_KEY).asInt(),node.get(Y_KEY).asInt()));
        }
        return Optional.empty();
    }
    /**
     * Gets the board width and height keys and returns them as an int
     * array where [0],[1]=width,height
     * @param board @{code JSONObject} JSon returned by the diamonds API for pollboard or move.
     * @return @{code int[]} [0] = width, [1] = height
     */
    public int[] getBoardDimensions(JsonNode board){
        return new int[] {board.get(WIDTH_KEY).asInt(), board.get(HEIGHT_KEY).asInt()};
    }
    public Optional<JsonNode> getBotSelf(JsonNode json,BaseBot bot){
        JsonNode bots = json.findPath(BOTS);
        return bots == null ? Optional.empty() :
                getStream(bots.iterator())
                .filter(node -> node.findPath(BOT_ID).asText()
                        .equals(bot.getId())).findFirst();

    }

    /**
     *
     * @param dateText
     * @return
     */
    private Long parseWaitTime(String dateText){
        if(dateText.startsWith("000")){
            return 0L;
        }
        Instant instant = Instant.parse(dateText);

        LocalDateTime utc = Instant.now().atZone(ZoneId.of("UTC")).toLocalDateTime();
        long until = utc.until(instant.atZone(ZoneId.of("UTC")), ChronoUnit.MILLIS);
        return until<0 ? 0 :until;
    }
    public void sleepUntilNextMove(JsonNode botJson,JsonNode boardJson) throws InterruptedException {
        Optional<Long> waitTime = Optional.ofNullable(botJson.findValue(NEXT_MOVE).asText()).map(
                        dateText -> parseWaitTime(dateText));
               if(waitTime.isPresent()){
                   Thread.sleep(waitTime.get());
                   return;
               }
               Optional<Integer> time =  Optional.ofNullable(boardJson.findValue(MINIMUM_DELAY).asInt());
               Thread.sleep(time.orElse(100));


    }
    /**
     * Returns an array of X and Y int values representing the bots position on the board.
     * @param board Board jsonNode or getBotSelf derived JsonNode.
     * @return @{code Optional@literal<Integer[]@literal> xy= {x,y}}
     */
    public Optional<Position> getPosition(JsonNode board, BaseBot bot){
        Optional<JsonNode> botOptional = getBotSelf(board,bot);

        return botOptional.isPresent() ?
                createPosition(botOptional.get()):
                Optional.empty();
    }

    /**
     * Returns a node containing all nodes representing diamonds, or and empty Optional if
     * such node is found.
     * @param board @{code JsonNode} retrieved from Diamonds API
     * @return @{code JsonNode} containing all nodes representing diamonds.
     */
    public Optional<JsonNode> getDiamonds(JsonNode board){
        return Optional.<JsonNode>ofNullable(board.findPath(DIAMONDS));
    }


    /**
     * Check position for a diamond
     * @param board
     * @param xy
     * @return
     */
    public Optional<JsonNode> checkForDiamond(JsonNode board, int[] xy) {
        Position checkPos = new Position(xy[0],xy[1]);
        return getDiamonds(board).flatMap(o ->
                getStream(o.iterator()).filter(n -> {
                    Optional<Position> pos = createPosition(n);
                    return pos.isPresent() && pos.get().compare(checkPos);
                }).findFirst());

    }
}
