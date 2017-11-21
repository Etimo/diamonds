package se.etimo.bot;


import com.fasterxml.jackson.databind.JsonNode;

/**
 * Simple bot interface to be plugged into the polling 
 * functions.
 */
public interface BaseBot {
    public String getEmail();
    public String getName();
    public String getToken();
    public String getId();
    public String getNextMove(JsonNode boardJson);
}
