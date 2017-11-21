package se.etimo.bot;

import com.fasterxml.jackson.databind.JsonNode;
import org.junit.Before;
import org.junit.Test;
import se.etimo.jsontools.Json;

import java.io.IOException;

import static org.junit.Assert.*;

public class UtilityBotTest {
    String botJson =
            "{\"name\":\"1510562724\"," +
                    "\"id\":\"911e189f-0b07-4972-8490-300f96d9401b\"," +
                    "\"email\":\"1510562724@etimo.se\"," +
                    "\"token\":\"5fefcef9-7bf7-4176-9d62-1dd685224ff0\"}";
    String boardJson =
            "{\"diamonds\":[{\"x\":0,\"y\":2}," +
                    "{\"x\":9,\"y\":6},{\"x\":5,\"y\":2},{\"x\":9,\"y\":6},{\"x\":8,\"y\":5},{\"x\":5,\"y\":5}" +
                    ",{\"x\":1,\"y\":0},{\"x\":7,\"y\":6},{\"x\":8,\"y\":6},{\"x\":2,\"y\":5},{\"x\":8,\"y\":8}," +
                    "{\"x\":0,\"y\":8},{\"x\":2,\"y\":9},{\"x\":1,\"y\":5},{\"x\":2,\"y\":5},{\"x\":5,\"y\":3}," +
                    "{\"x\":4,\"y\":6},{\"x\":3,\"y\":9},{\"x\":5,\"y\":3},{\"x\":0,\"y\":2}]," +
                    "\"bots\":[{\"diamonds\":0,\"millisecondsLeft\":59901,\"" +
                    "score\":0,\"timeJoined\":\"2017-11-13T08:45:25.237Z\"," +
                    "\"name\":\"1510562724\",\"position\":{\"x\":2,\"y\":0}," +
                    "\"botId\":\"911e189f-0b07-4972-8490-300f96d9401b\"," +
                    "\"nextMoveAvailableAt\":\"0001-01-01T00:00:00Z\"," +
                    "\"base\":{\"x\":2,\"y\":0}}],\"minimumDelayBetweenMoves\":100,\"width\":10,\"id\":\"1\"" +
                    ",\"gameObjects\":[{\"linkedTeleporterString\":\"229a2157-680a-4e6d-946c-509ca8c45aaf\"," +
                    "\"isBlocking\":false,\"name\":\"Teleporter\",\"position\":{\"x\":1,\"y\":7}}," +
                    "{\"linkedTeleporterString\":\"229a2157-680a-4e6d-946c-509ca8c45aaf\"," +
                    "\"isBlocking\":false,\"name\":\"Teleporter\",\"position\":{\"x\":3,\"y\":1}}," +
                    "{\"isBlocking\":true,\"name\":\"Wall\",\"position\":{\"x\":4,\"y\":3}}," +
                    "{\"isBlocking\":true,\"name\":\"Wall\",\"position\":{\"x\":4,\"y\":8}}," +
                    "{\"isBlocking\":true,\"name\":\"Wall\",\"position\":{\"x\":6,\"y\":2}}],\"height\":10}";
    JsonNode boardJsonNode=null;
    JsonNode botJsonNode=null;
    UtilityBot utilBot = null;
    @Before
    public void setup(){
        try {
            boardJsonNode= Json.JSON_MAPPER.readTree(boardJson);
            botJsonNode =  Json.JSON_MAPPER.readTree(botJson);
        } catch (IOException e) {
            e.printStackTrace();
        }
        utilBot = new UtilityBot(botJsonNode) {
            @Override
            public String getNextMove(JsonNode boardJson) {
                return null;
            }
        };
    }
    @Test
    public  void testGetOwnNode(){
        //JSONObject botSelf = utilBot.getBotSelf(boardJsonNode);
    }

}