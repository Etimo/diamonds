package se.etimo.api;


import com.fasterxml.jackson.databind.JsonNode;

public interface DiamondsApi {
    public boolean joinBoard(String boardId,String botToken) throws Exception;
    public JsonNode moveBot(String boardId, String botToken, String moveDirection) throws Exception;
    public JsonNode registerBot(String email, String name) throws Exception;
    public JsonNode pollBoard(String boardId) throws Exception;
}
