package se.etimo.bot;

import com.fasterxml.jackson.databind.JsonNode;
import org.json.JSONObject;

public abstract class UtilityBot  implements BaseBot{
    final String email;
    final String name;
    final String token;
    final String id;


    public UtilityBot(JsonNode object){

        this.email=object.findValue("email").asText();
        this.token=object.findValue("token").asText();
        this.name=object.findValue("name").asText();
        this.id=object.findValue("id").asText();
    }
    public  UtilityBot(String email,String name,String token,String id){
        this.email=email;
        this.token=token;
        this.name=name;
        this.id=id;
    }
    @Override
    public String getEmail() {
        return email;
    }
    @Override
    public String getId() {
        return id;
    }

    @Override
    public String getName() {
        return name;
    }

    @Override
    public String getToken() {
        return token;
    }
    @Override
    public abstract String getNextMove(JsonNode board);
}
